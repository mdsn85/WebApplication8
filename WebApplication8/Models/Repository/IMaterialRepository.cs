using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication8.Models.Repository
{
    public interface IMaterialRepository
    {
        void Insert(Material Notification);
        void Update(Material Notification);
        void Delete(Material Notification);
        Task<IEnumerable<Material>> Get();
        Task<List<Material>> GetById(int id, bool icludeRelated = true);
    }
}