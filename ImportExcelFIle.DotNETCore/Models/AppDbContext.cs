using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<ZoneMap_New> ZoneMap_New { get; set; }
        public DbSet<ZipCluster_New> ZipCluster_New { get; set; }
        public DbSet<OverNightMap> OverNightMap { get; set; }
        //  public DbSet<ZIPCluster> ZIPCluster { get; set; }
        // public DbSet<ZoneMap> ZoneMap { get; set; }

    }
    }


