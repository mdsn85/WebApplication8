using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication8.Models

{
    public interface IUnitOfWork
    {

         Task SaveChanges();
    }
}
