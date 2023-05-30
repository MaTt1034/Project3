using System;
namespace Project3.Controllers
{
	public interface IController
	{
		// gets all records
		public void Index();

		// shows an individual record
		public void Show(int id);

		public void Edit(int id);

		public void Create();

		public void Update(int id);

		public void Destroy(int id);
	}
}
