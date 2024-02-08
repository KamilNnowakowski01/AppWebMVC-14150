using AppDataBase.Data;
using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace AppWebMVC.Services
{
    public class ProducerServices : IProducerServices
    {
        private readonly ApplicationDbContext _context;

        public ProducerServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Producer> GetAll()
        {
            var producer =
                _context.Producers
                .Include(b => b.Computers)
                .ToList();

            return producer;
        }
        public Producer? GetById(int id)
        {
            var producer =
                _context.Producers
                .Include(b => b.Computers)
                .FirstOrDefault(p => p.ProducerId == id);

            return producer;
        }

        public void Create(Producer producer)
        {
            if (producer == null)
            {
                throw new ArgumentNullException(nameof(producer));
            }

            _context.Producers.Add(producer);
            _context.SaveChanges();
        }

        public void Update(Producer producer)
        {
            if (producer == null)
            {
                throw new ArgumentNullException(nameof(producer));
            }

            _context.Producers.Update(producer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var producer = _context.Producers
            .Include(p => p.Computers)
            .FirstOrDefault(p => p.ProducerId == id);

            if (producer != null)
            {
                _context.Computers.RemoveRange(producer.Computers);
                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
        }
    }
}
