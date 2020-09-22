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
        public string OriginZip { get; set; }
        public string FromZip { get; set; }
        public string ToZip { get; set; }
    }
}
