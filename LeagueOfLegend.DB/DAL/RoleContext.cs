using LeagueOfLegend.DB.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.DAL
{
    public class RoleContext
    {
        private string connectionString;

        public RoleContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();

            using (MySqlConnection connection = new MySqlConnection())
            {

                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom FROM role;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Role r = new Role();
                    r.Identifiant = reader.GetInt32("Identifiant");
                    r.Nom = reader.GetString("Nom");
                    roles.Add(r);
                }

            }

            return roles;

        }
        /// <summary>
        /// Récupère un rôle en BDD 
        /// </summary>
        /// <param name="identifiant">L'identifiant du rôle</param>
        /// <returns>Le rôle correspondant à l'identifiant</returns>
        public Role Get(int identifiant)
        {
            Role r = null;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom FROM role WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    r = new Role();
                    r.Identifiant = reader.GetInt32("Identifiant");
                    r.Nom = reader.GetString("Nom");
                 

                }

            }

            return r;
        }

        /// <summary>
        /// Insère un Rôle en BDD
        /// </summary>
        /// <param name="role">Le rôle à insérer</param>
        /// <returns>un booléen indiquant si l'insertion s'est réalisée</returns>
        public bool Insert(Role role)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO role(Identifiant, Nom) VALUE(@Identifiant, @Nom)";

                command.Parameters.AddWithValue("Identifiant", role.Identifiant);
                command.Parameters.AddWithValue("Nom", role.Nom);
              


                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime un rôle en BDD
        /// </summary>
        /// <param name="identifiant">L'identifiant du rôle à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(int identifiant)
        {
            int nbLignes = 0;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM role WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime un rôle en BDD
        /// </summary>
        /// <param name="identifiant">L'objet Rôle à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(Role role)
        {
            return Delete(role.Identifiant);
        }

        /// <summary>
        /// Modifie un rôle en BDD
        /// </summary>
        /// <param name="role">Le rôle à insérer</param>
        /// <returns>un booléen indiquant si la modification s'est réalisée</returns>
        public bool Update(Role role)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE role SET Identifiant = @Identifiant, Nom = @Nom
                        WHERE Identifiant = @Identifiant
                    ";

                command.Parameters.AddWithValue("Identifiant", role.Identifiant);
                command.Parameters.AddWithValue("Nom", role.Nom);
               


                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }
    }
}
