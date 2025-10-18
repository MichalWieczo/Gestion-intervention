using Microsoft.Extensions.Logging;

namespace Gestion_intervention
{
    using CommunityToolkit.Maui;
    using Gestion_de_stock.Utilities.DataAccess.Files;
    using Gestion_intervention.Utilities.DataAccess;
    using Gestion_intervention.Utilities.Interfaces;
    using Gestion_intervention.Utilities.Services;
    using Gestion_intervention.ViewModel;
    using Microsoft.Extensions.DependencyInjection;

    public static class MauiProgram
    {
        // Chemin de config utilisé sur Windows/macOS uniquement
        private const string CONFIG_File =
            @"C:\Users\micha\OneDrive\Bureau\Appli Barry Callebaut\Gestion intervention\Gestion intervention\Configuration\Datas\ConfigJson.txt";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Services communs
            builder.Services.AddSingleton<IAlertService, AlertServiceDisplay>();

            builder.Services.AddSingleton(_ => new DataFilesManager(CONFIG_File));
            builder.Services.AddSingleton<IDataAccess>(sp =>
                new DataAccessJsonFile(sp.GetRequiredService<DataFilesManager>()));

            // MVVM
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
