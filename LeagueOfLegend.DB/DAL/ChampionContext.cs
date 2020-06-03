using LeagueOfLegend.DB.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.DAL
{
    public class ChampionContext
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
                command.CommandText = "SELECT Identifiant, Nom, Surnom, identifiantRegion, identifiantRole  FROM champion;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Champion c = new Champion();
                    c.Identifiant = reader.GetInt32("Identifiant");
                    c.Nom = reader.GetString("Nom");
                    c.Surnom = reader.GetString("Surnom");
                    c.IdentifiantRegion = reader.GetInt16("IdentifiantRegion");
                    c.IdentifiantRole = reader.GetInt16("IdentifiantRole");
                    

                    champions.Add(c);
                }

            }

            return champions;
        }

        /// <summary>
        /// Récupère un Champion en BDD 
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
                command.CommandText = "SELECT Identifiant, Nom, Surnom, identifiantRegion, identifiantRole FROM champion WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    c = new Champion();
                    c.Identifiant = reader.GetInt32("Identifiant");
                    c.Nom = reader.GetString("Nom");
                    c.Surnom = reader.GetString("Surnom");
                    c.IdentifiantRegion = reader.GetInt16("IdentifiantRegion");
                    c.IdentifiantRole = reader.GetInt16("IdentifiantRole");
                    
                }

            }

            return c;
        }

        /// <summary>
        /// Insère un champion en BDD
        /// </summary>
        /// <param name="champion">Le champion à insérer</param>
        /// <returns>un booléen indiquant si l'insertion s'est réalisée</returns>
        public bool Insert(Champion champion)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO champion(Identifiant, Nom, Surnom, IdentifiantRegion, IdentifiantRole) VALUE(@Identifiant, @Nom, @Surnom, @IdentifiantRegion, @IdentifiantRole)";

                command.Parameters.AddWithValue("Identifiant", champion.Identifiant);
                command.Parameters.AddWithValue("Nom", champion.Nom);
                command.Parameters.AddWithValue("Surnom", champion.Surnom);
                command.Parameters.AddWithValue("IdentifiantRole", champion.IdentifiantRole);
                command.Parameters.AddWithValue("IdentifiantRegion", champion.IdentifiantRegion);


                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime un champion en BDD
        /// </summary>
        /// <param name="identifiant">L'identifiant du champion à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(int identifiant)
        {
            int nbLignes = 0;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM champion WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime un champion en BDD
        /// </summary>
        /// <param name="identifiant">L'objet Champion à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(Champion champion)
        {
            return Delete(champion.Identifiant);
        }

        /// <summary>
        /// Modifie un champion en BDD
        /// </summary>
        /// <param name="champion">Le champion à insérer</param>
        /// <returns>un booléen indiquant si la modification s'est réalisée</returns>
        public bool Update(Champion champion)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE champion SET Identifiant = @Identifiant, Nom = @Nom, Surnom = @Surnom,  IdentifiantRegion = @IdentifiantRegion, IdentifiantRole = @IdentifiantRole
                        WHERE Identifiant = @Identifiant
                    ";

                command.Parameters.AddWithValue("Identifiant", champion.Identifiant);
                command.Parameters.AddWithValue("Nom", champion.Nom);
                command.Parameters.AddWithValue("Surnom", champion.Surnom);
                command.Parameters.AddWithValue("IdentifiantRegion", champion.IdentifiantRegion);
                command.Parameters.AddWithValue("IdentifiantRole", champion.IdentifiantRole);
                

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }
    }
}
