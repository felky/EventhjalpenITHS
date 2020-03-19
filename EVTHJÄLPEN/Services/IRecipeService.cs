using EVTHJÄLPEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Services
{
    public interface IRecipeService
    {
        public ViewProducts GetViewModelByRecipeId(int id, int portion); 
    }
}
