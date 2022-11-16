using DoINeedUmbrellaToday.Services;
using Telegram.Bot;

namespace DoINeedUmbrellaToday
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSingleton<HttpClient>(new HttpClient());

            builder.Services.AddTransient<IForecastService, OpenMeteoForecastService>(sp =>
                new OpenMeteoForecastService(
                    configuration: sp.GetService<IConfiguration>(),
                    client: sp.GetService<HttpClient>()
                    )
                );

            builder.Services.AddTransient<ILocationService, PositionStackLocationService>(sp =>
                new PositionStackLocationService(
                    configuration: sp.GetService<IConfiguration>(),
                    client: sp.GetService<HttpClient>()
                )
            );

            builder.Services.AddTransient<TelegramService>(sp =>
                new TelegramService(
                    client: sp.GetService<ITelegramBotClient>(),
                    context: sp.GetService<DefaultContext>(),
                    location: sp.GetService<ILocationService>(),
                    forecast: sp.GetService<IForecastService>()
                    )
            );

            var botToken = builder.Configuration.GetValue<String>("Secrets:BotSecret");

            builder.Services.AddHttpClient("tgwebhook").AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botToken, httpClient));


            builder.Services.AddDbContext<DefaultContext>();

            var app = builder.Build();

            app.UseHttpLogging();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "hook",
                    pattern: $"bot/webhook",
                    new { controller = "Telegram", action = "Post" }
                    );

                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}