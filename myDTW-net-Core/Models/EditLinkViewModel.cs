using DTW_Repository.Links;
using DTW_Repository.User;
using System.Collections.Generic;

namespace LiveCSharpDTW052022.Models
{
    public class EditLinkViewModel
    {
        public LinkModel monLien { get; set; }
        public List<UserModel> lstUsers { get; set; }
    }
}

