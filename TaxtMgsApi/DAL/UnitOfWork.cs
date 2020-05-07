using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxtMgsApi.Model;
using TaxtMgsApi.DAL;

namespace TaxtMgsApi.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly TaxDbContext context;
        public UnitOfWork(TaxDbContext  _context)
        {
            context = _context;

        }
        private GenericRepository<Admin> Admin_genericRepository;
        private GenericRepository<ContactRecord> Contact_genericRepository;
        private GenericRepository<PaymentRecord> Payment_genericRepository;
        private GenericRepository<TaxRegistration> TaxReg_genericRepository;
        private GenericRepository<TaxApplication> TaxApplication_Repository;

        public GenericRepository<Admin> Admin_Repository
        {

            get
            {
                if(this.Admin_genericRepository == null)
                {
                    this.Admin_genericRepository = new GenericRepository<Admin>(context);
                 
                }

                return Admin_genericRepository;

            }
        }


        public GenericRepository<ContactRecord> Contact_Repository{

            get
            {
                if (this.Contact_genericRepository == null) {
                   this.Contact_genericRepository = new GenericRepository<ContactRecord>(context);
                
                }

                return Contact_genericRepository;
            }
        }


        public GenericRepository<PaymentRecord> Payment_Repository
        {
            get
            {

                if( Payment_genericRepository == null)
                {
                    this.Payment_genericRepository = new GenericRepository<PaymentRecord>(context);

                }

                return Payment_genericRepository;
            }
        }

        public GenericRepository<TaxRegistration> TaxReg_Repository { get
            {
                if(TaxReg_genericRepository == null)
                {
                    this.TaxReg_genericRepository = new GenericRepository<TaxRegistration>(context);

                }

                return TaxReg_genericRepository;
            } 
        
        }

        public GenericRepository<TaxApplication> TaxApp_Repository { get
            {  
                if(this.TaxApplication_Repository == null)
                {
                    this.TaxApplication_Repository = new GenericRepository<TaxApplication>(context);

                }
                return TaxApplication_Repository;
            } 
        
        
        }

        public async Task  Commit() {

          await  context.SaveChangesAsync();
        }

        public  void RollBack() {

            context.Dispose();
        }




    }
}
