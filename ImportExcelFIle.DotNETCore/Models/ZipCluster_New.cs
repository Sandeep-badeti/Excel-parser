using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImportExcelFIle.DotNETCore.Models
{
    public class ZipCluster_New
    {
            [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ZipClusterId { get; set; }
            public int CarrierId { get; set; }
            public string FirstZIP { get; set; }
            public string LastZIP { get; set; }
      
    }
}
