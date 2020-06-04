using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League_Of_Legend.ViewModels;
using LeagueOfLegend.DB.DAL;
using LeagueOfLegend.DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace League_Of_Legend.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILogger<RegionController> _logger;
        private String connectionString;
        public RegionController(ILogger<RegionController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            connectionString = configRoot["ConnectionString:DefaultConnection"];
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Régions";

            RegionIndexViewModels model = new RegionIndexViewModels();
            model.Title = "Les Régions sont :";

            RegionContext regionContext = new RegionContext(connectionString);
            List<Region> regions = regionContext.GetAll();
            model.Regions = regions;

            return View(model);
        }



        private List<SelectListItem> regions()
        {
            RegionContext regionContext = new RegionContext(connectionString);

            List<Region> regions = regionContext.GetAll();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (Region region in regions)
            {
                selectListItem.Add(new SelectListItem(region.Nom, region.Identifiant.ToString()));
            }

            return selectListItem;
        }

        public IActionResult Delete(int id)
        {
            RegionContext regionContext = new RegionContext(connectionString);
            bool isOK = regionContext.Delete(id);

            DeleteRegionViewModels model = new DeleteRegionViewModels();
            model.IsDeleted = isOK;

            return View(model);
        }

        public IActionResult Create()
        {

            RegionViewModels model = new RegionViewModels();
            model.Region = regions();

            return View(model);
        }



        [HttpPost]
        public IActionResult Create(RegionViewModels regionModel)
        {
            RegionContext regionContext = new RegionContext(connectionString);

            regionModel.Region = regions();

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Region region = new Region();

                region.Identifiant = regionModel.Identifiant;
                region.Nom = regionModel.Nom;




                bool isOK = regionContext.Insert(region);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(regionModel);
            }

            return retour;
        }

        public IActionResult Edit(int id)
        {
            RegionContext regionContext = new RegionContext(connectionString);
            Region region = regionContext.Get(id);
            RegionViewModels regionModel = new RegionViewModels();

            regionModel.Identifiant = region.Identifiant;
            regionModel.Nom = region.Nom;


            regionModel.Region = regions();

            return View(regionModel);
        }

        [HttpPost]
        public IActionResult Edit(RegionViewModels regionModel)
        {
            RegionContext regionContext = new RegionContext(connectionString);
            regionModel.Region = regions();
            //Rajouter des contrôles dynamiques

            //if(bugModel.IdentifiantSeverite == 2)
            //{
            //    ModelState.AddModelError("IdentifiantSeverite", "Ne peut être égal à 2");
            //}

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Region region = new Region();

                region.Identifiant = (int)regionModel.Identifiant;
                region.Nom = regionModel.Nom;


                bool isOK = regionContext.Update(region);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(regionModel);
            }

            return retour;
        }
    }
}