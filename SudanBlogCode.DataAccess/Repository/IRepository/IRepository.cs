﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T GetById(int id);
        // void Update(T entity);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
