﻿using System;
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
    public class ChampionController : Controller
    {
        private readonly ILogger<ChampionController> _logger;
        private String connectionString;
        public ChampionController(ILogger<ChampionController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            connectionString = configRoot["ConnectionString:DefaultConnection"];
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Champions";

            ChampionIndexViewModels model = new ChampionIndexViewModels();
            model.Title = "Les Champions sont :";

            ChampionContext championContext = new ChampionContext(connectionString);
            List<Champion> champions = championContext.GetAll();
            model.Champions = champions;

            return View(model);
        }


        
            private List<SelectListItem> champions()
        {
            ChampionContext championContext = new ChampionContext(connectionString);

            List<Champion> champions = championContext.GetAll();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (Champion champion in champions)
            {
                selectListItem.Add(new SelectListItem(champion.Nom, champion.Identifiant.ToString()));
            }

            return selectListItem;
        }

        public IActionResult Delete(int id)
        {
            ChampionContext championContext = new ChampionContext(connectionString);
            bool isOK = championContext.Delete(id);

            DeleteChampionViewModels model = new DeleteChampionViewModels();
            model.IsDeleted = isOK;

            return View(model);
        }

        public IActionResult Create()
        {

            ChampionViewModels model = new ChampionViewModels();
            model.Region = champions();

            return View(model);
        }



        [HttpPost]
        public IActionResult Create(ChampionViewModels championModel)
        {
            ChampionContext championContext = new ChampionContext(connectionString);

            championModel.Region = champions();

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Champion champion = new Champion();

                champion.Identifiant = championModel.Identifiant;
                champion.Nom = championModel.Nom;
                champion.Surnom = championModel.Surnom;
                champion.IdentifiantRegion = championModel.IdentifiantRegion;
                champion.IdentifiantRole = championModel.IdentifiantRole;
                


                bool isOK = championContext.Insert(champion);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(championModel);
            }

            return retour;
        }

        public IActionResult Edit(int id)
        {
            ChampionContext championContext = new ChampionContext(connectionString);
            Champion champion = championContext.Get(id);
            ChampionViewModels championModel = new ChampionViewModels();

            championModel.Identifiant = champion.Identifiant;
            championModel.Nom = champion.Nom;
            championModel.Surnom = champion.Surnom;
            championModel.IdentifiantRegion = champion.IdentifiantRegion;
            championModel.IdentifiantRole = champion.IdentifiantRole;

            championModel.Region = champions();

            return View(championModel);
        }

        [HttpPost]
        public IActionResult Edit(ChampionViewModels championModel)
        {
            ChampionContext championContext = new ChampionContext(connectionString);
            championModel.Region = champions();

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Champion champion = new Champion();

                champion.Identifiant = (int)championModel.Identifiant;
                champion.Nom = championModel.Nom;
                champion.Surnom = championModel.Surnom;
                champion.IdentifiantRegion = championModel.IdentifiantRegion;
                champion.IdentifiantRole = championModel.IdentifiantRole;

                bool isOK = championContext.Update(champion);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(championModel);
            }

            return retour;
        }


    }
}