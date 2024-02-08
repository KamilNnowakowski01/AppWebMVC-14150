using AppModel.Models;

namespace AppWebMVC.Services.Interfaces
{
    public interface IComputersGraphicsServices
    {
        Task Create(int computerId, int graphicsId);
        Task Delete(int id);

    }
}
