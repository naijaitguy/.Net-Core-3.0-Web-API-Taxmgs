using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxtMgsApi.DAL
{
    interface IGenericRepository<T> 
    {

      Task<  List<T>> GetAll();

             Task<List<T>> ExecuteQuery( string Query, object [] Parametere);

         

          Task  < int> ExecuteCommand(string Query, object[] Parameter);
         Task<T> GetById(object Id);

         Task<bool> Add(T Entity);

         void Update(T Entity);

         Task<bool> Delete(object Id);

    }
}
