using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClientBarometer.Common.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Common.Infrastructure.Repositories
{
    public abstract class RegisterRepository<T>: IRegisterRepository<T>  where T : class
    {
        protected readonly DbSet<T> DbSet;

        protected RegisterRepository(DbSet<T> dbSet)
        {
            DbSet = dbSet;
        }

        public virtual void RegisterNewRange(T[] entities)
        {
            if (entities.Select(ValidateModel).All(_ => _))
            {
                DbSet.AddRange(entities);
            }
        }

        public virtual void RegisterNew(T entity)
        {
            if (ValidateModel(entity))
            {
                DbSet.Add(entity);
            }
        }

        public virtual void RegisterDirtyRange(T[] entities)
        {
            if (entities.Select(ValidateModel).All(_ => _))
            {
                DbSet.UpdateRange(entities);
            }
        }

        public virtual void RegisterDirty(T entity)
        {
            if (ValidateModel(entity))
            {
                DbSet.Update(entity);
            }
        }

        public virtual void RegisterDelete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RegisterDeleteRange(T[] entities)
        {
            DbSet.RemoveRange(entities);
        }

        private static bool ValidateModel(T entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            if (isValid) return true;

            if (validationResults.Any())
            {
                throw new ArgumentException(validationResults[0].ErrorMessage);
            }

            throw new ArgumentException("Invalid model state.");
        }
    }
}
