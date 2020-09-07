using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class Zone
    {
        [Key]
        public int ZoneId { get; set; }
        public int CarrierId { get; set; }
        public int ZoneCode { get; set; }
    }
}
