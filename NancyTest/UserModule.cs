using System;
using System.Collections.Generic;
using System.Text;
using Nancy;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NancyTest
{
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            Get("/users/{UserId}", parameters => ReturnUserData(parameters.UserId));
            // Delete an existing user            
            Get("/users/delete/{UserId}", parameters => DeleteUser(parameters.UserId));
            // Add a new user
            Get("/users/{Name}/{Password}", parameters => PutNewUser(parameters.Name, parameters.Password));
            Get("/authentify/login={Name}&password={Password}", parameters => Authentification(parameters.Name, parameters.Password));
        }
        
        public static dynamic ReturnUserData(int userId)
        {
            using (var context = new Context())
            {
                var userInfo = (from u in context.Users
                                where u.UserId == userId
                                select new { u.Name, u.UserId }).FirstOrDefault();

                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize(userInfo);

                return jsonString;
            }

        }

        public dynamic DeleteUser(int userId)
        {
            using (var context = new Context())
            {
                var userToDelete = (from u in context.Users
                                    where u.UserId == userId
                                    select u).FirstOrDefault();

                context.Remove(userToDelete);
                context.SaveChanges();

                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize(userToDelete);

                return "user deleted";
            }
        }
        
        public dynamic PutNewUser(string userName, string password)
        {

            using (var context = new Context())
            {
                var user = new User() { Name = userName, Password = password };
                context.AddRange(user);
                context.SaveChanges();
            }

            return "user created";
        }

        public dynamic Authentification(string Name, string password)
        {
            var context = new Context();
            var authentif = (from u in context.Users
                             where u.Name == Name && u.Password == password
                             select u).FirstOrDefault();
            string message = "";
            if(authentif==null)
            {
                message = "You are not authorized";
            }
            else
            {
                message = Name + " is authorized";
            }
            string json = JsonConvert.SerializeObject(message);
            return json;
        }
    }
}
