using ClinicaVeterinaria.Data.Entities;

namespace ClinicaVeterinaria.Data
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        private readonly DataContext _context;

        public HistoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
