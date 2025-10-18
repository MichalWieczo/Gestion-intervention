using CommunityToolkit.Maui.Views;
using Gestion_intervention.Model.Gestion_intervention.Entities;
using Gestion_intervention.ViewModel;

namespace Gestion_intervention.View;

public partial class AddInterventionPopup : Popup
{
    private readonly MainPageViewModel _mainPageViewModel;

    // Afficher ou non la section Début/Fin (true en édition)
    public bool ShowTimes { get; private set; }

    // Modèle édité/ajouté
    public Intervention InterventionPopupDisplayed { get; private set; }

    // ==== Proxies pour DatePicker/TimePicker ====
    public DateTime StartDate
    {
        get => (DateTime)GetValue(StartDateProperty);
        set => SetValue(StartDateProperty, value);
    }
    public TimeSpan StartClock
    {
        get => (TimeSpan)GetValue(StartClockProperty);
        set => SetValue(StartClockProperty, value);
    }
    public DateTime EndDate
    {
        get => (DateTime)GetValue(EndDateProperty);
        set => SetValue(EndDateProperty, value);
    }
    public TimeSpan EndClock
    {
        get => (TimeSpan)GetValue(EndClockProperty);
        set => SetValue(EndClockProperty, value);
    }

    public static readonly BindableProperty StartDateProperty =
        BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(AddInterventionPopup), DateTime.Today);

    public static readonly BindableProperty StartClockProperty =
        BindableProperty.Create(nameof(StartClock), typeof(TimeSpan), typeof(AddInterventionPopup), TimeSpan.Zero);

    public static readonly BindableProperty EndDateProperty =
        BindableProperty.Create(nameof(EndDate), typeof(DateTime), typeof(AddInterventionPopup), DateTime.Today);

    public static readonly BindableProperty EndClockProperty =
        BindableProperty.Create(nameof(EndClock), typeof(TimeSpan), typeof(AddInterventionPopup), TimeSpan.Zero);
    // ============================================

    public AddInterventionPopup(MainPageViewModel mainPageVM, Intervention? interventionToEdit = null)
    {
        _mainPageViewModel = mainPageVM;
        ShowTimes = interventionToEdit != null;

        var nextId = _mainPageViewModel?.Interventions?.GetNextId() ?? 1;

        // Créer le modèle (nouveau ou copie pour édition)
        InterventionPopupDisplayed = interventionToEdit != null
            ? new Intervention(
                interventionToEdit.Id,
                interventionToEdit.Name,
                interventionToEdit.StartTime,
                interventionToEdit.EndTime,
                interventionToEdit.CategoryType,
                interventionToEdit.Problem,
                interventionToEdit.Cause,
                interventionToEdit.Solution,
                interventionToEdit.Description)
            : new Intervention(
                id: nextId,
                name: "",
                startTime: null,
                endTime: null,
                categoryType: Intervention.Category.EM,
                problem: Intervention.ProblemCode.Unknown,
                cause: Intervention.CauseCode.Unknown,
                solution: Intervention.SolutionCode.Unknown,
                description: ""
              );

        // Initialiser les proxies (valeurs par défaut si null)
        var st = InterventionPopupDisplayed.StartTime ?? DateTime.Today;
        StartDate = st.Date;
        StartClock = st.TimeOfDay;

        var et = InterventionPopupDisplayed.EndTime ?? DateTime.Today;
        EndDate = et.Date;
        EndClock = et.TimeOfDay;

        InitializeComponent();

        // BindingContext = modèle
        BindingContext = InterventionPopupDisplayed;

        // Alimentation des pickers enum
        categoryPicker.ItemsSource = Enum.GetValues(typeof(Intervention.Category));
        categoryPicker.SelectedItem = InterventionPopupDisplayed.CategoryType;

        problemPicker.ItemsSource = Enum.GetValues(typeof(Intervention.ProblemCode));
        problemPicker.SelectedItem = InterventionPopupDisplayed.Problem;

        causePicker.ItemsSource = Enum.GetValues(typeof(Intervention.CauseCode));
        causePicker.SelectedItem = InterventionPopupDisplayed.Cause;

        solutionPicker.ItemsSource = Enum.GetValues(typeof(Intervention.SolutionCode));
        solutionPicker.SelectedItem = InterventionPopupDisplayed.Solution;
    }

    private void OnCancelClicked(object sender, EventArgs e) => Close(null);

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!InterventionPopupDisplayed.isValid(out var msg))
        {
            await Application.Current.MainPage.DisplayAlert("Validation", msg, "OK");
            return;
        }

        // Si on modifie et que la section est visible, appliquer les changements
        if (ShowTimes)
        {
            InterventionPopupDisplayed.StartTime = StartDate.Date + StartClock;
            InterventionPopupDisplayed.EndTime = EndDate.Date + EndClock;
        }

        var existing = _mainPageViewModel.Interventions
            .FirstOrDefault(i => i.Id == InterventionPopupDisplayed.Id);

        if (existing == null)
        {
            _mainPageViewModel.Interventions.Add(InterventionPopupDisplayed);
        }
        else
        {
            existing.Name = InterventionPopupDisplayed.Name;
            existing.StartTime = InterventionPopupDisplayed.StartTime;
            existing.EndTime = InterventionPopupDisplayed.EndTime;
            existing.CategoryType = InterventionPopupDisplayed.CategoryType;
            existing.Problem = InterventionPopupDisplayed.Problem;
            existing.Cause = InterventionPopupDisplayed.Cause;
            existing.Solution = InterventionPopupDisplayed.Solution;
            existing.Description = InterventionPopupDisplayed.Description;
        }

        try
        {
            _mainPageViewModel.SaveInterventionsToFile();
            await Application.Current.MainPage.DisplayAlert("Save", "Intervention saved successfully.", "OK");
            Close(InterventionPopupDisplayed);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Save failed: {ex.Message}", "OK");
        }
    }
}
