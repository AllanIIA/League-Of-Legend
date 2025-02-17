﻿using LeagueOfLegend.DB.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfLegend.DB.DAL
{
    public class RegionContext
    {
        private string connectionString;

        public RegionContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Region> GetAll()
        {
            List<Region> regions = new List<Region>();

            using (MySqlConnection connection = new MySqlConnection())
            {

                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom FROM region;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Region r = new Region();
                    r.Identifiant = reader.GetInt32("Identifiant");
                    r.Nom = reader.GetString("Nom");
                    regions.Add(r);
                }

            }

            return regions;

        }
        /// <summary>
        /// Récupère une region en BDD 
        /// </summary>
        /// <param name="identifiant">L'identifiant de la region</param>
        /// <returns>La region correspondant à l'identifiant</returns>
        public Region Get(int identifiant)
        {
            Region r = null;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Identifiant, Nom FROM region WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    r = new Region();
                    r.Identifiant = reader.GetInt32("Identifiant");
                    r.Nom = reader.GetString("Nom");


                }

            }

            return r;
        }

        /// <summary>
        /// Insère une Region en BDD
        /// </summary>
        /// <param name="role">La region à insérer</param>
        /// <returns>un booléen indiquant si l'insertion s'est réalisée</returns>
        public bool Insert(Region region)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO region(Identifiant, Nom) VALUE(@Identifiant, @Nom)";

                command.Parameters.AddWithValue("Identifiant", region.Identifiant);
                command.Parameters.AddWithValue("Nom", region.Nom);



                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime une region en BDD
        /// </summary>
        /// <param name="identifiant">L'identifiant de la region à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(int identifiant)
        {
            int nbLignes = 0;
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM region WHERE Identifiant = @Identifiant;";

                command.Parameters.AddWithValue("Identifiant", identifiant);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        /// <summary>
        /// Supprime une region en BDD
        /// </summary>
        /// <param name="identifiant">L'objet region à supprimer</param>
        /// <returns>un booléen indiquant si la suppression s'est réalisée</returns>
        public bool Delete(Role role)
        {
            return Delete(role.Identifiant);
        }

        /// <summary>
        /// Modifie une region en BDD
        /// </summary>
        /// <param name="role">La region à insérer</param>
        /// <returns>un booléen indiquant si la modification s'est réalisée</returns>
        public bool Update(Region region)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE region SET Identifiant = @Identifiant, Nom = @Nom
                        WHERE Identifiant = @Identifiant
                    ";

                command.Parameters.AddWithValue("Identifiant", region.Identifiant);
                command.Parameters.AddWithValue("Nom", region.Nom);



                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }
    }
}
