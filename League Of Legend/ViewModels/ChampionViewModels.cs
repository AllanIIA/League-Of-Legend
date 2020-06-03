using LeagueOfLegend.DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace League_Of_Legend.ViewModels
{
    public class ChampionViewModels
    {
        public Champion Champions { get; set; }

        public List<SelectListItem> Region { get; set; }
        [Display(Name = "Identifiant")]
        public int Identifiant { get; set; }

        [Required(ErrorMessage = "Le Nom est requis")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le Surnom est requis")]
        public string Surnom { get; set; }

        [Range(1,14, ErrorMessage = "La valeur doit être comprise entre 1 et 14")]
        [Display(Name = "Région")]
        public int IdentifiantRegion { get; set; }
        [Range(1,6, ErrorMessage = "La valeur doit être comprise entre 1 et 6")]
        [Display(Name = "Rôle")]
        public int IdentifiantRole { get; set; }
    }

}
