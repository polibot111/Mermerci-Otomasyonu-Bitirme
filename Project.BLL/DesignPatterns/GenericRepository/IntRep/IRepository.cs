using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesingPatterns.GenericRepository.IntRep
{
    public interface IRepository<T>where T:BaseEntity
    {
        //List Commnads
        List<T> GetAll();
        List<T> GetPassives();
        List<T> GetModifieds();
        List<T> GetActives();

        //Modification Comnands
        void Add(T item);
        void AddRange(List<T> item);
        void Delete(T item);
        void DeleteRange(List<T> item);
        void Destroy(T item);
        void DestroyRange(List<T> item);
        void Update(T item);
        void UpdateRange(List<T> item);

        //ExperssionCommands
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);

        //Find Command
        T Find(int id);

    }
}
