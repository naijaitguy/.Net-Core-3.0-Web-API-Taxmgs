using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxtMgsApi.Model;

namespace TaxtMgsApi.DAL
{
  public  interface IUnitOfWork
    {

        GenericRepository<Admin> Admin_Repository { get; }
        GenericRepository<ContactRecord> Contact_Repository { get; }
        GenericRepository<PaymentRecord> Payment_Repository { get; }
        GenericRepository<TaxRegistration> TaxReg_Repository { get; }
        GenericRepository<TaxApplication> TaxApp_Repository { get; }
        Task Commit();
        void RollBack();

    }
}
