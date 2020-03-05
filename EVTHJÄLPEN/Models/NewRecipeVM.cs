using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;

namespace EVTHJÄLPEN.Models
{
    public class NewRecipeVM
    {
        public Recipe RecipeModel { get; set; }
        public int Ingredients { get; set; }
    }
}
