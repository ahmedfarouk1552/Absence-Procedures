using ExcelDataReader.Core;
using ExcelDataReader;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrainProject.Models;

namespace TrainProject.Controllers
{
    public class TestController : Controller
    {
        ProjectContext db;
        public TestController(ProjectContext db)
        {
            this.db = db;
        }
        //[Authorize]
        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }
        //public IActionResult getdata()
        //{
        //    return View(db.Users.ToList());
        //}
   [AllowAnonymous]
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult register(User u)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(u);
                db.SaveChanges();


                return RedirectToAction("Login");

            }
            else 
                return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(bool  rememb, User us)
        {
           
                User u = db.Users.Where(n => n.Email == us.Email && n.Password == us.Password).FirstOrDefault();
                if (u == null)
                  //  return Unauthorized();
                    return View();

            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                        new Claim(ClaimTypes.Email,u.Email),
                        new Claim(ClaimTypes.Role,u.UserRolesId.ToString())
                        
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principle = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle,
                        new AuthenticationProperties
                        {
                            IsPersistent=rememb,
                            ExpiresUtc = DateTime.Now.AddDays(30)
                        }) ;

                   return LocalRedirect("/Test/Index");


                    //return RedirectToAction("Index");
            }
       
            
          
        

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }



        public IActionResult admin()
        {
        //List<UserRole> x = db.UserRoles.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult calcAbsent(List<UserReport>userReports)
        {

            userReports = userReports == null ? new List<UserReport>() : userReports;
            ViewBag.userReports = userReports;
            ViewBag.count = userReports.Count();
            return View();
        }


        [HttpPost]
        public IActionResult calcAbsent(IFormFile file,[FromServices] IHostingEnvironment hostingEnvironment)
        {
            //       string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var students = this.GetReportsList(file.FileName);
            return calcAbsent(students);
        }
        private List<UserReport> GetReportsList (string fName)
        {
            List<UserReport> reports = new List<UserReport>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\"}" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using(var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        reports.Add(new UserReport()
                        {

                            UserId = int.Parse(reader.GetValue(0).ToString()),
                            JionTimeOfUser = DateTime.Parse(reader.GetValue(1).ToString()),
                            LeaveTimeOfUser = DateTime.Parse(reader.GetValue(2).ToString()),
                            Duration = reader.GetValue(3).ToString(),
                           ReportId = int.Parse(reader.GetValue(4).ToString())
                        });
                    }
                } 
            }
            return reports;
        }













    }
}
// Scaffold-DbContext "Server=.;Database=Project;Trusted_Connection=True;"Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models