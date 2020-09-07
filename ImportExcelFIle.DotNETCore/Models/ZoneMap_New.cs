using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ImportExcelFIle.DotNETCore.Models
{
    public class ZoneMap_New
    {
        [Key]
        public int ZoneMapId { get; set; }
        public int CarrierId { get; set; }
        public int FromZIPClusterId { get; set; }
        public int ToZIPClusterId { get; set; }
        public int? ZoneId { get; set; }
    }

}
