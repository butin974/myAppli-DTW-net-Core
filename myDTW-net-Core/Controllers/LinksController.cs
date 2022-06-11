using DTW_Repository.Links;
using DTW_Repository.User;
using LiveCSharpDTW052022.Models;
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
        private readonly IUserRepository _userRepository;

        public LinksController(ILinkRepository linkRepository ,IUserRepository userRepository)
            // des qu'on instancie un link controleur ,on recupere link repository
            // via interface et l'injection de dependances
        {
            _linkRepository = linkRepository;
            _userRepository = userRepository;
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

        public IActionResult EditLinkPage(int idLink)
        {
            //Je récupère mon Lien
            var leLien = _linkRepository.GetLink(idLink);

            if (leLien == null)
            {
                return NotFound();
            }
            else
            {
                //transformer le modèle de domaine en modèle de vue

                EditLinkViewModel vm = new EditLinkViewModel()
                {
                    monLien = leLien,
                    // on implemente la liste des users
                    lstUsers=_userRepository.GetAllUsers()

                };
                return View(vm);
            }
        }


        [HttpPost]// cette methone ne sera accessible que depuis un Post (eviter le get  pour securité)
        public IActionResult EditLinkAction(EditLinkViewModel vm)
        {
            // quand on arrive dans notre controleur ,on recoit le lien deja modifié
            // nous voulons une methode qui envoie un linkmodele et qui va modifier le lien

            if (!ModelState.IsValid)
            {   // il faut recreer la liste des utilisateurs
                vm.lstUsers = _userRepository.GetAllUsers();
                // et renvoyer le vm 
                return View("EditLinkPage", vm);
            }

            // j'appelle la methode depuis le controleur
            bool isOk = _linkRepository.EditLink(vm.monLien);
            if (isOk)   
            {   // Je renvoie sur la page index
                return RedirectToAction("Index");
            }
            else
            {
                // il faut recreer la liste des utilisateurs
                vm.lstUsers = _userRepository.GetAllUsers();
                // sinon je le renvoie vers la vue en lui donnant le modele de la vue
                // vm.lstUsers = _userRepository.GetAllUsers();
                return View("EditLinkPage", vm);
            } 
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
