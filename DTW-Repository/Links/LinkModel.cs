using DTW_Repository.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.Links
{
    public class LinkModel
    {
        // Idlink etant en lecture seule on cree un constructeur a la classe
        public LinkModel(int idLink, string title, string description, string uRL, UserModel auteur)
        {
            IdLink = idLink;
            Title = title;
            Description = description;
            URL = uRL;
            Auteur = auteur;
        }


        // modele de domaine pour la bdd
        public int IdLink { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public UserModel Auteur { get; set; }

        //TODO : auteur -> Ajouter en objet... a voir plus tard
    }
}
