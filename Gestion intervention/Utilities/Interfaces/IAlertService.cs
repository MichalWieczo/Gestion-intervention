using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_intervention.Utilities.Interfaces
{
    public interface IAlertService
    {
        Task ShowAlert(string title, string message);

        Task<bool> ShowConfirmation(string title, string message);
        Task<string> ShowQuestion(string title, params string[] button);
    }
}
