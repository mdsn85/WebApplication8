using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication8.Models.Repository
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> Get();
        Material GetById(int id, bool icludeRelated = true);
        void Insert(Material material);
        void Update(Material material);
        void Delete(Material material);

        float AvailableQty(Material material);
        void ReserveQty(Material material, float qty);
        void ReleaseReserveQty(Material material, float qty);

        void Save();


    }
}