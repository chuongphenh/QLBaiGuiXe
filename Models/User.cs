using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class User
{
    public int Id { get; set; }

    public string? TenTaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? TenDayDu { get; set; }

    public string? SoDienThoai { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
