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
    using Microsoft.Maui.Devices;
    using Microsoft.Maui.Storage;
    using System;
    using System.IO;

    public static class MauiProgram
    {
        // Chemin de config historique (Windows). Utilisé si le fichier existe encore sur la machine.
        private const string LegacyConfigFile =
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

            var configPath = EnsureConfigurationFile();

            builder.Services.AddSingleton(_ => new DataFilesManager(configPath));
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
        private static string EnsureConfigurationFile()
        {
            // Si le fichier historique existe encore (poste de dev Windows), on le conserve.
            if (DeviceInfo.Platform == DevicePlatform.WinUI && File.Exists(LegacyConfigFile))
            {
                return LegacyConfigFile;
            }

            var appDataDirectory = FileSystem.Current.AppDataDirectory;
            var configPath = Path.Combine(appDataDirectory, "ConfigJson.txt");

            if (!File.Exists(configPath))
            {
                var folderLine = $"FOLDER,{appDataDirectory}{Path.DirectorySeparatorChar}";
                var interventionsLine = "INTERVENTIONS,Intervention.json";
                var content = string.Join(Environment.NewLine, folderLine, interventionsLine);

                File.WriteAllText(configPath, content);
            }

            return configPath;
        }
    }
}
