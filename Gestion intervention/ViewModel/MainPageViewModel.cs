using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gestion_intervention.Model.Gestion_intervention.Collection;
using Gestion_intervention.Model.Gestion_intervention.Entities;
using Gestion_intervention.Utilities.Interfaces;
using Gestion_intervention.View;
using Newtonsoft.Json;

namespace Gestion_intervention.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(IDataAccess dataAccessService, IAlertService alertService)
    : base(alertService)
        {
            dataAccess = dataAccessService;

            Interventions = dataAccessService.GetAllIntervention();
            Interventions ??= new InterventionCollection(); // ← important
        }

        private IDataAccess dataAccess;

        public InterventionCollection Interventions { get; set; }

        [ObservableProperty]
        private Intervention selectedIntervention;

        [ObservableProperty]
        private Intervention interventionPopupDisplayed;

        [ObservableProperty]
        private bool isNewIntervention;

        [RelayCommand]
        private async void ShowInterventionDetails(Intervention intervention)
        {
            if (selectedIntervention != null)
            {
                var itr = selectedIntervention;

                string startStr = itr.StartTime?.ToString("dd-MM-yy HH:mm:ss") ?? "—";
                string endStr = itr.EndTime?.ToString("dd-MM-yy HH:mm:ss") ?? "—";
                string durationStr = itr.Duration.HasValue ? FormatDuration(itr.Duration.Value) : "—";

                string message =
                    $"Intervention name: {itr.Name}\n" +
                    $"Start date and time: {startStr}\n" +
                    $"End date and time: {endStr}\n" +
                    $"Intervention duration: {durationStr}\n" +
                    $"Category: {itr.CategoryType}\n" +
                    $"Problem code: {itr.Problem}\n" +
                    $"Cause code: {itr.Cause}\n" +
                    $"Solution code: {itr.Solution}\n" +
                    $"Description: {itr.Description}";


                await alertService.ShowAlert("Intervention details", message);
            }
            else
            {
                await alertService.ShowAlert("No intervention selected", "Please select an intervention to view the details.");
            }
        }

        // Arrondit à la seconde et formate proprement (gère > 24h)
        private static string FormatDuration(TimeSpan ts)
        {
            var s = TimeSpan.FromSeconds(Math.Round(ts.TotalSeconds)); // arrondi à la seconde
            if (s.TotalHours >= 24)
                return $"{(int)s.TotalDays}j {s:hh\\:mm\\:ss}";
            return s.ToString(@"hh\:mm\:ss");
        }


        [RelayCommand]
        private async Task DeleteIntervention()
        {
            if (selectedIntervention == null)
            {
                await alertService.ShowAlert("No intervention selected", "Please select an intervention to delete."
);
                return;
            }

            bool confirm = await Application.Current.MainPage.DisplayAlert("Delete confirmation", $"Are you sure you want to delete the intervention '{selectedIntervention.Name}'?", "YES", "NO"
);

            if (!confirm)
                return;

            Interventions.Remove(selectedIntervention);

            string path = "C:\\Users\\micha\\OneDrive\\Bureau\\Appli Barry Callebaut\\Gestion intervention\\Gestion intervention\\Configuration\\Datas\\Json\\Intervention.json";
            string json = JsonConvert.SerializeObject(Interventions, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(path, json);

            selectedIntervention = null;

            await alertService.ShowAlert("Intervention deleted", "The intervention has been successfully deleted."
);
        }

        [RelayCommand]
        public void ShowEditInterventionPopup()
        {
            if (selectedIntervention == null)
            {
                Application.Current.MainPage.DisplayAlert("No intervention selected", "Please select an intervention to edit.", "OK"
);
                return;
            }

            var popup = new AddInterventionPopup(this, selectedIntervention);
            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        [RelayCommand]
        public void ShowAddInterventionPopup()
        {
            var popup = new AddInterventionPopup(this);
            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        public void SaveInterventionsToFile()
        {
            // Utilise directement ton DataAccess
            dataAccess.UpdateAllIntervention(Interventions);
        }

        [RelayCommand]
        private void StartIntervention(Intervention it)
        {
            if (it == null || it.StartTime.HasValue) return;
            it.StartTime = DateTime.Now;
            SaveInterventionsToFile();
        }

        [RelayCommand]
        private void EndIntervention(Intervention it)
        {
            if (it == null || !it.StartTime.HasValue || it.EndTime.HasValue) return;
            it.EndTime = DateTime.Now;
            SaveInterventionsToFile();
        }


    }
}






























































































































