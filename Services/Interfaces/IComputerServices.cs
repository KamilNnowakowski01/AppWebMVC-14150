using AppModel.Models;

namespace AppWebMVC.Services.Interfaces
{
    public interface IComputerServices
    {
        List<Computer> GetAll();
        Computer? GetById(int id);
        void Create(Computer computer);
        void Update(Computer computer);
        void Delete(int id);

    }
}
