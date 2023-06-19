using Microsoft.AspNetCore.Mvc;
using Quanlybug.data;

namespace Quanlybug.Controllers
{
    public class Detail_project : Controller
    {
        QUANLYBUGContext ql=new QUANLYBUGContext();
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult pending()
        {
               var sql1 = HttpContext.Session.GetString("ID");

            var sql = ql.UserMembers.Where(x => x.IdUser == Convert.ToInt32(sql1)).ToList().FirstOrDefault();
         
           if (sql.Permission == "2")
            {
                var project = ql.Projects.Select(x => new { x.NameProject, x.ContextProject, x.Picture, x.IdUserNavigation.NameUser, x.Date, x.Peformer, x.Status, x.IdUser, x.IdProject }).Where(x => x.IdUser.Equals(sql.IdUser) && x.Status.Equals("Đang chờ duyệt"));
                if (project.Count() == 0)
                {
                    ViewBag.project1 = "không có dự án";
                    return View(sql);
                }
                else
                {
                    ViewBag.project = project;
                    return View(sql);
                }
       
            }
            else
            {
                var project = ql.Projects.Select(x => new { x.NameProject, x.ContextProject, x.Picture, x.IdUserNavigation.NameUser, x.Date, x.Peformer, x.Status, x.IdUser, x.IdProject }).Where(x => x.Peformer.Equals(sql.NameUser) && x.Status.Equals("Đang chờ duyệt"));
                if (project.Count() == 0)
                {
                    ViewBag.project1 = "không có dự án";
                    return View(sql);
                }
                else
                {
                    ViewBag.project = project;
                    return View(sql);
                }

            }

        }
        
        public IActionResult updatestatus(int id)
        {
            var sql = ql.Projects.Find(id);
            try
            {
                if (sql != null)
                {
                    sql.Status = "đã hoàn thành";
                    ql.SaveChanges();
                    return RedirectToAction("pending", "Detail_project");
                }
            }
            catch (Exception ex)
            {
                ViewBag.erro = "Lỗi cập nhật";
            }
            return RedirectToAction("pending", "Detail_project");
        }
        public IActionResult submit()
        {
            var sql1 = HttpContext.Session.GetString("ID");

            var sql = ql.UserMembers.Where(x => x.IdUser == Convert.ToInt32(sql1)).ToList().FirstOrDefault();

            if (sql.Permission == "2")
            {
                var project = ql.Projects.Select(x => new { x.NameProject, x.ContextProject, x.Picture, x.IdUserNavigation.NameUser, x.Date, x.Peformer, x.Status, x.IdUser, x.IdProject }).Where(x => x.IdUser.Equals(sql.IdUser) && x.Status.Equals("Đang chờ duyệt"));
                if (project.Count() == 0)
                {
                    ViewBag.project1 = "không có dự án";
                    return View(sql);
                }
                else
                {
                    ViewBag.project = project;
                    return View(sql);
                }

            }
            else
            {
                var project = ql.Projects.Select(x => new { x.NameProject, x.ContextProject, x.Picture, x.IdUserNavigation.NameUser, x.Date, x.Peformer, x.Status, x.IdUser, x.IdProject }).Where(x => x.Peformer.Equals(sql.NameUser) && x.Status.Equals("Đang chờ duyệt"));
                if (project.Count() == 0)
                {
                    ViewBag.project1 = "không có dự án";
                    return View(sql);
                }
                else
                {
                    ViewBag.project = project;
                    return View(sql);
                }

            }
   
        }
    }
}
