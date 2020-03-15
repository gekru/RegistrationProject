using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using RegistrationProject.DB;

namespace RegistrationProject.Models
{
    public class UserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name  { get; set; }

        DBConnection dBConnection = new DBConnection();
        
        public IEnumerable<string> GetAllUsersFromDataBase()
        {
            IEnumerable<string> items = from item in dBConnection.DataBase().Descendants("item")
                                        select (string)item;
            string[] users = items.Select(userCheck => JsonConvert.DeserializeObject<UserModel>(userCheck).Login).ToArray();

            return users;
        }

        
        private XElement SerializeUser(UserRegistrationModel user)
        {
            Dictionary<string, string> userDictionary = new Dictionary<string, string>();
            userDictionary.Add("login", user.Login);
            userDictionary.Add("password", user.Password);
            userDictionary.Add("name", user.Name);

            return new XElement("item", new XCData(JsonConvert.SerializeObject(userDictionary)));
        }
        public bool UserAlreadyExist(UserRegistrationModel user)
        {
            var users = GetAllUsersFromDataBase();

            return users.Contains(user.Login.Trim().ToLower());
        }
        public bool CreateUser(UserRegistrationModel user)
        {
            var dataBase = dBConnection.DataBase();
            var newUser = SerializeUser(user);
            dataBase.Add(newUser);
            dataBase.Save(dBConnection.XmlPath());
            return true;
        }
    }
    
}