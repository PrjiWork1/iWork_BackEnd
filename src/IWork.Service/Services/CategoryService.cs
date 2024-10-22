using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class CategoryService : BaseService<Category>
    {
        public CategoryService(DataContext context) : base(context)
        {
        }
    }
}
