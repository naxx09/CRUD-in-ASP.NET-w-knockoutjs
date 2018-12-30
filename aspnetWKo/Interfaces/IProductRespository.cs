using aspnetWKo.Models;
using System.Collections.Generic;
namespace aspnetWKo.Interfaces
{
    interface IProductRepository
    {
        IEnumerable<tblProduct> GetAll();
        tblProduct Get(int id);
        tblProduct Add(tblProduct item);
        bool Update(tblProduct item);
        bool Delete(int id);
    }
}