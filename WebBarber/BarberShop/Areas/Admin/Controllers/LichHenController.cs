﻿using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BarberShop.Areas.Admin.Controllers
{
    public class LichHenController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Danh sách dịch vụ
        public async Task<ActionResult> Index()
        {
            List<LichHen> lichHen = await db.LichHen.ToListAsync();
            return View(lichHen);
        }
    }
}