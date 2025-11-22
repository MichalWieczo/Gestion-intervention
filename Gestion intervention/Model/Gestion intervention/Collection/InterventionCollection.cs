using Gestion_intervention.Model.Gestion_intervention.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_intervention.Model.Gestion_intervention.Collection
{
    public class InterventionCollection : ObservableCollection<Intervention>
    {
        public InterventionCollection() { }

        /// <summary>
        /// Ajoute une intervention si son Id n'existe pas déjà.
        /// Retourne true si l'ajout a été effectué, false sinon.
        /// </summary>
        public bool AddIntervention(Intervention it)
        {
            if (it == null)
                return false;

            // Vérifie que l'ID n'existe pas déjà dans la collection
            if (!this.Any(i => i.Id == it.Id))
            {
                this.Add(it);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Supprime une intervention existante.
        /// Retourne true si la suppression a réussi, false sinon.
        /// </summary>
        public bool DeleteIntervention(Intervention it)
        {
            if (it == null)
                return false;

            // Recherche par Id si nécessaire
            var existing = this.FirstOrDefault(i => i.Id == it.Id);
            if (existing != null)
            {
                return this.Remove(existing);
            }

            return false;
        }

        /// <summary>
        /// Renvoie le prochain Id disponible.
        /// Si la collection est vide, retourne 1.
        /// </summary>
        public int GetNextId()
        {
            return this.Count >= 1 ? this.Max(sm => sm.Id) + 1 : 1;
        }
    }
}
