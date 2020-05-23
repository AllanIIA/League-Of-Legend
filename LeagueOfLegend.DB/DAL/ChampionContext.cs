using LeagueOfLegend.DB.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.DAL
{
    class ChampionContext
    {
        private string connectionString;

        public ChampionContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Champion> GetAll()
        {
            List<Champion> champions = new List<Champion>();

            using (MySqlConnection connection = new MySqlConnection())
            {

                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom, Surnom, identifiantRole, identifiantRegion FROM Champion;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Champion c = new Champion();
                    c.Identifiant = reader.GetInt32("Identifiant");
                    c.Nom = reader.GetString("Nom");
                    c.Surnom = reader.GetString("Surnom");
                    c.IdentifiantRole = reader.GetInt16("IdentifiantRole");
                    c.IdentifiantRegion = reader.GetInt16("IdentifiantRegion");

                    champions.Add(c);
                }

            }

            return champions;
        }

        /// <summary>
        /// Récupère une personne en BDD 
        /// </summary>
        /// <param name="identifiant">L'identifiant du champion</param>
        /// <returns>Le champion correspondant à l'identifiant</returns>
        public Champion Get(int identifiant)
        {
            Champion c = null;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom, Surnom, identifiantRole, identifiantRegion FROM Champion WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    c = new Champion();
                    c.Identifiant = reader.GetInt32("Identifiant");
                    c.Nom = reader.GetString("Nom");
                    c.Surnom = reader.GetString("Surnom");
                    c.IdentifiantRole = reader.GetInt16("IdentifiantRole");
                    c.IdentifiantRegion = reader.GetInt16("IdentifiantRegion");
                }

            }

            return c;
        }

        /// <summary>
        /// Insère un champion en BDD
        /// </summary>
        /// <param name="champion">Le champion à insérer</param>
        /// <returns>un booléen indiquant si l'insertion s'est réalisée</returns>
        public bool Add(Champion champion)
        {
            return true;
        }

        /// <summary>
        /// Supprime un champion en BDD
        /// </summary>
        /// <param name="identifiant">L'identifiant du champion à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Remove(int identifiant)
        {
            int nbLignes = 0;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Champion WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes == 1;
        }

        /// <summary>
        /// Supprime un champion en BDD
        /// </summary>
        /// <param name="identifiant">L'objet Champion à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Remove(Champion champion)
        {
            return Remove(champion.Identifiant);
        }

        /// <summary>
        /// Modifie un champion en BDD
        /// </summary>
        /// <param name="champion">Le champion à insérer</param>
        /// <returns>un booléen indiquant si la modification s'est réalisée</returns>
        public bool Modify(Champion champion)
        {
            return true;
        }
    }
}
