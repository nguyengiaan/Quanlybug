using Microsoft.AspNetCore.Mvc;
using Quanlybug.data;
using Quanlybug.Models;
using System.Diagnostics;

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

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}