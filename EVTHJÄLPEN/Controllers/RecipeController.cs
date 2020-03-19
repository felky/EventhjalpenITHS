using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using EVTHJÄLPEN.Models;
using EVTHJÄLPEN.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EVTHJÄLPEN.Controllers
{
    public class RecipeController : Controller
    {
        public IRecipeService _recipeService; 
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService; 
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult Recipes(int id)
        {
            List<Recipe> _recepies = new List<Recipe>();
            List<RecipeType> _recipeTypes = new List<RecipeType>();
            RecipeVM vm = new RecipeVM();

            if (id <= 0)
            {
                try
                {
                    using (ApplicationDbContext ctx = new ApplicationDbContext())
                    {
                        _recepies = ctx.Recipe
                            .ToList();
                        _recipeTypes = ctx.RecipeType
                            .ToList();
                    }


                    vm.recipes = _recepies;
                    vm.recipeTypes = _recipeTypes;

                    return View(vm);
                }
                catch (Exception e)
                {
                    return Content("Failed loading recepies: " + e);
                }
            }

            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    _recipeTypes = ctx.RecipeType
                        .ToList();
                    _recepies = ctx.Recipe.Where(x => x.RecipeTypeId == id)
                        .ToList();
                }

                vm.recipes = _recepies;
                vm.recipeTypes = _recipeTypes;

                return View(vm);
            }
            catch (Exception e)
            {
                return Content("Failed loading recepies: " + e);
            }
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult ViewRecipe(int id, int portion)
        {
               if (portion == 0)
                portion = 4;

            ViewProducts vp = _recipeService.GetViewModelByRecipeId(id, portion);                    
            return View(vp);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult OnPost([Bind("Product")] List<IngredientToCart> ic)
        {


            return RedirectToAction("ViewCart", "Checkout");

        }
    }
}