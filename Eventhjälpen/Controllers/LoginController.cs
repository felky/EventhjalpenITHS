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
        private Hasher hs = new Hasher();
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

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View(new VMRegister());
        }


        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult Register(VMRegister vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (vm != null)
            {
                try
                {
                    string password = hs.GetHashPw(vm.Password);
                    using (TranbarDBOContext tbx = new TranbarDBOContext())
                    {
                        var user = new Users()
                        {
                            Firstname = vm.FirstName,
                            Lastname = vm.LastName,
                            Email = vm.Email,
                            Password = password,
                            Phonenumber = vm.PhoneNumber
                        };

                        tbx.Add(user);
                        tbx.SaveChanges();
                        return RedirectToAction("Index", "Home");

                    }
                }
                catch(Exception e) {
                    return Content("Error inserting user: " + e);
                }
            }

            return View();
        }

        public bool LoginAction(string inputMail, string inputPassword)
        {
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