using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class ZIPCluster
    {
        [Key]
        public int ZipClusterId { get; set; }
        public int CarrierId { get; set; }
        public string FirstZIP { get; set; }
        public string LastZIP { get; set; }
    }
}
