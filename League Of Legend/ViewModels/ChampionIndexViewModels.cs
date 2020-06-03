using LeagueOfLegend.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League_Of_Legend.ViewModels
{
    public class ChampionIndexViewModels
    {
        public string Title { get; set; }
        public List<Champion> Champions { get; set; }

        public List<Region> Regions { get; set; }

        public List<Role> Roles { get; set; }
    }
}
