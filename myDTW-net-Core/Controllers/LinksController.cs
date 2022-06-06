using DTW_Repository.Links;
using Microsoft.AspNetCore.Mvc;
using myDTW_net_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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



        //ce  controleur est appelé par une route :
        //https://localhost:44386/Links/index?perPage=20@nbPage=2
        // /links/index
        public IActionResult Index(int perPage=12,int nbPage=1, string search = "")
        {

            //Je recupere la totalité des liens en BDD
            // via 'GetAllLinks' de DTW-repository/LinkRepository
            // qui ouvre la connection,execute la requette sql et la transforme en objet


            var allLinks =_linkRepository.GetAllLinks();

            // test si recherche
            // la recherche se fait sur la reponse totale de la bdd
            // (a eviter sur de trops grosses bases)

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                // la recherche se fait dans le titre ou la description
                allLinks = allLinks.Where(link =>
                        link.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                        link.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
            }


            // je recupere dans mon view modele
            int nbLinkTotal=allLinks.Count();
            
            //Faire ma pagination
            // LINQ : Skip pour passer un certain nombre d'éléments
            // LINQ : Take pour prendre un certain nombre d'éléments
            allLinks = allLinks.Skip(perPage*(nbPage-1))
                               .Take(perPage)
                               .ToList();

         
         
            //
            var vm = new ListLinksViewModel()
            {
                // appel de la methode via interface/injection
                LstLinks = allLinks,
                //LstLinks = _linkRepository.GetAllLinks()

                NbLinksTotalBdd = nbLinkTotal,
                NbPage = nbPage,
                PerPage = perPage,
                Recherche = search
            };
            // Je l'envoie a ma vue
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
