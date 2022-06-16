using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.config
{
    //Cette classe va contenir les fonctions de connection a la BDD
    //On va pouvoir Heriter de cette classe
    public class BaseRepository:IBaseRepository

    {
      //on a besoin de ma chaine de connection..
      // ( elle est dans le appsettings.json )

        public string ConnectionString { get; set; }

            // a la construction de mon BaseRepository. je veux recuperer la connection qui est dans l'interface
            // qui s'appelle : Iconfiguration !
            // Iconfiguration fait partie d'un paquet qui s'appelle :
            // "Microsoft.Extentions.Configuration.Abstraction" que l'on appelle un "Pakage-Nugget"
            // ( c'est une dependance ,une sorte de "npm" )
        public BaseRepository(IConfiguration configuration) 
        {
            //ConnectionString = configuration.GetConnectionString("DefaultConnection");//
            
            var builder=new MySqlConnectionStringBuilder(
                configuration.GetConnectionString("DefaultConnection"));
                builder.UserID = configuration["MyId"];
                builder.Password = configuration["DbPassword"];
                ConnectionString = builder.ConnectionString + ";";
        }
     
        public MySqlConnection OpenConnection()
        {
            //je peux avoir ma chaine de connection
            //connectionString="server=mysql-devtechwatch.alwaysdata.net;database=devtechwatch_dtw; uid=267041;pwd=NYV8e@6u4$j4Wc7Eh;"
            try
            { 
            MySqlConnection cnn = new MySqlConnection(ConnectionString);
                cnn.Open();
                //j'ouvre la connection et je la retourne'
                return cnn;
             }
            catch(Exception ex)
            {
                throw new Exception("Impossible de se connecter a la BDD");
            }

        }
    }
}
