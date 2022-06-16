using DTW_Repository.config;
using DTW_Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.Links
{
    public class LinkRepository:BaseRepository,ILinkRepository
        {
        //refactoring public string ConnectionString { get; set; }

        public LinkRepository(IConfiguration configuration):base(configuration)
        {
           // ConnectionString = configuration.GetConnectionString("DefaultConnection");
           //refactoring var builder = new MySqlConnectionStringBuilder(
            //configuration.GetConnectionString("DefaultConnection"));
            //builder.Password = configuration["DbPassword"];
            //builder.UserID = configuration["MyId"];
            //ConnectionString = builder.ConnectionString + ";";


        }
        public List<LinkModel> GetAllLinks() {

            // je connecte a Bdd

            var cnn = OpenConnection();
            
            
            // refactoring MySqlConnection cnn = new MySqlConnection(ConnectionString);



            // J'ouvre la connection ! ****
           // cnn.Open();
            


            // je cree une requette SQL
            string sql = @"
                SELECT 
                    l.idLinks, 
                    l.Title,
                    l.Description, 
                    l.Link,
                    u.idUser,
                    u.forename, 
                    u.surname,
                    u.mail
                FROM 
                    links l
                INNER JOIN users u ON l.idAuteur = u.idUser
                ";
            
            //----------------- end if


            // executer la requette SQL donc  creer une comande avec l'objet MysqlComand
            // je cree une commande en passant sql(la requette) ,et la connection (cnn)
            MySqlCommand cmd=new MySqlCommand(sql,cnn);

            // je dois maintenant executer cette commande:
            var reader = cmd.ExecuteReader();
            var maListe=new List<LinkModel>();

            while (reader.Read())
            {
                UserModel auteur = new UserModel(
                    Convert.ToInt32(reader["idUser"]),
                    reader["forename"].ToString(),
                    reader["surname"].ToString(),
                    reader["mail"].ToString());

                var lelien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["Title"].ToString(),
                    reader["Description"].ToString(),
                    reader["Link"].ToString(),
                    auteur
                );
                maListe.Add(lelien);
            }
            // on ferme la connection !
            cnn.Close();
            // retour liste
            return maListe;
                   
        }

        //*********************************************************



        public LinkModel GetLink(int id)
        {
            //je me connecte à la bdd
            var cnn = OpenConnection(); 


            //Je crée une requête sql

            string sql = @"
                SELECT 
                    l.idLinks, 
                    l.Title,
                    l.Description, 
                    l.Link,
                    u.idUser,
                    u.forename, 
                    u.surname,
                    u.mail
                FROM 
                    links l
                INNER JOIN users u ON l.idAuteur = u.idUser
                WHERE l.idLinks = @idLink
                ";

            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@idLink", id);

            var reader = cmd.ExecuteReader();
            LinkModel monLien = null;
            //Récupérer le retour, et le transformer en objet
            if (reader.Read())
            {
                UserModel auteur = new UserModel(
                    Convert.ToInt32(reader["idUser"]),
                    reader["forename"].ToString(),
                    reader["surname"].ToString(),
                    reader["mail"].ToString());

                monLien = new LinkModel(
                    Convert.ToInt32(reader["idLinks"]),
                    reader["Title"].ToString(),
                    reader["Description"].ToString(),
                    reader["Link"].ToString(),
                    auteur
                );
            }

            cnn.Close();
            return monLien;



        }

        //*********************************************************

        public bool EditLink(LinkModel link)
        {
            try
            {
                //je me connecte à la bdd via BaseRepository herité
                var cnn = OpenConnection();

                //Je crée une requête sql
                string sql = @"
                UPDATE links
                SET 
                    Title = @title,
                    Description = @description,
                    Link = @link,
                    idAuteur = @idUser
                WHERE 
                    IdLinks = @idLink
                ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@title", link.Title);
                cmd.Parameters.AddWithValue("@description", link.Description);
                cmd.Parameters.AddWithValue("@link", link.URL);
                cmd.Parameters.AddWithValue("@idUser", link.Auteur.IdUser);
                cmd.Parameters.AddWithValue("@idLink", link.IdLink);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteLink(int id)
        {
            try
            {
                //je me connecte à la bdd via BaseRepository herité
                var cnn = OpenConnection();

                string sql = @"
                DELETE FROM 
                links
                WHERE 
                    IdLinks = @idLink
                ";

                //Executer la requête sql, donc créer une commande
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idLink", id);

                var nbRowEdited = cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
