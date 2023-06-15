using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using Quanlybug.data;
using Quanlybug.Models;
using System.Diagnostics;
using System.Net;

namespace Quanlybug.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QUANLYBUGContext ql=new QUANLYBUGContext();
  
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            var sql = ql.UserMembers.Where(x => x.Account.Equals(user.Account) && x.Password.Equals(user.Password)).FirstOrDefault();
            if(sql==null)
            {
                ViewBag.error = "Tài khoản hoặc mật khẩu bị sai";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("ID",Convert.ToString(sql.IdUser));
                return RedirectToAction("Home", "Home");
            }
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register register,IFormCollection form)
        {
            try
            {
              if(ModelState.IsValid)
              {
                    var sql=ql.UserMembers.Where(x=>x.Account.Equals(register.Account)).FirstOrDefault(); 
                    if(sql==null)
                    {
                       

                        var select = form["job"];
                        UserMember user = new UserMember();
                        user.Account = register.Account;
                        user.NameUser = register.NameUser;
                        user.Permission = select;
                        user.Password = register.Password;
                        ql.UserMembers.Add(user);
                        ql.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = "tài khoản đã tồn tài";
                    }    
              }    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Home() 
        {
            var sql1 = HttpContext.Session.GetString("ID");
            var sql = ql.UserMembers.Where(x => x.IdUser==Convert.ToInt32( sql1)).ToList().FirstOrDefault();
            ViewBag.project = ql.Projects.Select(x => new { x.NameProject, x.ContextProject, x.Picture ,x.IdUserNavigation.NameUser});
            return View(sql);
        }
        [HttpGet]
        public IActionResult CreateProject()
        {
            var sql1 = HttpContext.Session.GetString("ID");
            var sql = ql.UserMembers.Where(x => x.IdUser == Convert.ToInt32(sql1)).ToList().FirstOrDefault();
            return View(sql);
       
        }
  
        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync(Project_1 project, IFormFile uploadhinh)
        {
            Project pro=new Project();
            var project1 = new Quanlybug.Models.Project_1();
            ViewData["Project"] = project1;
            var sql1 = Convert.ToInt32(HttpContext.Session.GetString("ID"));
            var sql = ql.UserMembers.Where(x => x.IdUser == Convert.ToInt32(sql1)).ToList().FirstOrDefault();
          
            try
            {
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/Images", uploadhinh.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await uploadhinh.CopyToAsync(stream);
                    }
                    pro.NameProject = project.NameProject;
                    pro.ContextProject=project.ContextProject;
                    pro.Picture = uploadhinh.FileName;
                    pro.IdUser = sql1;
                    ql.Projects.Add(pro);
                    ql.SaveChanges();
                    return RedirectToAction("Home");
                   
                }
            }catch (Exception ex)
            {
                return View(sql);
            }
            return View(sql);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}