using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private List<Cheese> Cheeses = new List<Cheese>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            int error = 0;
            //string addcheese = name;
            if (name == null)
            {
                string errorMessage = "Cheese name is required.";
                ViewBag.error = errorMessage;
                return View("Add");
            }
            foreach (char letter in name)
            {
                if (!(char.IsLetter(letter) || char.IsWhiteSpace(letter)))
                {
                    error += 1;
                }
            }

            if (error == 0)
            {
                Cheese addcheese = new Cheese(name, description);
                Cheeses.Add(addcheese);
                return Redirect("/Cheese");
            }
            else
            {
                string errorMessage = "Cheese name must only contain " +
                    "letters and spaces.";
                ViewBag.error = errorMessage;
                return View("Add");

            }
            

            
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult RemoveCheese(List<string> names)
        {
            foreach (string name in names)
            {
                Cheeses.RemoveAll(cheese => cheese.Name == name);
            }

            return Redirect("/Cheese");
        }
    }
}
