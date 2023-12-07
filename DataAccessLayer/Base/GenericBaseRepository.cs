using DataAccessLayer.RepositoryEvents;
using EntityLayer.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base
{
    public abstract class GenericBaseRepository<T> where T : class
    {
        protected ADC db;
        protected DbSet<T> Table;
        protected IServiceProvider serviceProvider;
        public event Action<T, RepositoryEventArgs> OnCreate;
        public event Action<IEnumerable<T>, RepositoryEventArgs> OnCreateRange;
        public event Action<T, RepositoryEventArgs> OnUpdate;
        public event Action<IEnumerable<T>, RepositoryEventArgs> OnUpdateRange;
        public event Action<T, RepositoryEventArgs> OnDelete;
        public event Action<IEnumerable<T>, RepositoryEventArgs> OnDeleteRange;
        public GenericBaseRepository(IServiceProvider serviceProvider, ADC db)
        {
            this.serviceProvider = serviceProvider;
            this.db = db;
            this.Table = db.Set<T>();
            OnDeleteRange= OnUpdateRange =OnCreateRange = (_, __) => { };
            OnCreate = OnUpdate = OnDelete = (_, __) => { };
            RepositoryEventSetter<T>.SetRepositoryEvents(this);
        }
        public virtual string? Create(T t)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnCreate(t, eventargs);
            if (!eventargs.PassTheAction && !eventargs.IsCancelled)
                Table.Add(t);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            return "Action is canceled";
        }
        public virtual string? CreateRange(IEnumerable<T> values)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnCreateRange(values, eventargs);
            if (!eventargs.PassTheAction && !eventargs.IsCancelled)
                Table.AddRange(values);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            return "Action is canceled";
        }
        public virtual T? Get(object id)
        {
            return Table.Find(id);
        }
        public virtual bool Get(object id, out T? t)
        {
            t = this.Get(id);
            return t != null;
        }
        public virtual T GetWhere(Expression<Func<T, bool>> expression)
        {
            return Table.FirstOrDefault(expression);
        }
        public virtual IQueryable<T> GetAll(int? index = null, int? count = null)
        {
            var query = Table.AsQueryable();
            if (index != null)
                query = query.Skip((int)index);
            if (count != null)
                query = query.Take((int)count);
            return query;
        }
        public virtual bool Exist(Expression<Func<T, bool>> expression)
        {
            return Table.Any(expression);
        }
        public virtual string? Update(T t)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnUpdate(t, eventargs);
            if (!eventargs.PassTheAction && !eventargs.IsCancelled)
                db.Set<T>().Update(t);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            return "Action is canceled";
        }
        public virtual string? UpdateRange(IEnumerable<T> values)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnUpdateRange(values, eventargs);
            if (!eventargs.PassTheAction && !eventargs.IsCancelled)
                Table.UpdateRange(values);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            return "Action is canceled";
        }
        public virtual string? Delete(T t)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnDelete(t, eventargs);
            if (!eventargs.PassTheAction)
                db.Set<T>().Update(t);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            else
                return "Action Canceled";
        }
        public virtual string? Delete(object id)
        {
            var val = db.Set<T>().Find(id);
            if (val != null)
                return Delete(val);
            return null;
        }
        public virtual string? DeleteRange(IEnumerable<T> values)
        {
            var eventargs = new RepositoryEventArgs();
            this.OnDeleteRange(values, eventargs);
            if (!eventargs.PassTheAction)
                db.Set<T>().RemoveRange(values);
            if (!eventargs.IsCancelled)
                return SaveChanges();
            else
                return "Action Canceled";
        }
        public virtual string? SaveChanges()
        {
            string? val = null;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                val = ex.Message;
            }
            return val;
        }
    }
}
