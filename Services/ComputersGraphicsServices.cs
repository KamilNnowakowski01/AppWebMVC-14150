using AppDataBase.Data;
using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebMVC.Services
{
    public class ComputersGraphicsServices : IComputersGraphicsServices
    {
        private readonly ApplicationDbContext _context;

        public ComputersGraphicsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(int computerId, int graphicsId)
        {
            var computersGraphics = new ComputersGraphics
            {
                ComputerId = computerId,
                GraphicsId = graphicsId
            };

            await _context.AddAsync(computersGraphics);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int computerId)
        {
            var graphicsEntries = await _context.ComputersGraphics
                                                .Where(cg => cg.ComputerId == computerId)
                                                .ToListAsync();

            if (graphicsEntries.Any())
            {
                _context.ComputersGraphics.RemoveRange(graphicsEntries);
                await _context.SaveChangesAsync();
            }
        }
    }
}
