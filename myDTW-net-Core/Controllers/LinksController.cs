using DTW_Repository.Links;
using Microsoft.AspNetCore.Mvc;
using myDTW_net_Core.Models;
using System.Collections.Generic;

namespace myDTW_net_Core.Controllers
{
    public class LinksController : Controller
    {
        // todo recuperer le modele depuis le repository
        
        private readonly ILinkRepository _linkRepository;
        public LinksController(ILinkRepository linkRepository)
            // des qu'on instancie un link controleur ,on recupere link repository
            // via interface et l'injection de dependances
        {
            _linkRepository = linkRepository;
        }

        public IActionResult Index()
        {
            var vm = new ListLinksViewModel()
            {
                LstLinks = _linkRepository.GetAllLinks()
                // appel de la methode via interface/injection
            };
            return View(vm);
        }
    }
}


    

        /*public IActionResult Index()
        {
            var vm = new ListLinksViewModel()
            {
             LstLinks=  new List<LinkModel>
                {
                    new LinkModel()
                    {
                      IdLink=1,
                      Title="test1",
                      URL="https://picsum.photos/151",
                      Description="Lorem ipsum dolor sit amet, sollicitudin feugiat lorem mattis. Morbi pharetra nibh sed justo vulputate tempus. Integer vel consectetur nunc. Cras."
                    },

                     new LinkModel()
                    {
                      IdLink=1,
                      Title="test2",
                      URL="https://picsum.photos/152",
                      Description="Lorem ipsum dolor sit amet, sollicitudin feugiat lorem mattis. Morbi pharetra nibh sed justo vulputate tempus. Integer vel consectetur nunc. Cras."
                    },

                     new LinkModel()
                    {
                      IdLink=1,
                      Title="test3",
                      URL="https://picsum.photos/150",
                      Description="Lorem ipsum dolor sit amet, sollicitudin feugiat lorem mattis. Morbi pharetra nibh sed justo vulputate tempus. Integer vel consectetur nunc. Cras."
                    },
                }
            };
             return View(vm);
        }*/
