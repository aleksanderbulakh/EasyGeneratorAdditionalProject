using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.DataAccess.Repositories;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }        

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
