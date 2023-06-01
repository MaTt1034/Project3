using Project3.Models;
using System;

namespace Project3{
    public class Department: IModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

        public Department(int id, string name, string desc = "")
        {
            Name = name;
            Id = id;
            Description = desc;
        }

    }
}