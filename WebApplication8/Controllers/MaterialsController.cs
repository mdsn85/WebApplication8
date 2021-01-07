using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Controllers.Resources;
using WebApplication8.Models;
using WebApplication8.Models.Repository;
using WebApplication8.Persistence;

namespace WebApplication8.Controllers
{
    public class MaterialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IMaterialRepository materialRepository;

        public MaterialsController()
        {
            this.materialRepository = new MaterialRepository(new ApplicationDbContext());
        }
        // GET: Materials
        [Authorize(Roles = RoleNames.ROLE_MaterialView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public async Task<ActionResult> Index()
        {
            ViewBag.title1 = "Material List";
            IEnumerable<Material> materials =await materialRepository.Get();

            return View(materials.ToList());
        }

        public ActionResult MaterialMovment(int id)
        {
            var materials = db.Materials.Include(m => m.Unit);
            List<StockIssueDetail> sid = db.StockIssueDetails.Where(s => s.MaterialId == id).ToList();
            Material mm  = db.Materials.Find(id);
            @ViewBag.Code = mm.code;
            @ViewBag.Name = mm.Name;

            @ViewBag.Dimension = mm.Dimension;

            @ViewBag.OpenQty = mm.OpeningQty;

            @ViewBag.Date = mm.CreatedDate;

            @ViewBag.AvgPrice = mm.AvgPrice;

            @ViewBag.latestPrice = mm.latestPrice;

            @ViewBag.qty = mm.qty;

            return View(sid.ToList());
        }

        public ActionResult IndexShort(string sortOrder, string searchStringName, String searchStringCode, String searchStringBarcode)
        {

            List<Material> materials = db.Materials.Include(m => m.Unit).ToList();


            ViewBag.AvalableQty = String.IsNullOrEmpty(sortOrder) ? "AvalableQty" : "";//client
            ViewBag.CustomerGroupsSortParm = String.IsNullOrEmpty(sortOrder) ? "CustomerGroups_desc" : "";
            ViewBag.TradeLicenceNoSortParm = String.IsNullOrEmpty(sortOrder) ? "TradeLicenceNo_desc" : "";




            int search = 0;
            if (!String.IsNullOrEmpty(searchStringName))
            {
                materials = materials.Where(s => s.Name.Contains(searchStringName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                search = 1;
            }
            if (!String.IsNullOrEmpty(searchStringCode))
            {
                materials = materials.Where(s => s.code.Contains(searchStringCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
                search = 1;
            }
            if (!String.IsNullOrEmpty(searchStringBarcode))
            {
                materials = materials.Where(s => s.barcode.Contains(searchStringBarcode)).ToList();
                search = 1;
            }

            switch (sortOrder)
            {
                case "AvalableQty":
                    materials = materials.OrderBy(s => s.AvalableQty).ToList();
                    break;


            }
            return View(materials.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Materials/Details/5
        public JsonResult get(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Material material = db.Materials.Find(id);

            
            MaterialFull mf = new MaterialFull();

            //object pass to function by refrence
            MappingMaterial(material, mf);


            return Json(mf
            , JsonRequestBehavior.AllowGet);
        }

        public void MappingMaterial(Material material, MaterialFull mf)
        {
            mf.MaterialId = material.MaterialId;
            mf.Name = material.Name;
            mf.latestPrice = material.latestPrice;

            mf.Dimension = material.Dimension ?? "";
            mf.AvalableQty = material.AvalableQty;

            mf.Unit = new KeyValuePair { Id = material.UnitId, Name = material.Unit.Name };
            if (material.MaterialCategoryId != null)
            {
                mf.MaterialCategory = new KeyValuePair { Id = material.MaterialCategoryId, Name = material.MaterialCategory.Name };
            }
            if (material.MaterialTypeId != null)
            {
                mf.MaterialType = new KeyValuePair { Id = material.MaterialTypeId, Name = material.MaterialType.Name };
            }
            mf.WareHouse = new KeyValuePair { Id = material.WareHouseId, Name = material.WareHouse.Name };
        }

        // GET: Materials/Create
        [Authorize(Roles = RoleNames.ROLE_MaterialCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name");
            ViewBag.UnitId2 = new SelectList(db.Units, "UnitId", "Name");
            ViewBag.WareHouseId = new SelectList(db.Warehouses, "WarehouseId", "Name");

            ViewBag.MaterialCategoryId = new SelectList(db.MaterialCategorys, "MaterialCategoryId", "Name");

            ViewBag.MaterialTypeId = new SelectList(db.MaterialTypes, "MaterialTypeId", "Name");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaterialCategoryId,MaterialTypeId,Code,MaterialId,Name,latestPrice,MinReOrder,Description,qty,AvgPrice,barcode,UnitId,UnitId2,convert,WareHouseId,Dimension")] Material material)
        {
            if (ModelState.IsValid)
            {

                if (material.AvgPrice == null)
                {
                    material.AvgPrice = 0;
                }

                if (material.latestPrice == null)
                {
                    material.latestPrice = 0;
                }

                material.qty = material.OpeningQty;

                material.CreatedDate = DateTime.Now;
                material.Resevedqty = 0;
                if(material.MinReOrder == null)
                {
                    material.MinReOrder = 0;
                }
                db.Materials.Add(material);
                db.SaveChanges();

                MaterialUnit mu = new MaterialUnit();
                mu.MaterialId = material.MaterialId;
                mu.UnitId = material.UnitId;
                mu.convert = 1;
                db.MaterialUnits.Add(mu);

                if (material.UnitId2 != null)
                {
                    MaterialUnit mu2 = new MaterialUnit();
                    mu2.MaterialId = material.MaterialId;
                    mu2.UnitId = material.UnitId2 ?? 0;
                    mu2.convert = material.convert ?? 0;
                    db.MaterialUnits.Add(mu2);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", material.UnitId);
            ViewBag.UnitId2 = new SelectList(db.Units, "UnitId", "Name", material.UnitId2);

            ViewBag.WareHouseId = new SelectList(db.Warehouses, "WarehouseId", "Name",material.WareHouseId);

            ViewBag.MaterialCategoryId = new SelectList(db.MaterialCategorys, "MaterialCategoryId", "Name");

            ViewBag.MaterialTypeId = new SelectList(db.MaterialTypes, "MaterialTypeId", "Name");
            return View(material);
        }

        // GET: Materials/Edit/5
        [Authorize(Roles = RoleNames.ROLE_MaterialEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.WareHouseId = new SelectList(db.Warehouses, "WarehouseId", "Name", material.WareHouseId);
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name");
            ViewBag.UnitId2 = new SelectList(db.Units, "UnitId", "Name", material.UnitId2);

            ViewBag.MaterialCategoryId = new SelectList(db.MaterialCategorys, "MaterialCategoryId", "Name", material.MaterialCategoryId);

            ViewBag.MaterialTypeId = new SelectList(db.MaterialTypes, "MaterialTypeId", "Name",material.MaterialTypeId);
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaterialCategoryId,MaterialTypeId,Code,CreatedDate,Resevedqty,barcode,Dimension,MaterialId,Name,MinReOrder,Description,qty,latestPrice,AvgPrice,UnitId,UnitId2,convert,WareHouseId")] Material material)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", material.UnitId);
            ViewBag.UnitId2 = new SelectList(db.Units, "UnitId", "Name", material.UnitId2);
            ViewBag.WareHouseId = new SelectList(db.Warehouses, "WarehouseId", "Name",material.WareHouseId);
            ViewBag.MaterialCategoryId = new SelectList(db.MaterialCategorys, "MaterialCategoryId", "Name", material.MaterialCategoryId);

            ViewBag.MaterialTypeId = new SelectList(db.MaterialTypes, "MaterialTypeId", "Name", material.MaterialTypeId);
            return View(material);
        }

        [HttpPost]
        public async Task<ActionResult> ImportFile()
        {
            return View("ImportFile");
        }



        private List<Material> GetDataFromCSVFile(Stream stream)
        {
            var empList = new List<Material>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names  
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            empList.Add(new Material()
                            {
                                MaterialId = Convert.ToInt32(objDataRow["MaterialId"].ToString()),
                                Name = objDataRow["Name"].ToString(),
                                Description = objDataRow["Description"].ToString(),
                                UnitId = int.Parse(objDataRow["Unit"].ToString()),
                                MinReOrder =Convert.ToInt16( objDataRow["MinReOrder"].ToString()),
                                qty = float.Parse(objDataRow["qty"].ToString()),
                                AvgPrice = float.Parse(objDataRow["AvgPrice"].ToString()),
                            });
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return empList;
        }



        // GET: Materials/Delete/5
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Materials.Find(id);
            foreach (var m in material.MaterialUnits.ToList())
            {
                db.MaterialUnits.Remove(m);
            }
            db.Materials.Remove(material);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
