using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication8.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool? IsEnabled { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0xx xxxxxxx")]
        public string Mobile { get; set; }

        //[RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0x xxxxxxx")]
        //public string Telephone { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }




        public DbSet<Unit> Units { get; set; }
        public DbSet<Material> Materials { get; set; }

        public DbSet<Employee> Employees { get; set; }




        public System.Data.Entity.DbSet<CuttingSheet> CuttingSheets { get; set; }
        public System.Data.Entity.DbSet<CuttingSheetDetail> CuttingSheetDetails { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Warehouse> Warehouses { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Lpo> Lpoes { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.LpoDetail> LpoDetails { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.CreditTerm> CreditTerms { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.supplier> suppliers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.StockIssueType> StockIssueTypes { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.StockIssue> StockIssues { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.StockIssueDetail> StockIssueDetails { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.LpoTerms> LpoTerms { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.LpoSequense> LpoSequense { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.MaterialUnit> MaterialUnits { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.CuttingSheetSequense> CuttingSheetSequenses { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.LpoStatus> LpoStatus { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.SalesMan> SalesMen { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Project> Projects { get; set; }

        //public System.Data.Entity.DbSet<WebApplication8.Models.Project1> Project1 { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.ProjectPaymentTerm> ProjectPaymentTerms { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Designer> Designers { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.Project2> Project2 { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.CustomersType> CustomersTypes { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.SalesType> SalesTypes { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.ProjectStatus> ProjectStatus { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.ProjectFile> projectFiles { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.EmployeeUser> EmployeeUsers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Payment> Payments { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.StockIssueSequense> StockIsssueSequense { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Permission> Permissions { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.PermissionUser> PermissionUsers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.ApprovalType> ApprovalTypes { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.RequestApproval> RequestApprovals { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Quotation> Quotations { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.EmailServer> EmailServers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.SalesTeam> SalesTeam { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.CreditTermSupplier> CreditTermSuppliers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.MaterialCategory> MaterialCategorys { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.MaterialType> MaterialTypes { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.Area> Areas { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Notification1> Notifications { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.NotificationCategory> NotificationCategorys { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.NotificationCatUser> NotificationCatUsers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.CuttingSheetFile> CuttingSheetFiles { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.LpoLocation> LpoLocations { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.PaymentMoodSupplier> PaymentMoodSuppliers { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.LpoNature> LpoNatures { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.Invoice> Invoices { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.InvoiceSequense> InvoiceSequenses { get; set; }


        public System.Data.Entity.DbSet<WebApplication8.Models.ModeOfLead> ModeOfLeads { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.StatusOFLead> StatusOFLeads { get; set; }
        public System.Data.Entity.DbSet<WebApplication8.Models.Lead> Leads { get; set; }

        public System.Data.Entity.DbSet<WebApplication8.Models.Country> Countries { get; set; }



        public DbSet<LeadFile> LeadFiles { get; set; }

        public DbSet<NatureOfBusiness> NatureOfBusinesses { get; set; }

        public DbSet<QuotaionProduct> QuotaionProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<InvoiceProduct>()
           .HasKey(x => new { x.InvoiceId, x.ProductId });

            modelBuilder.Entity<InvoiceProduct>()
            .HasRequired(pt => pt.Invoice)
            .WithMany(p => p.InvoiceProducts)
            .HasForeignKey(pt => pt.InvoiceId);

            modelBuilder.Entity<InvoiceProduct>()
                .HasRequired(pt => pt.Product)
                .WithMany(t => t.InvoiceProducts)
                .HasForeignKey(pt => pt.ProductId);


        }

    }
}