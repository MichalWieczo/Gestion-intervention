using Gestion_intervention.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_intervention.Utilities.Services
{
    public class AlertServiceDisplay : IAlertService
    {
        /// <summary>
        /// Show alert with a pop-up display with just a title and a message
        /// </summary>
        public async Task ShowAlert(string title, string message)
        {
            // var currentPage = GetCurrentPage();
            // {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");

            // }
        }

        /// <summary>
        /// Show alert with a pop-up display with a confirmation question Yes or No
        /// </summary>
        public async Task<bool> ShowConfirmation(string title, string message)
        {
            return await Shell.Current.DisplayAlert(title, message, "Yes", "No");
        }

        /// <summary>
        /// Show alert with a pop-up display with a confirmation with personalized text accept and cancel
        /// </summary>
        public async Task<bool> ShowConfirmation(string title, string message, string accept, string cancel)
        {
            return await Shell.Current.DisplayAlert(title, message, accept, cancel);
        }

        /// <summary>
        /// Show alert with a pop-up display with a list of buttons (multiple choices)
        /// </summary>
        public async Task<string> ShowQuestion(string title, params string[] buttons)
        {
            return await Shell.Current.DisplayActionSheet(title, "Cancel", null, buttons);
        }

        /// <summary>
        /// Show alert with a pop-up display with an entry box
        /// </summary>
        public async Task<string> ShowPrompt(string title, string message)
        {
            return await Shell.Current.DisplayPromptAsync(title, message);
        }
    }
}
