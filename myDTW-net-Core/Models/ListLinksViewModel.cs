using DTW_Repository.Links;
using System.Collections.Generic;

namespace myDTW_net_Core.Models
{
    public class ListLinksViewModel
    {


        public int PerPage { get; set; }
        public int NbPage { get; set; }


        // liste de liens
        public List<LinkModel> LstLinks{get;set;}
        
    }




}
