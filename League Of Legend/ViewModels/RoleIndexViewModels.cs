using LeagueOfLegend.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League_Of_Legend.ViewModels
{
    public class RoleIndexViewModels
    {
        public string Title { get; set; }
        public List<Role> Roles { get; set; }
    }
}
