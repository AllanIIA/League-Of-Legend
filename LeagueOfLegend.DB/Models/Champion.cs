using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.Models
{
    public class Champion
    {
        
        public int Identifiant { get; set; }
        public string Nom { get; set; }
        public string Surnom { get; set; }
        public int IdentifiantRole { get; set; }
        public int IdentifiantRegion{ get; set; }
    }
}
