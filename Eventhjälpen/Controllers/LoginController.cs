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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(string inputMail, string inputPassword)
        {
            Hasher hs = new Hasher();
            var hashedPassword = hs.GetHashPw(inputPassword);

            using (TranbarDBOContext tbx = new TranbarDBOContext())
            {
                var pass = tbx.Users.Where(x => x.Password == hashedPassword).ToString();
                var username = tbx.Users.Where(x => x.Email == inputMail).ToString();

                if (hashedPassword == pass && inputMail == username)
                {
                    return View();
                }

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(LoginModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                if (loginVM != null)
                {
                    var json = JsonConvert.SerializeObject(loginVM);
                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await _client.PostAsync("https://localhost:44367/User/Login", stringContent);
                        if (bool.Parse(await response.Content.ReadAsStringAsync()))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            catch
            {

            }
            return View();
        }
    }
}