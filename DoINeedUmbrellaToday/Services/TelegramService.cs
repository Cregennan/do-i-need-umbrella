using DoINeedUmbrellaToday.Exceptions;
using DoINeedUmbrellaToday.Localization;
using DoINeedUmbrellaToday.Models;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DoINeedUmbrellaToday.Services
{
    /// <summary>
    /// Сервис, обрабатывающий запросы от Telegram
    /// </summary>
    public class TelegramService
    {

        /// <summary>
        /// Входная точка сервиса, в зависимости от состояния пользователя по разному обрабатывает запрос
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<IActionResult> Process(Update update)
        {

            if (update.Message is null)
            {
                return new OkObjectResult("OK");
            }

            var chatId = update?.Message?.Chat.Id;
            TelegramChat? currentChat = context.TelegramChats.Where(x => x.Id == chatId).FirstOrDefault();
            if (currentChat == null)
            {
                currentChat = new TelegramChat
                {
                    Id = (long)chatId,
                    Language = "ru",
                    State = "lang"
                };
                context.TelegramChats.Add(currentChat);
                await context.SaveChangesAsync();
            }

            TelegramBotProgress? currentProgress = context.TelegramBotProgresses.Where(x => x.Id == chatId).FirstOrDefault();
            if (currentProgress == null)
            {
                currentProgress = new TelegramBotProgress
                {
                    Id = (long)chatId
                };
                context.TelegramBotProgresses.Add(currentProgress);

                await context.SaveChangesAsync();
            }

            //Получаем локализацию для текущего языка
            this.localizer = BotLocalizer.GetCurrentLocalizer(currentChat.Language);

            switch (currentChat.State)
            {
                //Состояние - выбор языка
                case "lang":
                    return await ResolveStateLang(update, currentChat, currentProgress);
                //Состояние - выбор города
                case "city":
                    return await ResolveStateCity(update, currentChat, currentProgress);
                //Состояние - выдача погоды
                default:
                    return await ResolveStateReady(update, currentChat, currentProgress);
            }

            return new OkObjectResult("OK");
        }


        /// <summary>
        /// Обработчик состояния "Выбор языка"
        /// </summary>
        /// <param name="update"></param>
        /// <param name="chat"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IActionResult> ResolveStateLang(Update update, TelegramChat chat, TelegramBotProgress progress)
        {
            var langCollection = new[] { "Русский", "Татарча", "English" };

            //Пользователь ввел язык
            if (langCollection.Contains(update.Message.Text))
            {
                //Выбираем язык
                chat.Language = update.Message.Text switch
                {
                    "Русский" => "ru",
                    "Татарча" => "tt",
                    "English" => "en",
                    _ => "ru",
                };

                localizer = BotLocalizer.GetCurrentLocalizer(chat.Language);
                
                //Меняем состояние бота на "Выбор города"
                chat.State = "city";

                await context.SaveChangesAsync();

                await client.SendTextMessageAsync(chat.Id, localizer["langSelected"],
                    //После выбора языка убираем клавиатуру
                    replyMarkup: new ReplyKeyboardRemove()
                );
            }
            else
            {
                await client.SendTextMessageAsync(chat.Id, String.Format(localizer["welcome"], update.Message.Chat.FirstName),
                    //Выдаем пользователю клавиатуру с вариантами языков
                    replyMarkup: new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Русский", "English", "Татарча" }, }) { ResizeKeyboard = true }
                );
            }

            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Состояние - выбор города
        /// </summary>
        /// <param name="update"></param>
        /// <param name="chat"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IActionResult> ResolveStateCity(Update update, TelegramChat chat, TelegramBotProgress progress)
        {

            //Получаем кнопки подтверждения "да-нет" для текущего языка
            var confirmationButtons = this.ConfirmationButtons(chat);


            if (update.Message is not { } message)
            {
                return new OkObjectResult("OK");
            }
            if (message.Text is not { } messageText)
            {
                return new OkObjectResult("OK");
            }

            try
            {

                //Если пользователь выбрал вариант ответа на вопрос "Правильно ли мы выбрали город"
                if (confirmationButtons.Contains(messageText) || progress.Latitude != null)
                {
                    if (messageText == this.AgreeButton(chat))
                    {
                        //меняем состояние на "выдачу погоды"
                        chat.State = "ready";
                        await context.SaveChangesAsync();
                        
                        //Отправляем ответ
                        await client.SendTextMessageAsync(message.Chat.Id, localizer["citySelected"],
                            replyMarkup: new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton(localizer["weatherTemplate"]) }, }) { ResizeKeyboard = true }
                        );
                    }
                    else
                    {
                        //ставим метку того, что пользователь отказался от предложения по городу
                        progress.Latitude = null;
                        await context.SaveChangesAsync();
                        throw new TelegramMessageResultException(localizer["cityNotFound"], dropMarkup: true);
                    }
                }
                //Если пользователь ввел что-то еще, то воспринимаем это как запрос
                else
                {
                    //Вводим название города
                    var query = update.Message.Text;
                    if (query == null || query.Trim().Length == 0)
                    {
                        throw new TelegramMessageResultException(localizer["cityRequest"]);
                    }

                    var result = await location.GetLocationAsync(query.Trim());

                    //Если введено неверно
                    if (result.CityName == null)
                    {
                        throw new TelegramMessageResultException(localizer["cityBad"]);
                    }

                    //Сохраняем полученные координаты города в базе
                    progress.Latitude = result.Latitude;
                    progress.Longitude = result.Longitude;
                    progress.City = result.CityName;
                    progress.LastQuery = query;
                    await context.SaveChangesAsync();

                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: String.Format(localizer["cityFound"], result.CityName, result.RegionName, result.CountryName),
                        //отсылаем кнопки подтверждения
                        replyMarkup: new ReplyKeyboardMarkup(new[]{confirmationButtons.Select(x => new KeyboardButton(x)),}){ResizeKeyboard = true},
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                    );
                }

            }
            catch (TelegramMessageResultException e)
            {
                await client.SendTextMessageAsync(chatId: message.Chat.Id, text: e.Message, replyMarkup: e.DropMarkup ? new ReplyKeyboardRemove() : null);
                return new OkObjectResult("OK");
            }

            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Состояние - готов (выдача погоды)
        /// </summary>
        /// <param name="update"></param>
        /// <param name="chat"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IActionResult> ResolveStateReady(Update update, TelegramChat chat, TelegramBotProgress progress)
        {
            try
            {   
                //Если пришло слово "Погода" на нужном языке
                if (update.Message.Text == localizer["weatherTemplate"])
                {
                    //Получаем погоду на сегодня через погодный сервис
                    var weather = await forecast.GetForecastForToday(new Weather.UserLocation { Latitude = progress.Latitude, Longitude = progress.Longitude });

                    //Готовим сообщение с ответом
                    string WeatherMessage = String.Format(localizer["theWeather"],
                        progress.City, //Город
                        weather.WeatherCodeIcons,
                        localizer[$"weather:{weather.WeatherCode}"], //weather:погодныйкод
                        weather.UmbrellaRequired ? localizer["umbrellaTrue"] : localizer["umbrellaFalse"],//Нужен ли зонт
                        weather.TemperatureMin,
                        weather.TemperatureMax
                        );
                        
                    //Возвращаем с той-же кнопкой и разметкой markdown
                    await client.SendTextMessageAsync(update.Message.Chat.Id, WeatherMessage,
                        replyMarkup: new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton(localizer["weatherTemplate"]) }, }) { ResizeKeyboard = true },
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                    );

                }


            }
            catch (TelegramMessageResultException e)
            {
                await client.SendTextMessageAsync(chat.Id, e.Message, replyMarkup: e.DropMarkup ? new ReplyKeyboardRemove() : null);
            }
            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Локализация кнопок подтверждения
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        private String[] ConfirmationButtons(TelegramChat chat)
        {
            return chat.Language switch
            {
                "tt" => new[] { "Әйе", "Юк" },
                "en" => new[] { "Yes", "No" },
                _ => new[] { "Да", "Нет" }
            };
        }

        private String AgreeButton(TelegramChat chat)
        {
            return this.ConfirmationButtons(chat)[0];
        }
        private String DenyButton(TelegramChat chat)
        {
            return this.ConfirmationButtons(chat)[1];
        }

        public TelegramService(ITelegramBotClient client, DefaultContext context, ILocationService location, IForecastService forecast)
        {
            this.client = client;
            this.context = context;
            this.location = location;
            this.forecast = forecast;
        }


        private BotLocalizer localizer;
        private ITelegramBotClient client;
        private DefaultContext context;
        private ILocationService location;
        private IForecastService forecast;





    }
}
