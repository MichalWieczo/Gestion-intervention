using Gestion_de_stock.Utilities.DataAccess.Files;
using Gestion_intervention.Model.Gestion_intervention.Collection;
using Gestion_intervention.Utilities.Interfaces;
using Microsoft.Maui.Storage; // FileSystem.AppDataDirectory
using Newtonsoft.Json;
using System;

using Formatting = Newtonsoft.Json.Formatting;

namespace Gestion_intervention.Utilities.DataAccess
{
    public class DataAccessJsonFile : DataAccess, IDataAccess
    {
        public DataAccessJsonFile(string filePath) : base(filePath) { }
        public DataAccessJsonFile(string filePath, string[] extensions) : base(filePath, extensions) { }
        public DataAccessJsonFile(DataFilesManager dfm) : base(dfm)
        {
            DataFilesManager = dfm;
        }

        // --- Helpers ---

        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// Résout le chemin du fichier JSON selon la plateforme.
        /// - ANDROID : ~/Android/data/<app>/files/… (FileSystem.AppDataDirectory)
        /// - Autres : via DataFilesManager (clé "INTERVENTIONS")
        /// </summary>
        private string ResolveAccessPath()
        {
#if ANDROID
            // On place le fichier dans le dossier local de l’app
            var fileName = "Intervention.json";
            var path = Path.Combine(FileSystem.AppDataDirectory, fileName);
            return path;
#else
            // Windows/Mac : on continue d’utiliser ta config existante
            return DataFilesManager?.DataFiles?.GetFilePathByCodeFunction("INTERVENTIONS")
                   ?? AccessPath; // fallback si jamais
#endif
        }

        /// <summary>
        /// S’assure que le fichier existe ; si non, crée un JSON vide.
        /// </summary>
        private void EnsureFileExists(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(path))
            {
                // Fichier neuf -> on écrit une collection vide
                var empty = new InterventionCollection();
                var json = JsonConvert.SerializeObject(empty, Formatting.Indented, JsonSettings);
                File.WriteAllText(path, json);
            }
        }

        // --- IDataAccess ---

        public override InterventionCollection GetAllIntervention()
        {
            try
            {
                AccessPath = ResolveAccessPath();
                EnsureFileExists(AccessPath);

                var jsonFile = File.ReadAllText(AccessPath);
                var its = JsonConvert.DeserializeObject<InterventionCollection>(jsonFile, JsonSettings);

                return its ?? new InterventionCollection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllIntervention] {ex.Message}");
                return new InterventionCollection();
            }
        }

        public void UpdateAllIntervention(InterventionCollection its)
        {
            try
            {
                AccessPath = ResolveAccessPath();
                EnsureFileExists(AccessPath);

                var jsonFile = JsonConvert.SerializeObject(its ?? new InterventionCollection(),
                                                           Formatting.Indented, JsonSettings);
                File.WriteAllText(AccessPath, jsonFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateAllIntervention] {ex.Message}");
            }
        }
    }
}
