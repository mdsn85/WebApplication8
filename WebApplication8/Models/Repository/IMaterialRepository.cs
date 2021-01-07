using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication8.Models.Repository
{
    public interface IMaterialRepository
    {
        void Insert(Material material);
        void Update(Material material);
        void Delete(Material material);
        Task<IEnumerable<Material>> Get();
        Task<List<Material>> GetById(int id, bool icludeRelated = true);

        float AvailableQty(int materialId);
        void ReserveQty(int materialId, float qty);
        void ReleaseReserveQty(int materialId, float qty);

        void Save();


    }
}