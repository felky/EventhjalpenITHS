using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eventhjälpen.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;
        public LoginController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult Login()
        {
            return View(new VMLogin());
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Login(VMLogin vm)
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Failed Valid");

                return View();
            } 

            if(vm.Email != null && vm.Password != null)
            {
                System.Diagnostics.Debug.WriteLine("Trying to log in");

                if (LoginAction(vm.Email, vm.Password))
                {
                    System.Diagnostics.Debug.WriteLine("Success");

                    return RedirectToAction("Index", "Home");
                }
            }

            System.Diagnostics.Debug.WriteLine("Could not log in");

            return View();
        }

        public bool LoginAction(string inputMail, string inputPassword)
        {
            Hasher hs = new Hasher();
            var hashedPassword = hs.GetHashPw(inputPassword);

            using (TranbarDBOContext tbx = new TranbarDBOContext())
            {
                System.Diagnostics.Debug.WriteLine(tbx);

                var pass = tbx.Users.FirstOrDefault(x => x.Password == hashedPassword).ToString();
                var email = tbx.Users.FirstOrDefault(x => x.Email == inputMail).ToString();

                if (email != null && pass != null)
                {
                    System.Diagnostics.Debug.WriteLine("Success!");
                    return true;
                } else
                {
                    System.Diagnostics.Debug.WriteLine("L Failed");
                    return false;
                }
            }
        }
    }
}