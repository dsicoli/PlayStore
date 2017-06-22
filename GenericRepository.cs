using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using PlayStore.Model;

namespace PlayStore.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly PlayStoreDBContext _appStoreDBContext;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(PlayStoreDBContext appStoreDBContext)
        {
            _appStoreDBContext = appStoreDBContext;
            _dbSet = appStoreDBContext.Set<TEntity>();
        }

        public IList<TEntity> GetAll()//What is the advantage of using 
        //IEnumerable<TEntity> rather than List<TEntity>?
        {
            return _dbSet.ToList<TEntity>();
        }



        public void AddEntity(TEntity inputEntity)
        {
            _dbSet.Add(inputEntity);
        }

       
        public TEntity AddEntityReturned(TEntity inputEntity)
        {
            //_dbSet.Add(inputEntity);
            TEntity entity = _dbSet.Add(inputEntity).Entity;
            return entity;
        }


        public void Update(TEntity inputEntity)
        {
            if( _appStoreDBContext.Set<TEntity>().Local.Any<TEntity>( e => e == inputEntity ) )
            {
                _dbSet.Update(inputEntity);
            }else
            {
            throw new KeyNotFoundException("The entity you tried to update does not exist");
            throw new Exception();
            }
            
        }

        public void Patch(TEntity inputEntity, Expression<Func<TEntity,bool>> predicate = null)
        {
            if( predicate == null )
            {
                if( _appStoreDBContext.Set<TEntity>().Local.Any<TEntity>( e => e == inputEntity ) )
                {
                    _dbSet.Attach(inputEntity);
                    _appStoreDBContext.Entry(inputEntity).State = EntityState.Modified;
                }else
                throw new KeyNotFoundException("The entity you tried to patch does not exist");
            }else
            {
                DbSet<TEntity> entitySet = _dbSet;
                IList<TEntity> result = entitySet.Where(predicate).ToList<TEntity>();

                foreach(TEntity element in result)
                {
                    entitySet.Update(inputEntity);
                }

            }
   
        }


        public TEntity GetById(int recordId)
        {
            if(_dbSet.Find(recordId)!=null)
                return _dbSet.Find(recordId);
                else
                throw new KeyNotFoundException("The ID you entered corresponds to no entity");
        }

        public IList<TEntity> GetByLambda( Expression<Func<TEntity,bool>> predicate )
        {
                DbSet<TEntity> set = _appStoreDBContext.Set<TEntity>();

                IList<TEntity> result = new List<TEntity>();

                result = set.Where(predicate).ToList();

                return result;
        }

        public void DeleteSingleEntity(TEntity inputEntity)
        {

            if( _appStoreDBContext.Set<TEntity>().Local.Any<TEntity>( e => e == inputEntity ) )
            {
                if(_appStoreDBContext.Entry(inputEntity).State==EntityState.Detached)
                {
                    _dbSet.Attach(inputEntity);
                }
                _dbSet.Remove(inputEntity);
            }else
            throw new KeyNotFoundException("The entity you tried to delete does not exist");
        }

        public void Delete(Expression<Func<TEntity,bool>> predicate)
        {
            DbSet<TEntity> entitySet = _appStoreDBContext.Set<TEntity>(); //This is the whole entity-set corresponding to the TEntity model
            IList<TEntity> waste = new List<TEntity>();
            
            waste = entitySet.Where(predicate).ToList();

            foreach(TEntity element in waste)
                DeleteSingleEntity(element);
        }

        public void SaveRepository()
        {
            _appStoreDBContext.SaveChanges();
        }
    }
}
