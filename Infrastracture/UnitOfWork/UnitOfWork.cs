﻿using Core.Interfaces;
using Infrastracture.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {

        public DataContext _context { get; }
        private IGenericRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
