using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class ExcelColumnsDTO
    {
       public string originClusterStartZip { get; set; }
        public string originClusterEndZip { get; set; }
        public string startZip { get; set; }
        public string endZip { get; set; }
        public string zone { get; set; }
    }
}
