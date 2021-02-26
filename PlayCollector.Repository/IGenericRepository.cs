using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace PlayCollector.Repository
{
    public interface IGenericRepository<TEntity> where TEntity: class, new()
    {
        //Notwendige Methoden
        //Alle Methoden Synchron und Asynchron
        //Alle Datensätze lesen
        //Eindeutigen Datensatz lesen
        //Datensätze gefilter lesen
        //Daten einfügen
        //Daten ändern
        //Daten löschen
     
        DbSet<TEntity> DbSet { get; }

        event EventHandler<RepositoryEventArgs> InsertEvent;


        //Read methodes
        ICollection<TEntity> Select();
        Task<ICollection<TEntity>> SelectAsync();
        TEntity SelectById(object key);
        Task<TEntity> SelectByIdAsync(object key);

        //Insert methodes
        TEntity Insert(TEntity t);
        Task<TEntity> InsertAsync(TEntity t);

        //Update methodes
        TEntity Update(TEntity t, object key);
        Task<TEntity> UpdateAsync(TEntity t, object key);


        //Delete methodes
        void Delete(object key);
        Task DeleteAsync(object key);



    }
}
