using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace EnvironmentCrime.Models
{
    public interface IErrandRepository
    {
        IQueryable<Errand> Errands { get; }
        Task<Errand> GetErrandDetail(int id);
    }
}
