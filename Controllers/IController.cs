using System;
using Project3.Models;

namespace Project3.Controllers
{
	public interface IController
	{
		public void Index();

		public void Show(int id);

		public void Create();

		public void Update();

		public void Destroy(int id);
	}
}
