using Gestion_intervention.Model.Gestion_intervention.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_intervention.Utilities.Interfaces
{
    public interface IDataAccess
    {
        string AccessPath
        {
            get;
            set;
        }

        InterventionCollection GetAllIntervention();
        void UpdateAllIntervention(InterventionCollection interventions);
    }
}
