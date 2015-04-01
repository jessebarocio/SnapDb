using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb
{
    /// <summary>
    /// A simple generic repository for performing CRUD operations on a SnapDb.
    /// </summary>
    /// <typeparam name="T">The desired type.</typeparam>
    public class SnapRepository<T>
    {
        private ISnapStore<T> snapStore;


        /// <summary>
        /// Creates a new SnapRepository using the given SnapDb file path. If the file does not exist it will be created.
        /// </summary>
        /// <param name="path">The path to the SnapDb file.</param>
        public SnapRepository(string path)
            : this(new FileSnapStore<T>(path)) { }

        /// <summary>
        /// Creates a new SnapRepository using the given ISnapStore.
        /// </summary>
        /// <param name="snapStore">The ISnapStore used to load/save records.</param>
        public SnapRepository(ISnapStore<T> snapStore)
        {
            this.snapStore = snapStore;
        }


        private List<T> records;
        /// <summary>
        /// The collection of all records in the SnapDb.
        /// </summary>
        internal List<T> Records
        {
            get
            {
                return records ?? (records = snapStore.LoadRecords().ToList());
            }
        }


        /// <summary>
        /// Gets the records that match the given filter. If no filter is passed all records will be returned.
        /// </summary>
        /// <param name="filter">The expression used to filter records.</param>
        /// <returns>An IQueryable of records matching the filter or all records if filter is null.</returns>
        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var results = Records.AsQueryable();
            if (filter != null)
            {
                results = results.Where(filter);
            }
            return results;
        }

        /// <summary>
        /// Inserts the item into the SnapDb.
        /// </summary>
        /// <remarks>You must call SaveChanges() before the record will be saved to the SnapDb store.</remarks>
        /// <param name="item">The item to insert.</param>
        public void Insert(T item)
        {
            Records.Add(item);
        }

        /// <summary>
        /// Removes an item from the SnapDb. No exception is thrown if the item is not found in the SnapDb.
        /// </summary>
        /// <remarks>You must call SaveChanges() before the record will be removed from the SnapDb store.</remarks>
        /// <param name="item">The item to remove.</param>
        public void Delete(T item)
        {
            Records.Remove(item);
        }

        /// <summary>
        /// Saves all changes to the SnapDb store.
        /// </summary>
        public void SaveChanges()
        {
            snapStore.SaveRecords(Records);
        }
    }
}
