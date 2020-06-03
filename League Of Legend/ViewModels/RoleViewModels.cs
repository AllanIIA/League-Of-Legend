using LeagueOfLegend.DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace League_Of_Legend.ViewModels
{
    public class RoleViewModels
    {
        public Role Roles { get; set; }

        public List<SelectListItem> Region { get; set; }
        [Display(Name = "Identifiant")]
        public int Identifiant { get; set; }

        [Required(ErrorMessage = "Le Nom est requis")]
        public string Nom { get; set; }

    }
}
