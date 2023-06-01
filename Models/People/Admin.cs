using System;

namespace Project3{
    public class Admin : User
    {
        public Admin(string username, string name, string email, string password) : base(username, name, email, password)
        {
        }
    }
}