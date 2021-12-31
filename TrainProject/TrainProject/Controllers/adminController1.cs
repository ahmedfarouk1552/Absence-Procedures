using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainProject.Models;

namespace TrainProject.Controllers
{
    public class adminController : Controller
    {
        ProjectContext db;
        public adminController(ProjectContext db)
        {
            this.db = db;

        }
        [Authorize(Roles ="1")]
        public IActionResult Index()
        {
            var x = db.UserRoles.ToList();


            return View();
        }
    }
}
