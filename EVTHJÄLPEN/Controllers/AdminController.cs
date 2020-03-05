using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Models;
using Microsoft.AspNetCore.Authorization;

namespace EVTHJÄLPEN.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        //  [Authorize(Roles = "admin")]
        public IActionResult AddRecipe(int ingredients = 5)
        {
            NewRecipeVM vm = new NewRecipeVM();

            if(ingredients <= 0 || ingredients >= 40)
            {
                vm.Ingredients = 1;
                return View(vm);
            }

            vm.Ingredients = ingredients;
            return View(vm);
        }
    }
}