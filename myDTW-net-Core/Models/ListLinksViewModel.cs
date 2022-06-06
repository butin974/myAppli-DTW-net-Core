using DTW_Repository.Links;
using System;
using System.Collections.Generic;

namespace myDTW_net_Core.Models
{
    public class ListLinksViewModel
    {
        public int PerPage { get; set; }// nombre par page
        public int NbPage { get; set; } // no de page
        public int NbLinksTotalBdd { get; set; }// nombre d'elements en bdd
        public int NbPageTotal //calcul du nombre de pages
        {
            get
            {
                return Convert.ToInt16(Math.Ceiling(((decimal)NbLinksTotalBdd / PerPage)));
            }
        }
        // liste de liens
        public List<LinkModel> LstLinks{get;set;}

        public string Recherche { get; set; }
     
    }
}
