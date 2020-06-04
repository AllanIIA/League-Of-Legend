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
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private String connectionString;
        public RoleController(ILogger<RoleController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            connectionString = configRoot["ConnectionString:DefaultConnection"];
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Rôles";

            RoleIndexViewModels model = new RoleIndexViewModels();
            model.Title = "Les Rôles sont :";

            RoleContext roleContext = new RoleContext(connectionString);
            List<Role> roles = roleContext.GetAll();
            model.Roles = roles;

            return View(model);
        }



        private List<SelectListItem> roles()
        {
            RoleContext roleContext = new RoleContext(connectionString);

            List<Role> roles = roleContext.GetAll();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (Role role in roles)
            {
                selectListItem.Add(new SelectListItem(role.Nom, role.Identifiant.ToString()));
            }

            return selectListItem;
        }

        public IActionResult Delete(int id)
        {
            RoleContext roleContext = new RoleContext(connectionString);
            bool isOK = roleContext.Delete(id);

            DeleteRoleViewModels model = new DeleteRoleViewModels();
            model.IsDeleted = isOK;

            return View(model);
        }

        public IActionResult Create()
        {

            RoleViewModels model = new RoleViewModels();
            model.Region = roles();

            return View(model);
        }



        [HttpPost]
        public IActionResult Create(RoleViewModels roleModel)
        {
            RoleContext roleContext = new RoleContext(connectionString);

            roleModel.Region = roles();

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Role role = new Role();

                role.Identifiant = roleModel.Identifiant;
                role.Nom = roleModel.Nom;
              



                bool isOK = roleContext.Insert(role);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(roleModel);
            }

            return retour;
        }

        public IActionResult Edit(int id)
        {
            RoleContext roleContext = new RoleContext(connectionString);
            Role role = roleContext.Get(id);
            RoleViewModels roleModel = new RoleViewModels();

            roleModel.Identifiant = role.Identifiant;
            roleModel.Nom = role.Nom;


            roleModel.Region = roles();

            return View(roleModel);
        }

        [HttpPost]
        public IActionResult Edit(RoleViewModels roleModel)
        {
            RoleContext roleContext = new RoleContext(connectionString);
            roleModel.Region = roles();
            //Rajouter des contrôles dynamiques

            //if(bugModel.IdentifiantSeverite == 2)
            //{
            //    ModelState.AddModelError("IdentifiantSeverite", "Ne peut être égal à 2");
            //}

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Role role = new Role();

                role.Identifiant = (int)roleModel.Identifiant;
                role.Nom = roleModel.Nom;
               

                bool isOK = roleContext.Update(role);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(roleModel);
            }

            return retour;
        }
    }
}