using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.Links
{
    public class LinkRepository:ILinkRepository
    {
        public string ConnectionString { get; set; }

        public LinkRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public List<LinkModel> GetAllLinks() {

            // je connecte a Bdd
            MySqlConnection cnn = new MySqlConnection(ConnectionString);
            // J'ouvre la connection ! ****
            cnn.Open();

            // je cree une requette SQL
            string sql = @"
               SELECT 
                    l.idLinks, 
                    l.Title,
                    l.Description, 
                    l.Link
                FROM 
                    links l
                    LIMIT 25
                ";

            // executer la requette SQL donc  creer une comande avec l'objet MysqlComand
            // je cree une commande en passant sql(la requette) ,et la connection (cnn)
            MySqlCommand cmd=new MySqlCommand(sql,cnn);

            // je dois maintenant executer cette commande:
            var reader = cmd.ExecuteReader();
            var maListe=new List<LinkModel>();

            while (reader.Read())
            {
                var lelien = new LinkModel()
                {
                    IdLink = (int)reader["idLinks"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    URL = reader["Link"].ToString(),
                };
                maListe.Add(lelien);
            }
            // on ferme la connection !
            cnn.Close();
            // retour liste
            return maListe;
                   
        
        }
    }
}
