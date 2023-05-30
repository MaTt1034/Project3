using System;
namespace Project3.Controllers
{
	public interface IController
	{
		public void Index();

		public void Show(int id);

		public void Create();

		public void Update(int id);

		public void Destroy(int id);
	}
}
