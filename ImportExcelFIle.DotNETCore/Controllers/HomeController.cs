using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImportExcelFIle.DotNETCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ImportExcelFIle.DotNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private IHostingEnvironment hostingEnvironment;
        private readonly AppDbContext _appDbContext;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment, AppDbContext appDbContext)
        {
            logger = logger;
            hostingEnvironment = hostingEnvironment;
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult ImportData([FromBody] ExcelColumnsDTO col)
        //{
        //    try
        //    {
        //        var data = _appDbContext.Zone.FirstOrDefault();
        //        int fromclusterId = _appDbContext.ZIPCluster.Where(x => x.FirstZIP == col.originClusterStartZip && x.LastZIP == col.originClusterEndZip).First().ZipClusterId;
        //        var count = _appDbContext.ZIPCluster.Where(x => x.FirstZIP == col.startZip && x.LastZIP == col.endZip ).Count();
        //        ZoneMap zoneMap = new ZoneMap();
        //        ZIPCluster zipCluster = new ZIPCluster();
        //        if (count == 0)
        //        {
        //            zipCluster.CarrierId = 1;
        //            zipCluster.FirstZIP = col.startZip;
        //            zipCluster.LastZIP = col.endZip;

        //            _appDbContext.ZIPCluster.Add(zipCluster);
        //            _appDbContext.SaveChanges();
        //            int zipClusterId = zipCluster.ZipClusterId;

        //            zoneMap.CarrierId = 1;
        //            zoneMap.FromZIPClusterId = fromclusterId;
        //            zoneMap.ToZIPClusterId = zipClusterId;
        //            if (col.zone != "NA")
        //            {
        //                int zoneCode = Convert.ToInt32(col.zone);
        //                zoneMap.ZoneId = _appDbContext.Zone.Where(x => x.ZoneCode == zoneCode).First().ZoneId;
        //            }
        //            else
        //            {
        //                zoneMap.ZoneId = null;
        //            }
        //            _appDbContext.ZoneMap.Add(zoneMap);
        //            _appDbContext.SaveChanges();
        //        }
        //        else
        //        {
        //            zoneMap.CarrierId = 1;
        //            zoneMap.FromZIPClusterId = fromclusterId;
        //            zoneMap.ToZIPClusterId = _appDbContext.ZIPCluster.Where(x => x.FirstZIP == col.startZip && x.LastZIP == col.endZip).First().ZipClusterId;

        //            if (col.zone.Contains("NA"))
        //            {
        //                zoneMap.ZoneId = null;
        //            }
        //            else
        //            {
        //                int zoneCode = Convert.ToInt32(col.zone);
        //                zoneMap.ZoneId = _appDbContext.Zone.Where(x => x.ZoneCode == zoneCode).First().ZoneId;
        //            }

        //            int ZoneMapExist = _appDbContext.ZoneMap.Where(x => x.FromZIPClusterId == zoneMap.FromZIPClusterId && x.ToZIPClusterId == zoneMap.ToZIPClusterId).Count();
        //            if (ZoneMapExist == 0)
        //            {
        //                _appDbContext.ZoneMap.Add(zoneMap);
        //                _appDbContext.SaveChanges();
        //            }
        //        }
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return Ok();
        //    }


          
        //}

        [HttpPost]
        public ActionResult ImportData([FromBody] ExcelColumnsDTO col)
        {       
                int fromclusterId = _appDbContext.ZipCluster_New.Where(x => x.FirstZIP == col.originClusterStartZip && x.LastZIP == col.originClusterEndZip).First().ZipClusterId;
                ZoneMap_New zoneMap = new ZoneMap_New();
                //ZipCluster_New zipCluster = new ZipCluster_New();
                zoneMap.CarrierId = 1;
                zoneMap.FromZIPClusterId = fromclusterId;
                int firstZip = Convert.ToInt32(col.startZip);
                int lastZip = Convert.ToInt32(col.endZip);
                var zipClusters = _appDbContext.ZipCluster_New.FromSqlRaw(" GetZipClustersBetween {0},{1}", firstZip, lastZip).ToList();
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
                        zoneMap.ZoneId = _appDbContext.Zone.Where(x => x.ZoneCode == zoneCode).First().ZoneId;
                    }
                    int ZoneMapExist = _appDbContext.ZoneMap_New.Where(x => x.FromZIPClusterId == zoneMap.FromZIPClusterId && x.ToZIPClusterId == zoneMap.ToZIPClusterId).Count();
                    if (ZoneMapExist == 0)
                    {
                    try
                    {
                        zoneMap.ZoneMapId = 0;
                        zoneMap.ToZIPClusterId = item.ZipClusterId;
                        _appDbContext.ZoneMap_New.Add(zoneMap);
                        _appDbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return Ok(ex);
                    }
                    }
                }
                return Ok();
                     
        }
    }
}
