using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication8.Models;
using WebApplication8.Models.Repository;

namespace WebApplication8.Persistence
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext db;

        public MaterialRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Delete(Material Notification)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IEnumerable<Material>> Get()
        {
            return await db.Materials.Include(m=>m.Unit).ToListAsync();
        }



        public Material GetById(int id, bool icludeRelated = true)
        {
            return  db.Materials.Include(m => m.Unit).Where(m => m.MaterialId == id).FirstOrDefault();
        }

        public void Insert(Material Notification)
        {
            throw new NotImplementedException();
        }


        public void Update(Material Notification)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void ReleaseReserveQty(Material material, float qty)
        {
            float Resevedqty = material.Resevedqty ?? 0;
            material.Resevedqty = Resevedqty - qty;
        }

        public float AvailableQty(Material material)
        {
            float Resevedqty = material.Resevedqty ?? 0;
            float MinReOrder = material.MinReOrder ?? 0;
            float qty = material.qty ?? 0;
            return qty - Resevedqty - MinReOrder;
        }

        public void ReserveQty(Material material, float qty)
        {
            float Resevedqty = material.Resevedqty ?? 0;
            material.Resevedqty = Resevedqty + qty;
        }

    }
}