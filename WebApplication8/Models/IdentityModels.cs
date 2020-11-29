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
        






    }
}