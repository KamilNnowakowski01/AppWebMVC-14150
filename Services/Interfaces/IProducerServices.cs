using AppModel.Models;

namespace AppWebMVC.Services.Interfaces
{
    public interface IProducerServices
    {
        List<Producer> GetAll();
        Producer? GetById(int id);
        void Create(Producer producer);
        void Update(Producer producer);
        void Delete(int id);

    }
}
