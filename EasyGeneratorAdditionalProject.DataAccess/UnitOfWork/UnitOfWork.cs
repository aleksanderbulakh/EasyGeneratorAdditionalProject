using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.DataAccess.Providers;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Course> _courseRepository;

        public UnitOfWork(DatabaseContext context, IRepository<Role> roleRepository, IRepository<User> userRepository, IRepository<Course> courseRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository; 
        }

        public IRepository<Role> roleRepository
        {
            get
            {
                return _roleRepository;
            }
        }

        public IRepository<User> userRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public IRepository<Course> courseRepository
        {
            get
            {
                return _courseRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
