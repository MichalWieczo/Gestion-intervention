using CommunityToolkit.Mvvm.ComponentModel;
using Gestion_intervention.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_intervention.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        public BaseViewModel(IAlertService alertService)
        {
            this.alertService = alertService;
        }
        protected IAlertService alertService;
    }
}
