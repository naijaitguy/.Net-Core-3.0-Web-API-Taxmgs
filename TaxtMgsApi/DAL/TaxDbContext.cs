using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxtMgsApi.Model;

namespace TaxtMgsApi.DAL
{
    public class TaxDbContext :DbContext 
    {

        public TaxDbContext(DbContextOptions<TaxDbContext>options) :base(options) { }


     public   DbSet<Admin> Admins { get; set; }

     public DbSet<ContactRecord> ContactRecords { get; set; }

     public DbSet<TaxRegistration> TaxRegistrations { get; set; }

     public DbSet<TaxApplication> TaxApplications { get; set; }

     public DbSet<PaymentRecord> PaymentRecords { get; set; }

    }
}
