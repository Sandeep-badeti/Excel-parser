using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class OverNightMap
    { 
        [Key]
        public int Id { get; set; }
        public string Zipcode { get; set; }
        public string StartZip { get; set; }
        public string EndZip { get; set; }
    }
}
