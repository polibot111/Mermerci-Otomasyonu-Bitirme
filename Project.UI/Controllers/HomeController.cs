using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        UserCardRepository _usRep;
        public HomeController()
        {
            _usRep = new UserCardRepository();
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        
        }
        [HttpPost]
        public ActionResult Login(UserCard userCard) 
        {

            UserCard uye = _usRep.FirstOrDefault(x => x.UserName == userCard.UserName);

            string decrypted = DantexCrypt.DeCrypt(uye.Password);
            
            if (userCard.Password == decrypted && uye != null)
            {

                if (uye.Role == ENTITIES.Enums.UserCardRole.Admin)
                {
                    if (!uye.Active)
                    {
                        return AktifKontrol();
                    }
                    Session["admin"] = uye;
                    return RedirectToAction("CategoryList", "Category", new { area = "Admin" });

                }
               

                else
                {
                    ViewBag.RolBelirsiz = "Rol belirlenmemiş";
                    return View();
                }




            }

            ViewBag.KullaniciYok = "Kullanıcı bulunamadı";
            return View();




        }

        private ActionResult AktifKontrol()
        {
            ViewBag.AktifDegil = "Lutfen hesabınızı aktif hale getiriniz...Mailinizi kontrol ediniz...";
            return View("Login");
        }


    }


    
}