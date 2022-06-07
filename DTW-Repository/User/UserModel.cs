using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.User
{
   
    public class UserModel
    {
        //CONSTRUCTEUR
        public UserModel(int idUser, string userForeName, string userSurName, string userEmail)
        {
            IdUser = idUser;
            UserForeName = userForeName;
            UserSurName = userSurName;
            UserEmail = userEmail;
        }

        public int IdUser { get; }
        public string UserForeName { get; }
        public string UserSurName { get; }
        public string UserEmail { get; }

    }
}
