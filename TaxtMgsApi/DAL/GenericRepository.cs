using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxtMgsApi.DAL
{
    public class GenericRepository<T>  :IGenericRepository <T> where T :class
    {

        internal TaxDbContext context;
        public GenericRepository( TaxDbContext _context)
        
        { this.context = _context; }

        public async  Task<List<T>> GetAll() {

            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(object Id) {

            if (Id != null) 
            
            { return await context.Set<T>().FindAsync(Id); }

            return null;
        }

        public async Task<bool> Add(T Entity) {

           await context.Set<T>().AddAsync(Entity);
            return true;
        }

        public void Update(T Entity) {

                 
            context.Set<T>().Attach(Entity);
            context.Entry(Entity).State = EntityState.Modified;
       
        
                
        
        }

        public async Task<bool> Delete(object Id) {

            T User = await context.Set<T>().FindAsync(Id);
            if (User != null)
            {
                context.Set<T>().Remove(User);
                return true;
            }
            return false;


        }






      public async  Task<List<T>> ExecuteQuery(string Query, object[] Parameter) {


       return await context.Set<T>().FromSqlRaw(Query, Parameter).ToListAsync();

          
                          
        }

        public async Task <T> ExecuteQuerySingle(string Query, object[] Parameter)
        {


            return await context.Set<T>().FromSqlRaw(Query, Parameter).SingleAsync();


        }


        public async  Task<int>   ExecuteCommand(string Query, object[] Parameter) {

            return  await  context.Database.ExecuteSqlRawAsync(Query, Parameter);


        }


    }
}
