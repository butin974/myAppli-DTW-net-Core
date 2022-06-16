using DTW_Repository.config;
using DTW_Repository.User;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.User
{
    public class UserRepository:BaseRepository,IUserRepository
    {
        //public string ConnectionString { get; set; }

        public UserRepository(IConfiguration configuration):base(configuration)
        {
           // ConnectionString = configuration.GetConnectionString("DefaultConnection");
            //var builder = new MySqlConnectionStringBuilder(
            //configuration.GetConnectionString("DefaultConnection"));
            //builder.Password = configuration["DbPassword"];
            //builder.UserID = configuration["MyId"];
            //ConnectionString = builder.ConnectionString + ";";
        }
        public List<UserModel> GetAllUsers() {
            // je connecte a Bdd
            // MySqlConnection cnn = new MySqlConnection(ConnectionString);
            // J'ouvre la connection ! ****
            var cnn=OpenConnection();

            // je cree une requette SQL
            string sql = @"
                SELECT 
                    u.idUser,
                    u.forename, 
                    u.surname,
                    u.mail
                FROM 
                    users u
            ";
            // executer la requette SQL donc  creer une comande avec l'objet MysqlComand
            // je cree une commande en passant sql(la requette) ,et la connection (cnn)
            MySqlCommand cmd=new MySqlCommand(sql,cnn);
            // je dois maintenant executer cette commande:
            var reader = cmd.ExecuteReader();
            var maListe=new List<UserModel>();
            while (reader.Read())
            {
                UserModel auteur = new UserModel(
                    Convert.ToInt32(reader["idUser"]),
                    reader["forename"].ToString(),
                    reader["surname"].ToString(),
                    reader["mail"].ToString()) ;
               
                maListe.Add(auteur);
            }
            // on ferme la connection !
            cnn.Close();
            // retour liste
            return maListe;
         }
          //*********************************************************

    }
}
