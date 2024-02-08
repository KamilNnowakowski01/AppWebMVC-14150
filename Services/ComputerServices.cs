using AppDataBase.Data;
using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class ComputerServices : IComputerServices
    {
        private readonly ApplicationDbContext _context;

        public ComputerServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Computer> GetAll()
        {
            var computers =
                _context.Computers
               .Include(b => b.Producer)
               .Include(b => b.ComputersGraphics)
                    .ThenInclude(ba => ba.Graphics)
               .ToList();

            return computers;
        }

        public Computer? GetById(int id)
        {
            var computer =
                _context.Computers
               .Include(b => b.Producer)
               .Include(b => b.ComputersGraphics)
                    .ThenInclude(ba => ba.Graphics)
               .FirstOrDefault(p => p.ComputerId == id);

            return computer;
        }

        public void Create(Computer computer)
        {
            if (computer == null)
            {
                throw new ArgumentNullException(nameof(computer));
            }

            _context.Computers.Add(computer);
            _context.SaveChanges();
        }

        public void Update(Computer computer)
        {
            if (computer == null)
            {
                throw new ArgumentNullException(nameof(computer));
            }

            _context.Computers.Update(computer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var computer = _context.Computers.Find(id);
            if (computer != null)
            {
                _context.Computers.Remove(computer);
                _context.SaveChanges();
            }
        }
    }
}
