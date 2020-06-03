using LeagueOfLegend.DB.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.DAL
{
    public class RegionContext
    {
            private String connectionString;
            public RegionContext(string connectionString)
            {
                this.connectionString = connectionString;
            }


            public List<Region> GetAll()
            {
                List<Region> regions = new List<Region>();
                using (MySqlConnection c = new MySqlConnection(connectionString))
                {
                    c.Open();
                    MySqlCommand command = c.CreateCommand();
                    command.CommandText = "SELECT Identifiant, Nom FROM Region ORDER BY identifiant";

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Region r = new Region();

                        r.Identifiant = reader.GetInt16("Identifiant");
                        r.Nom = reader.GetString("Nom");

                        regions.Add(r);
                    }
                    return regions;
                }
            }

            public bool Insert(Region region)
            {
                return true;
            }

            public bool Update(Region region)
            {
                return true;
            }

            public bool Delete(int id)
            {
                return true;
            }
        }
}
