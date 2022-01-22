using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITransportService _transportService;
        private readonly IUserProfileService _profileService;

        public HomeController(ITransportService transportService, IUserProfileService profileService)
        {
            _transportService = transportService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var totalWeek = await _transportService.GetTotalSpendThisWeekAsync(profile);
            ViewBag.TotalWeek = totalWeek;

            var totalLastWeek = await _transportService.GetTotalSpendLastWeekAsync(profile);
            ViewBag.TotalLastWeek = totalLastWeek;

            var totalMonth = await _transportService.GetTotalSpendThisMonthAsync(profile);
            ViewBag.TotalMonth = totalMonth;

            var totalLastMonth = await _transportService.GetTotalSpendLastMonthAsync(profile);
            ViewBag.TotalLastMonth = totalLastMonth;

            var totalYear = await _transportService.GetTotalSpendThisYearAsync(profile);
            ViewBag.TotalThisYear = totalYear;

            var monthlyReport = await _transportService.GetTotalSpendByMonthAsync(DateTime.Now.Year, profile);
            List<decimal> monthlyReportArray = new List<decimal>();
            for(decimal index = 1; index < 13; index++)
            {
                decimal? value = monthlyReport.Where(x => x.Month == index).Select(x => x.Total).FirstOrDefault();
                monthlyReportArray.Add(value.HasValue ? value.Value : 0);
            }
            ViewBag.MonthlyReport = JsonConvert.SerializeObject(monthlyReportArray);

            //x = ((NEW - LAST) * 100) / LAST
            ViewBag.WeekBalance = ((totalWeek - totalLastWeek) * 100) / (totalLastWeek > 0 ? totalLastWeek : 1);
            ViewBag.MonthBalance = ((totalMonth - totalLastMonth) * 100) / (totalLastMonth > 0 ? totalLastMonth : 1);

            return View();
        }
    
        public IActionResult Error()
        {
            return View("500");
        }
    }
}
