using CommunityToolkit.Maui.Views;
using Gestion_intervention.Model.Gestion_intervention.Entities;
using Gestion_intervention.Utilities.Interfaces;
using Gestion_intervention.View;
using Gestion_intervention.ViewModel;

namespace Gestion_intervention
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageVM, IDataAccess dataAccessService, IAlertService alertService)
        {
            dataAccess = dataAccessService;
            alert = alertService;

            mainPageViewModel = mainPageVM;
            // Définition du BindingContext avec le ViewModel 
            BindingContext = mainPageVM;

            InitializeComponent();
        }

        private IDataAccess dataAccess;
        private IAlertService alert;
        private MainPageViewModel mainPageViewModel;

        private async void OnOpenPopupClicked(object sender, EventArgs e)
        {
            // Récupère l’intervention sélectionnée si tu veux éditer
            var toEdit = (BindingContext as MainPageViewModel)?.SelectedIntervention;

            // Ouvre la popup (on lui passe le VM comme dans ta classe)
            var popup = new AddInterventionPopup(mainPageViewModel, toEdit);
            var result = await this.ShowPopupAsync(popup) as Intervention;

            // Si l’utilisateur a validé
            if (result != null)
            {
                // À toi de choisir : ajout si nouveau, remplace si même Id, etc.
                // Exemple basique :
                var vm = (MainPageViewModel)BindingContext;
                var existing = vm.Interventions.FirstOrDefault(i => i.Id == result.Id);
                if (existing == null)
                    vm.Interventions.Add(result);
                else
                {
                    // Mise à jour des champs principaux
                    existing.Name = result.Name;
                    existing.StartTime = result.StartTime;
                    existing.EndTime = result.EndTime;
                    existing.CategoryType = result.CategoryType; 
                    existing.Problem = result.Problem;   
                    existing.Cause = result.Cause;    
                    existing.Solution = result.Solution;  
                    existing.Description = result.Description;
                }
            }
        }
    }
}
