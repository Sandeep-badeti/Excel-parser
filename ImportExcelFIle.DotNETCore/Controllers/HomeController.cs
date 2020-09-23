using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ImportExcelFIle.DotNETCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImportExcelFIle.DotNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        public HomeController(AppDbContext appDbContext)
        {
            context = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportData([FromBody] ExcelColumnsDTO col)
        {
            int fromclusterId = context.ZipCluster_New.Where(x => x.FirstZIP == col.originClusterStartZip && x.LastZIP == col.originClusterEndZip).First().ZipClusterId;
            ZoneMap_New zoneMap = new ZoneMap_New();
            zoneMap.CarrierId = 1;
            zoneMap.FromZIPClusterId = fromclusterId;
            int firstZip = Convert.ToInt32(col.startZip);
            int lastZip = Convert.ToInt32(col.endZip);
            var zipClusters = context.ZipCluster_New.FromSqlRaw(" GetZipClustersBetween {0},{1}", firstZip, lastZip).ToList();
            foreach (var item in zipClusters)
            {
                if (col.zone.Contains("NA"))
                {
                    zoneMap.ZoneId = null;
                }
                else
                {
                    zoneMap.ToZIPClusterId = item.ZipClusterId;
                    int zoneCode = Convert.ToInt32(col.zone);
                    zoneMap.ZoneId = context.Zone.Where(x => x.ZoneCode == zoneCode).First().ZoneId;
                }
                int ZoneMapExist = context.ZoneMap_New.Where(x => x.FromZIPClusterId == zoneMap.FromZIPClusterId && x.ToZIPClusterId == zoneMap.ToZIPClusterId).Count();
                if (ZoneMapExist == 0)
                {
                    try
                    {
                        zoneMap.ZoneMapId = 0;
                        zoneMap.ToZIPClusterId = item.ZipClusterId;
                        context.ZoneMap_New.Add(zoneMap);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return Ok(ex);
                    }
                }
            }
            return Ok();

        }

        [HttpPost]
        public ActionResult ImportOverNightMapData([FromBody] OverNightMap col)
        {
            try
            {
                OverNightMap onMap = new OverNightMap();
                onMap.Zipcode = col.Zipcode;
                onMap.StartZip = col.StartZip;
                onMap.EndZip = col.EndZip;
                onMap.Id = col.Id;
                context.OverNightMap.Add(onMap);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }


            return Ok();

        }
    }
}
