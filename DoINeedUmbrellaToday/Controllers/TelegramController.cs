using DoINeedUmbrellaToday.Exceptions;
using DoINeedUmbrellaToday.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Telegram.Bot.Types;

namespace DoINeedUmbrellaToday.Controllers
{

    public class TelegramController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            /* А почему ввод тела запроса через [FromBody]Update update не работает? 
             * Я понимаю что такие маневры скорее всего влекут проблемы с безопасностью, но вроде должно работать
             */

            using (var reader = new StreamReader(Request.Body))
            {
                try
                {
                    //Читаем тело POST запроса
                    string body = await reader.ReadToEndAsync();

                    Debug.WriteLine(body);
                    
                    //Парсим данные телеграма
                    Update update = JsonConvert.DeserializeObject<Update>(body);

                    if (update == null)
                    {
                        throw new BadHttpRequestException("Неправильный запрос от Telegram");
                    }

                    return await this.service.Process(update);
                }
                catch (TelegramException e)
                {
                    return new BadRequestObjectResult(e.Message);
                }
            }
        }

        private TelegramService service;

        public TelegramController(TelegramService service)
        {
            this.service = service;
        }

    }
}
