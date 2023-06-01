using System;

namespace Project3
{
    public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public User(string username, string name, string email, string password)
        {
            UserName = username;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
