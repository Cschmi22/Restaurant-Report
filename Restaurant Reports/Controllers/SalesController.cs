using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reports.Data;
using Restaurant_Reports.Models;
using Restaurant_Reports.Models.Domain;
using System.Linq;

namespace Restaurant_Reports.Controllers
{
    public class SalesController : Controller
    {
        private RedDBContext redDBContext;

        public SalesController(RedDBContext redDBContext) 
        {
            this.redDBContext = redDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            var sales = await redDBContext.SalesReports.ToListAsync();

            if (sortOrder == "asc")
            {
                sales = sales.OrderBy(x => x.Week).ToList();
            }
            else if (sortOrder == "desc")
            {
                sales = sales.OrderByDescending(x => x.Week).ToList();
            }
            else

            ViewData["CurrentSort"] = sortOrder;

            return View(sales);
        }

            [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        private int CalculateTotalSales(SalesReport salesReport)
        {
            return salesReport.MonSales + salesReport.TuesSales + salesReport.WedSales +
                   salesReport.ThursSales + salesReport.FriSales + salesReport.SatSales + salesReport.SunSales;
        }
        public async Task<IActionResult> Add(UpdateSalesViewModel updateSales)
        {
            var sales = new SalesReport()
            {
                Id = Guid.NewGuid(),
                Week = updateSales.Week,
                Course = updateSales.Course,
                Name = updateSales.Name,
                MonSales = updateSales.MonSales,
                TuesSales = updateSales.TuesSales,
                WedSales = updateSales.WedSales,
                ThursSales = updateSales.ThursSales,
                FriSales = updateSales.FriSales,
                SatSales = updateSales.SatSales,
                SunSales = updateSales.SunSales,
            };
            sales.TotalSales = CalculateTotalSales(sales);
            await redDBContext.SalesReports.AddAsync(sales);
            await redDBContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var sales = await redDBContext.SalesReports.FirstOrDefaultAsync(x => x.Id == id);

            if(sales != null)
            {
                var saleView = new EditSalesViewModel()
                {
                    Id = sales.Id,
                    Week = sales.Week,
                    Course = sales.Course,
                    Name = sales.Name,
                    MonSales = sales.MonSales,
                    TuesSales = sales.TuesSales,
                    WedSales = sales.WedSales,
                    ThursSales = sales.ThursSales,
                    FriSales = sales.FriSales,
                    SatSales = sales.SatSales,
                    SunSales = sales.SunSales,
                    TotalSales = sales.TotalSales,
                };
                return await Task.Run(() => View("View",saleView));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(EditSalesViewModel sModel)
        {
            var sales = await redDBContext.SalesReports.FindAsync(sModel.Id);

            if(sales != null)
            {
                sales.Week = sModel.Week;
                sales.Course = sModel.Course;
                sales.Name = sModel.Name;
                sales.MonSales = sModel.MonSales;
                sales.TuesSales = sModel.TuesSales;
                sales.WedSales = sModel.WedSales;
                sales.ThursSales = sModel.ThursSales;
                sales.FriSales = sModel.FriSales;
                sales.SatSales = sModel.SatSales;
                sales.SunSales = sModel.SunSales;
                sales.TotalSales = CalculateTotalSales(sales);

                await redDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditSalesViewModel sModel)
        {
            var sales = await redDBContext.SalesReports.FindAsync(sModel.Id);

            if(sales != null)
            {
                redDBContext.SalesReports.Remove(sales);
                await redDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
