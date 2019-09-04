using Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF
{
    public class ClonesDbContext : DbContext
    {
        public DbSet<Field> Fields { get; set; }
    }
}
