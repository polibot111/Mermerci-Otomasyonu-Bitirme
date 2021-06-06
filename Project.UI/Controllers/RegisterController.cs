using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using System;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System.Collections.Generic;
using Project.UI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        UserCardRepository _usRep;
        CompanyCardRepository _ccRep;

        public RegisterController()
        {
            _usRep = new UserCardRepository();
            _ccRep = new CompanyCardRepository();
        }


        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(UserCardVM apvm)
        {

            UserCard userCard = apvm.UserCard;

            CompanyCard companyCard = apvm.CompanyCard;

            userCard.Password = DantexCrypt.Crypt(userCard.Password); //sifreyi kriptoladık

            if (_usRep.Any(x => x.UserName == userCard.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmıs";
                return View();
            }
            else if (_usRep.Any(x => x.Email == userCard.Email))
            {
                ViewBag.ZatenVar = "Email zaten kayıtlı";
                return View();
            }

            //Kullanıcı basarılı bir şekilde register işlemini tamamladıysa ona mail gonder...

            string gonderilecekMail = "Tebrikler...Hesabınız olusturulmustur...Hesabınızı aktive etmek icin https://localhost:44392/Register/Activation/" + userCard.ActivationCode + " linkine tıklayabilirsiniz.";

            MailSender.Send(userCard.Email, body: gonderilecekMail, subject: "Hesap aktivasyon!");
            _usRep.Add(userCard); //öncelikle bunu eklemelisiniz...Cünkü AppUser'in ID'si ilk basta olusmalı...Cünkü siz birebir ilişkide AppUser zorunlu olan alan Profile ise opsiyonal alan olarak olusturdunuz... Dolayısıyla ilk basta AppUser'in ID'si SaveChanges ile olusmalı 


            if (!string.IsNullOrEmpty(companyCard.CompanyName) || !string.IsNullOrEmpty(companyCard.Phone) || !string.IsNullOrEmpty(companyCard.Address))
            {
                companyCard.ID = userCard.ID;
                _ccRep.Add(companyCard);
            }

            return View("RegisterOk");
        }

        public ActionResult Activation(Guid id)
        {
            UserCard aktifEdilecek = _usRep.FirstOrDefault(x => x.ActivationCode == id);

            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _usRep.Update(aktifEdilecek);

                TempData["HesapAktifmi"] = "Hesabınız Aktif hale getirildi";
                return RedirectToAction("Login", "Home");
            }
            TempData["HesapAktifmi"] = "Aktif edilecek hesap bulunamadı";
            return RedirectToAction("Login", "Home");



        }

        public ActionResult RegisterOk()
        {
            return View();
        }
    }
}