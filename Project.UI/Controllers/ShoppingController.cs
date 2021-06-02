using Project.BLL.DesingPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.UI.Models;
using Project.UI.Models.ShoppingTools;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Project.COMMON.Tools;

namespace Project.UI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;
        public ShoppingController()
        {
            _oRep = new OrderRepository();
            _odRep = new OrderDetailRepository();
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();

        }
        public ActionResult ShoppingList(int? page, int? categoryID) 
        {

            PAVM pavm = new PAVM()
            {
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                Categories = _cRep.GetActives()
            };

            if (categoryID != null) TempData["catID"] = categoryID;


            return View(pavm);
        }


        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = _pRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath
            };

            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");
        }







        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["scart"] as Cart;

                cpvm.Cart = c;
                return View(cpvm);
            }

            TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("ShoppingList");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                c.SepettenSil(id);
                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }

            return RedirectToAction("ShoppingList");
        }

        public ActionResult SiparisOnayla()
        {
            UserCard mevcutKullanici;

            if (Session["member"] != null)
            {
                mevcutKullanici = Session["member"] as UserCard;
            }
            else
            {
                TempData["anonim"] = "Kullanıcı üye degil";
            }
            return View();

        }

        //https://localhost:44392/

        [HttpPost]
        public ActionResult SiparisiOnayla(OrderVM ovm)
        {
            bool result;

            Cart sepet = Session["scart"] as Cart;

            ovm.Order.TotalPrice = ovm.PaymentDTO.ShoppingPrice = sepet.TotalPrice.Value;

            //API kullanımı

            //WebApiRestService.WebAPIClient kütüphanesi indirmeyi unutmayın...Yoksa BackEnd'den API'ya istek gönderemezsiniz...

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentDTO);

                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {

                    TempData["baglantiRed"] = "Banka baglantıyı reddetti";
                    return RedirectToAction("ShoppingList");
                }


                if (sonuc.IsSuccessStatusCode) result = true;

                else result = false;

                if (result)
                {
                    if (Session["member"] != null)
                    {
                        UserCard kullanici = Session["member"] as UserCard;
                        ovm.Order.UserCardID = kullanici.ID;
                        ovm.Order.UserName = kullanici.UserName;
                    }
                    else
                    {
                        ovm.Order.UserCardID = null;
                        ovm.Order.UserName = TempData["anonim"].ToString();
                    }

                    _oRep.Add(ovm.Order); //OrderRepository bu noktada Order'i eklerken onun ID'sini olusturuyor...

                    foreach (CartItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);

                        //stoktan düsmesini istiyorsanız

                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitsInStock -= item.Amount;
                        _pRep.Update(stokDus);

                    }

                    TempData["odeme"] = "Siparişiniz bize ulasmıstır. Tesekkür ederiz";
                    MailSender.Send(ovm.Order.Email, body: $"Siparişiniz basarıyla alındı...{ovm.Order.TotalPrice}");
                    return RedirectToAction("ShoppingList");

                }

                else
                {
                    TempData["sorun"] = "Odeme ile ilgili bir sorun olustu. Lutfen bankanızla iletişmize geciniz";
                    return RedirectToAction("ShoppingList");
                }

            }

            return View();
        }
    }
}