using AppDataBase.Data;
using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class GraphicsServices : IGraphicsServices
    {
        private readonly ApplicationDbContext _context;

        public GraphicsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Graphics> GetAll()
        {
            var graphics = _context.Graphics.ToList();

            return graphics;
        }

        public Graphics? GetById(int id)
        {
            var graphics =
                _context.Graphics
                .FirstOrDefault(p => p.GraphicsId == id);

            return graphics;
        }

        public void Create(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
            _context.Graphics.Add(graphics);
            _context.SaveChanges();
        }

        public void Update(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            _context.Graphics.Update(graphics);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var graphics = _context.Graphics.Find(id);
            if (graphics != null)
            {
                _context.Graphics.Remove(graphics);
                _context.SaveChanges();
            }
        }
    }
}
