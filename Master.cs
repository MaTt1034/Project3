using Project3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    internal class Master
    {
      public static ProductController ProductController = new ProductController();
      public static DepartmentController DepartmentController = new DepartmentController();
    }
}
