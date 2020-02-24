using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventhjälpen.Models;

namespace Eventhjälpen.Controllers
{
    public class RecepiesController : Controller
    {
        [HttpGet]
        [Route("[controller]")]
        public IActionResult Recepies()
        {
            List<Recipe> recepies = new List<Recipe>();

            try
            {
                using (TranbarDBOContext tbx = new TranbarDBOContext())
                {
                    recepies = tbx.Recipe
                        .ToList();
                }
               
                return View(recepies);
            }
            catch (Exception e)
            {
                return Content("Failed loading recepies: " + e);
            }
        }
    }
}