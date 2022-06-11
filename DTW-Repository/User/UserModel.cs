using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.User
{
   
    public class UserModel
    {
        //CONSTRUCTEUR

        public UserModel()
        {
            // surcharge du constructeur pour passer dans linkcontroler/linkaction
        }



        public UserModel(int idUser, string userForeName, string userSurName, string userEmail)
        {
            IdUser = idUser;
            UserForeName = userForeName;
            UserSurName = userSurName;
            UserEmail = userEmail;
        }

        public int IdUser { get; set; }
        public string UserForeName { get; set; }
        public string UserSurName { get; set; }
        public string UserEmail { get; set; }

    }
}
