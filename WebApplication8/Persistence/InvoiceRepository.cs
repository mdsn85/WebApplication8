using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication8.Models;
using WebApplication8.Models.Repository;

using Microsoft.AspNet.Identity;

using System.Data;
using System.Data.Entity;

using System.Net;

using System.Web.Mvc;
using WebApplication8.Models.Fillter;



namespace WebApplication8.Persistence
{
    public class InvoiceRepository:IInvoiceRepository,IDisposable
    {
       public static string BankDetails = "Company's Bank Details \r\n" +
                                    "Bank Name : RAK BANK \r\n" +
                                    "A/c No. : '0292856640001 \r\n" +
                                    "IBAN : AE030400000292856640001 \r\n" +
                                    "Branch & Swift Code: IBN BATTUTA & NRAKAEAK ";
        public static string Declaration = "Terms and Conditions:  \r\n" +
                                    "(1) Cheques to be made in the name of company only. \r\n" +
                                    "(2) Any claims after 2 days of delivery shall not be \r\n" +
                                    "acceptable.Kindly check goods upon receipt.";
        private readonly ApplicationDbContext _DbContext;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }



        public IEnumerable<Invoice> GetAll(InvoiceFillter fillter)
        {
            var invoices = _DbContext.Invoices.Include(i => i.Customer).Include(i => i.Project).Include(i => i.InvoiceProducts).AsQueryable();
            if(!string.IsNullOrEmpty( fillter.SearchCode))
            {
                invoices = invoices.Where(i => i.Code== fillter.SearchCode.ToUpper());
            }
            if (!string.IsNullOrEmpty(fillter.ProjectId))
            {
                int qqqq = int.Parse(fillter.ProjectId);
                invoices= invoices.Where(i => i.ProjectId == qqqq);
            }
            if (!string.IsNullOrEmpty(fillter.CustomerId))
            {
                int qqqq = int.Parse(fillter.CustomerId);
                invoices= invoices.Where(i => i.CustomerId == qqqq);
            }
            if (!String.IsNullOrEmpty(fillter.StartDate))
            {
                if (!String.IsNullOrEmpty(fillter.EndDate))
                {
                    DateTime ddStart = DateTime.ParseExact(fillter.StartDate, "yyyy-MM-dd", null);
                    DateTime ddEnd = DateTime.ParseExact(fillter.EndDate, "yyyy-MM-dd", null);
                    ddEnd = ddEnd.AddDays(1);
                    invoices = invoices.Where(cb => cb.InvoiceDate >= ddStart && cb.InvoiceDate < ddEnd);
                }
                else
                {
                    //on selected day
                    DateTime ddStart = DateTime.ParseExact(fillter.StartDate, "yyyy-MM-dd", null);
                    DateTime ddEnd = ddStart.AddDays(1);
                    invoices = invoices.Where(cb => cb.InvoiceDate >= ddStart && cb.InvoiceDate < ddEnd);
                }
            }

            return invoices.ToList();
        }

        public Invoice Get(int id)
        {
            return _DbContext.Invoices.Include(i => i.Customer).Include(i => i.Project).Where(i=>i.InvoiceId==id).Include(i=>i.InvoiceProducts)

                .FirstOrDefault(); 
        }

        public int Add(Invoice entity, List<InvoiceProduct> Products)
        {
            entity.StampDate = DateTime.Now;
            entity.CreateDate = DateTime.Now;


            List<InvoiceProduct> Linklist = new List<InvoiceProduct>();
            foreach (var Product in Products)
            {
                Linklist.Add(new InvoiceProduct
                {
                    Invoice = entity,
                    Product = _DbContext.Products.Find(Product.ProductId),
                    Qty =Product.Qty,
                    Price = Product.Price
                });
            }
            if (Linklist.Count() > 0)
            {
                entity.InvoiceProducts = Linklist;
            }
            _DbContext.Invoices.Add(entity);
            return entity.InvoiceId;
        }

        public bool Delete(int id)
        {
            var invoice =  _DbContext.Invoices.Find(id);
            _DbContext.Invoices.Remove(invoice);
            return true;
        }





        public  bool Update(Invoice entity)
        {
            var invoice = this.Get(entity.InvoiceId);
            _DbContext.Entry(invoice).CurrentValues.SetValues(entity);

            return true;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public string getBank()
        {
            return BankDetails;
        }

        public string getDeclaration()
        {
            return Declaration;
        }
    }
}