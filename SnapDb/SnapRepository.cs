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


        private IEnumerable<T> records;
        internal IEnumerable<T> Records
        {
            get
            {
                return records ?? (records = snapStore.LoadRecords<T>());
            }
        }


        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
