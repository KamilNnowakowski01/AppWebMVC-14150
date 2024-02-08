using AppModel.Models;

namespace AppWebMVC.Services.Interfaces
{
    public interface IGraphicsServices
    {
        List<Graphics> GetAll();
        Graphics? GetById(int id);
        void Create(Graphics graphics);
        void Update(Graphics graphics);
        void Delete(int id);

    }
}
