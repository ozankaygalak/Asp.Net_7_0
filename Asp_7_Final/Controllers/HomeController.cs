using Asp_7_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;

        public HomeController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sirala()
        {
            var users = _dbContext.Users.OrderBy(u => u.DogumTarihi).ToList();
            return View(users);
        }

        public IActionResult Arama(string ad,String soyad)
        {
            var users = _dbContext.Users.ToList();
            if (!string.IsNullOrEmpty(ad) && !string.IsNullOrEmpty(soyad))
            {
                users = users.Where(u => u.Ad == ad).OrderBy(u => u.Ad).ToList();
                users = users.Where(u => u.Soyad == soyad).OrderBy(u => u.Soyad).ToList();
            }
            else if (!string.IsNullOrEmpty(ad))
            {
                users = users.Where(u => u.Ad == ad).OrderBy(u => u.Ad).ToList();

            }
            
            else if(!string.IsNullOrEmpty(soyad))
            {
                users = users.Where(u => u.Soyad == soyad).OrderBy(u => u.Soyad).ToList();

            }
            else
            {
                users = _dbContext.Users.OrderBy(u => u.Ad).ToList();
            }

            return View(users);
        }


        [HttpGet]
        public IActionResult GrafikCinsiyet()
        {
            var users = _dbContext.Users.ToList();
            int e = users.Count(u => u.cinsiyet == "Erkek");
            int k = users.Count(u => u.cinsiyet == "Kadın");
            int d = users.Count(u => u.cinsiyet == "Diğer");
            var cinsiyetToplam = new List<int> { e, k, d };

            return View("GrafikCinsiyet", cinsiyetToplam);
        }
        
        public IActionResult GrafikYas()
        {
            var users = _dbContext.Users.ToList();
            var toplamKisi = new List<int>()
            {
                0,0,0,0
            };
            DateTime today = DateTime.Today;
            foreach (var user in users)
            {

                int age = today.Year - user.DogumTarihi.Year;
                if (user.DogumTarihi.Month < today.Month)
                {
                    age++;
                }
                if (age < 15)
                {
                    toplamKisi[0] += 1;
                }else if (age < 30 && age > 14)
                {
                    toplamKisi[1] += 1;

                }
                else if (age < 45 && age > 29)
                {
                    toplamKisi[2] += 1;

                }
                else
                {
                    toplamKisi[3] += 1;
                }


                
            }
            return View("GrafikYas", toplamKisi);
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate < today.AddYears(-age))
            {
                age++;
            }

            return age;
        }

        [HttpPost]
        public IActionResult Sil(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Sirala"); // or redirect to any other desired page
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            int today = DateTime.Today.Year;
            int yas = user.DogumTarihi.Year;
            int fark = today - yas;
            if (ModelState.IsValid && fark<150 && fark >0 )
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var foundUser = _dbContext.Users.FirstOrDefault(u => u.Ad == user.Ad && u.Soyad == user.Soyad && u.DogumTarihi == user.DogumTarihi);
            if (foundUser != null)
            {
                // Giriş başarılı
                // Kullanıcı oturumunu başlat
                return RedirectToAction("Home");
            }
            else
            {
                // Kullanıcı bulunamadı veya bilgiler hatalı
                ModelState.AddModelError("", "Geçersiz kullanıcı bilgileri.");
                return View(user);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
