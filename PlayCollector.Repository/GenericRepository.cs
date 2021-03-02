using log4net;
using PlayCollector.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace PlayCollector.Repository
{

    public class GenericRepository<TEntiy, TModel> : IGenericRepository<TEntiy>
        where TEntiy: class, new()
        where TModel: DbContext, new()
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GenericRepository<TEntiy, TModel>));


        //Fields
        public DbContext _context;
        private DbSet<TEntiy> _dbSet;


        //Events
        //1. Event deklarieren
        public event EventHandler<RepositoryEventArgs> InsertEvent;


        //Properties
        public DbSet<TEntiy> DbSet
        {
            get
            {
                return _dbSet;
            }
        }




        //Constructor
        public GenericRepository()
        {
            Init(new TModel());
        }

        public GenericRepository(DbContext context)
        {
            Init(context);
        }




        //Methodes
        private void Init(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntiy>();
        }



        //Read methodes
        public ICollection<TEntiy> Select()
        {
            try
            {
                var resultList = _dbSet.ToList();
                log.Debug(string.Format("{0}: {1} records found", MethodBase.GetCurrentMethod().Name, resultList.Count));
                return resultList;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}: Error occured", MethodBase.GetCurrentMethod().Name), ex);
                return null;
            }
           
        }

        public async Task<ICollection<TEntiy>> SelectAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntiy SelectById(object key)
        {
            return _dbSet.Find(key);
        }

       public async Task<TEntiy> SelectByIdAsync(object key)
        {
            return await _dbSet.FindAsync(key);
        }






        //Insert methodes
        public TEntiy Insert(TEntiy t)
        {
            if(t!=null)
            {
                EntityHelper.SetObjectProperty("Created", DateTime.Now, t);
                _dbSet.Add(t);
                _context.SaveChanges();
                //OnInsert(new RepositoryEventArgs(t.GetType().Name, _context.Entry(t).Property("Id").CurrentValue.ToString()));
                OnInsert(new RepositoryEventArgs(t.GetType().Name, GetKeyValue(t)));
            }
            return t;
        }

        public async Task<TEntiy> InsertAsync(TEntiy t)
        {
            if (t != null)
            {
                EntityHelper.SetObjectProperty("Created", DateTime.Now, t);
                _dbSet.Add(t);
                await _context.SaveChangesAsync();
            }
            return t;

        }




        //Update methodes
        public TEntiy Update(TEntiy t, object key)
        {
            try
            {
                TEntiy tExisting = _dbSet.Find(key);
                if (tExisting != null)
                {
                    EntityHelper.SetObjectProperty("Updated", DateTime.Now, tExisting);
                    _context.Entry(tExisting).CurrentValues.SetValues(t);
                    _context.SaveChanges();
                    _context.Entry(tExisting).Reload();
                    log.Debug(string.Format("{0}: Record {1} updated", MethodBase.GetCurrentMethod().Name, key));
                }
                else
                {
                    log.Warn(string.Format("{0}: Record {1} not found", MethodBase.GetCurrentMethod().Name, key));
                }
                return tExisting;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}: Error occured", MethodBase.GetCurrentMethod().Name), ex);
                return null;
            }

            
        }

        public async Task<TEntiy> UpdateAsync(TEntiy t, object key)
        {
            TEntiy tExisting = await _dbSet.FindAsync(key);
            if (tExisting != null)
            {
                EntityHelper.SetObjectProperty("Updated", DateTime.Now, tExisting);
                _context.Entry(tExisting).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
                await _context.Entry(tExisting).ReloadAsync();
            }
            return tExisting;
        }




        //Delete methodes
        public void Delete(object key)
        {
            TEntiy tExisting = _dbSet.Find(key);
            if (tExisting != null)
            {
                _dbSet.Remove(tExisting);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync(object key)
        {
            TEntiy tExisting = await _dbSet.FindAsync(key);
            if (tExisting != null)
            {
                _dbSet.Remove(tExisting);
                await _context.SaveChangesAsync();
            }
        }





        //Internal Methodes
        protected virtual void OnInsert(RepositoryEventArgs e)
        {
            EventHandler<RepositoryEventArgs> insertEvent = InsertEvent;
            if(insertEvent!=null)
            {
                InsertEvent(this, e);
            }
        }


        //Methode zum Festellen des Primärschlüssels und dessen Wert
        private string GetKeyValue(TEntiy t)
        {
            string keyField = string.Empty;
            string keyValue = string.Empty;
            
            var entry = _context.Entry(t);
            var type = t.GetType();

            var objectSet = ((IObjectContextAdapter)_context).ObjectContext.CreateObjectSet<TEntiy>();
            string[] keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
            keyField = keyNames[0];
            keyValue = _context.Entry(t).Property(keyField).CurrentValue.ToString();
            return keyValue;
        }





    }
}
