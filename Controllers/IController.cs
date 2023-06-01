using System;
using Project3.Models;

namespace Project3.Controllers
{
    public interface IController<T,TIdentifier>
    {

        public IReadOnlyCollection<T> GetList();
        

        public void Index();

        public void Show(T item);

        public void Show(TIdentifier id);

        public void Create();

        public void Add(T item);

        public void Update(T temp);

        public bool Destroy(TIdentifier id);
        public bool Destroy(T item);

    }
}
