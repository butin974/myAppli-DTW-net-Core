using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.Links
{
    public interface ILinkRepository

    {
        public List<LinkModel> GetAllLinks();

        public LinkModel GetLink(int id);


        // on lui demande de modifier le lien et de retourner un Boolean
        // declaration a l'interface
        public bool EditLink(LinkModel link);

    }
}
