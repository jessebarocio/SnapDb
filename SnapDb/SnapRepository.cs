using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    public class SnapRepository<T>
    {
        private ISnapStore snapStore;


        public SnapRepository()
        {

        }

        internal SnapRepository(ISnapStore snapStore)
        {
            this.snapStore = snapStore;
        }


        private List<T> records;
        internal List<T> Records
        {
            get
            {
                return records ?? (records = snapStore.LoadRecords<T>().ToList());
            }
        }


        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var results = Records.AsQueryable();
            if(filter != null)
            {
                results = results.Where(filter);
            }
            return results;
        }

        public void Insert(T item)
        {
            Records.Add(item);
        }

        public void Delete(T item)
        {
            Records.Remove(item);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
