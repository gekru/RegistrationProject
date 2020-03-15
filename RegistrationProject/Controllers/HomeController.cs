using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using RegistrationProject.Models;


namespace RegistrationProject.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        #region skip
        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        */
        #endregion
        public ActionResult SignUp()
        {
            ViewBag.Message = "Реєстрація користувача";
            UserRegistrationModel user = new UserRegistrationModel();
            return View();
        }

        public ActionResult Authorization()
        {
            ViewBag.Message = "Авторизація користувача";
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserRegistrationModel user)
        {
            if (UserAlreadyExist(user))
            {
                ModelState.AddModelError("Login", "already-exist");
                return View();
            }
            else
            {
                CreateUser(user);
                TempData["message"] = "Created";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Authorization(UserAuthorizationModel user)
        {
            if (FindUser(user) != null)
            {
                TempData["message"] = $"Привіт {user.Login.Remove(1).ToUpper()+user.Login.Substring(1)}";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Ви не правильно вказали логін та/або пароль";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult SignupAsJSONAsync(UserRegistrationModel user)
        {
            if (UserAlreadyExist(user))
            {
                ModelState.AddModelError("Login", "already-exist");
                return new HttpStatusCodeResult(422, "already-exist");
            }
            else
            {
                CreateUser(user);
                TempData["message"] = "Created";
                return new HttpStatusCodeResult(201, "Created");
            }
        }
        public IEnumerable<UserModel> GetAllUsersFromDataBase()
        {
            IEnumerable<string> items = from item in DataBase().Descendants("item")
                                        select (string)item;
            UserModel[] users = items.Select(userCheck => JsonConvert.DeserializeObject<UserModel>(userCheck)).ToArray();
            
            return users;
        }

        private string XmlPath() 
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"DB\data.xml";
        }
        private XElement DataBase()
        {
            return XElement.Load(XmlPath());
        }
        private XElement SerializeUser(UserRegistrationModel user)
        {
            Dictionary<string, string> userDictionary = new Dictionary<string, string>();
            userDictionary.Add("login", user.Login);
            userDictionary.Add("password", user.Password);
            userDictionary.Add("name", user.Name);

            return new XElement("item", new XCData(JsonConvert.SerializeObject(userDictionary)));
        }
        private bool UserAlreadyExist(UserRegistrationModel user)
        {
            var users = GetAllUsersFromDataBase();
            
            return users.Select(dbuser => dbuser.Login).Contains(user.Login.Trim().ToLower());
        }
        private bool CreateUser(UserRegistrationModel user)
        {
            var dataBase = DataBase();
            var newUser = SerializeUser(user);
            dataBase.Add(newUser);
            dataBase.Save(XmlPath());
            return true;
        }

        private UserModel FindUser(UserAuthorizationModel user)
        {
            var users = GetAllUsersFromDataBase();
            return users.Where(dbuser => dbuser.Login == user.Login && dbuser.Password == user.Password).FirstOrDefault();
        }

    }
}