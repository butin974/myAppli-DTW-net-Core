using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.Links
{
    public interface ILinkRepository

    {
        public List<LinkModel> GetAllLinks();

        public LinkModel GetLink(int id);

    }
}
