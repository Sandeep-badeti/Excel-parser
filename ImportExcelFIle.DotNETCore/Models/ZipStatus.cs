using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class ZipStatus
    {
        [Key]
        public int Id { get; set; }
        public string Zip { get; set; }
        public Boolean Status { get; set; }
    }
}
