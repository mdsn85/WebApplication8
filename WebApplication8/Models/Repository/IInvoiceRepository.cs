using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication8.Models.Fillter;

namespace WebApplication8.Models.Repository
{
    public interface IInvoiceRepository
    {

        Invoice Get(int id);
        IEnumerable<Invoice> GetAll(InvoiceFillter fillter);
        int Add(Invoice entity, List<InvoiceProduct> Products);
        bool Delete(int id);
        bool Update(Invoice bookEntity);
        void Save();

        string getBank();
        string getDeclaration();
    }
}