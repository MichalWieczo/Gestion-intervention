using Gestion_de_stock.Utilities.DataAccess.Files;
using Gestion_intervention.Model.Gestion_intervention.Collection;
using Gestion_intervention.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gestion_intervention.Utilities.DataAccess
{
    public abstract class DataAccess : IDataAccess
    {
        private string _accessPath;

        public DataAccess(string filePath)
        {
            _accessPath = filePath;
        }
        public DataAccess(string filePath, string[] extensions)
        {
            Extensions = new List<string>(extensions.ToList());
            _accessPath = filePath;
        }
        public DataAccess(DataFilesManager dfm, IAlertService alertService)
        {
            this.DataFilesManager = dfm;
            this.alertService = alertService;
        }

        protected DataAccess(DataFilesManager dfm)
        {
            this.dfm = dfm;
        }

        protected IAlertService alertService;
        private DataFilesManager dfm;

        public List<string> Extensions { get; set; }

        public DataFilesManager DataFilesManager { get; set; }
        public virtual string AccessPath
        {
            get => _accessPath;

            set
            {
                _accessPath = value;
            }
        }//end AccessPath

        public bool IsValidAccessPath => CheckAccessPath(AccessPath);

        public abstract InterventionCollection GetAllIntervention();

        public bool CheckAccessPath(string tryPath)
        {

            if (File.Exists(tryPath))
            {
                if (Extensions?.Any() ?? false)
                {
                    string pattern = "";
                    foreach (string ext in Extensions)
                    {
                        pattern += ext + "|";
                    }
                    pattern = pattern.Substring(0, pattern.Length - 1);
                    //check extension file
                    if (!Regex.IsMatch(tryPath, pattern + "$")) //pattern exemple = ".txt|.csv$"
                    {
                        Console.WriteLine($"The file extension {tryPath} is not valid. Expected extensions: {pattern}", "File error");
                        return false;
                    }

                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"[File error] The file {tryPath} does not exist.");
                //alertService.ShowAlert($"Le fichier {tryPath} n'existe pas", "Erreur de fichier");
                return false;
            }
        }

        public void UpdateAllIntervention(InterventionCollection interventions)
        {
            throw new NotImplementedException();
        }
    }
}
