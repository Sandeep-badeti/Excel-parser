namespace ImportExcelFIle.DotNETCore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string DestZIP { get; set; }
        public int DestZIPClusterId { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal DimWeight { get; set; }
    }
}
