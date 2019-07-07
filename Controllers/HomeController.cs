using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Full") == null)
            {
                HttpContext.Session.SetInt32("Full", 20);
            }
            if (HttpContext.Session.GetInt32("Happy") == null)
            {
                HttpContext.Session.SetInt32("Happy", 20);
            }
            if (HttpContext.Session.GetInt32("Meals") == null)
            {
                HttpContext.Session.SetInt32("Meals", 3);
            }
            if (HttpContext.Session.GetInt32("Energy") == null)
            {
                HttpContext.Session.SetInt32("Energy", 50);
            }
            if(HttpContext.Session.GetInt32("Full") < 0 || HttpContext.Session.GetInt32("Energy") < 0){
                HttpContext.Session.SetString("Msg", "You Lost! Learn how to take care of ya self fool!");
            }
            if(HttpContext.Session.GetInt32("Full") >= 100 || HttpContext.Session.GetInt32("Energy") >= 100){
                HttpContext.Session.SetString("Msg", "Way to go you champion of a person!");
            }

            ViewBag.Full = HttpContext.Session.GetInt32("Full");
            ViewBag.Happy = HttpContext.Session.GetInt32("Happy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Msg = HttpContext.Session.GetString("Msg");
            return View();
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            Random rand = new Random();
            int x = rand.Next(1,5);
            if(x == 3)
            {
                int? meals = HttpContext.Session.GetInt32("Meals");
                meals--;
                HttpContext.Session.SetInt32("Meals", Convert.ToInt32(meals));
                HttpContext.Session.SetString("Msg", $"Dojodachini ate but did not like the meal and lost 1 meal");
                return RedirectToAction("Index");
            }
            int? meal = HttpContext.Session.GetInt32("Meals");
            meal--;
            HttpContext.Session.SetInt32("Meals", Convert.ToInt32(meal));
            int? fullness = HttpContext.Session.GetInt32("Full");
            int y = rand.Next(5,11);
            fullness += y;
            HttpContext.Session.SetString("Msg", $"Dojodachini ate your delicious meal: Gained {y} and lost 1 meal");
            HttpContext.Session.SetInt32("Full", Convert.ToInt32(fullness));
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            Random rand = new Random();
            int x = rand.Next(1,5);
            if(x == 3)
            {
                int? energies = HttpContext.Session.GetInt32("Energy");
                energies-=5;
                HttpContext.Session.SetInt32("Energy", Convert.ToInt32(energies));
                HttpContext.Session.SetString("Msg", $"Dojodachini played but did not like it and lost 5 energy");
                return RedirectToAction("Index");
            }
            int? energy = HttpContext.Session.GetInt32("Energy");
            energy-=5;
            HttpContext.Session.SetInt32("Energy", Convert.ToInt32(energy));
            int? happiness = HttpContext.Session.GetInt32("Happy");
            int y = rand.Next(5,11);
            happiness += y;
            HttpContext.Session.SetString("Msg", $"Dojodachini played: Gained {y} and lost 5 energy");
            HttpContext.Session.SetInt32("Happy", Convert.ToInt32(happiness));
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            int? energy = HttpContext.Session.GetInt32("Energy");
            energy-=5;
            HttpContext.Session.SetInt32("Energy", Convert.ToInt32(energy));
            Random rand = new Random();
            int? meals = HttpContext.Session.GetInt32("Meals");
            int y = rand.Next(1,4);
            meals += y;
            HttpContext.Session.SetInt32("Meals", Convert.ToInt32(meals));
            HttpContext.Session.SetString("Msg", $"Dojodachini played: Gained {y} and lost 5 energy");
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            int? energy = HttpContext.Session.GetInt32("Energy");
            energy+=15;
            HttpContext.Session.SetInt32("Energy", Convert.ToInt32(energy));
            Random rand = new Random();
            int? fullness = HttpContext.Session.GetInt32("Full");
            fullness -= 5;
            HttpContext.Session.SetInt32("Full", Convert.ToInt32(fullness));
            int? happy = HttpContext.Session.GetInt32("Happy");
            happy -= 5;
            HttpContext.Session.SetInt32("Happy", Convert.ToInt32(happy));
            return RedirectToAction("Index");
        }

    }
}
