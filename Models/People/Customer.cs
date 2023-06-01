using System;

namespace Project3{
    public class Customer : User
    {
        public Customer(string username, string name, string email, string password) : base(username, name, email, password)
        {
        }
    }
}