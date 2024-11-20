using System.Collections.Generic;

namespace BarberShop.Models.ViewModels
{
    public class LichHenItems
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string ThoiGianDat { get; set; }
        public int MaChiNhanh { get; set; }
        public string Notes { get; set; }
        public List<int> Services { get; set; }
    }
}