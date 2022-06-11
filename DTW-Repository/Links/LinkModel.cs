using DTW_Repository.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTW_Repository.Links
{
    public class LinkModel
    {
        public LinkModel()
        {
          // surcharge du constructeur pour passer dans linkcontroler/linkaction
        }
     
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
        public int IdLink { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string URL { get; set; }
        public UserModel Auteur { get; set; }
 
       
    }
}
