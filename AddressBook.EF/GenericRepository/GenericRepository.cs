using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.DB;
using System.Linq.Expressions;
using Address_Book.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.EF.GenericRepository
{
    public class GenericRepository<Tentity> where Tentity : class
    {
        private MyDbContext _db;
        private DbSet<Tentity> dbset;

        public GenericRepository(MyDbContext db)
        {
            this._db = db;
            dbset = _db.Set<Tentity>();
        }

        public virtual IEnumerable<Tentity> GetAllData()
        {
            try
            {
                IEnumerable<Tentity> query = dbset;
                return query.ToList();
            }
            catch (Exception ex)
            {
                string s = ex.GetType().Name;
                throw new System.ArgumentNullException("can't found data");
            }
        }

        public virtual Tentity GetDataById(object Id)
        {
            try
            {
                Tentity entity = dbset.Find(Id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("can not find data");
            }
        }

        public virtual IEnumerable<Tentity> GetDataByCondition(Expression<Func<Tentity, bool>> predicate)
        {
            try
            {
                IEnumerable<Tentity> listdata = dbset.Where(predicate);
                return listdata;
            }
            catch (Exception ex)
            {
                throw new System.ArgumentNullException("can't found data");
            }
        }

        public virtual bool Add(Tentity model)
        {
            try
            {
                dbset.Add(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool addrange(IEnumerable<Tentity> models)
        {
            try
            {
                dbset.AddRange(models);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            try
            {
                Tentity entity = dbset.Find(id);
                dbset.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Modify(Tentity model)
        {
            try
            {
                dbset.Update(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}