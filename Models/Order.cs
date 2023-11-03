using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? BienSo { get; set; }

    public string? LoaiXe { get; set; }

    public string? TenXe { get; set; }

    public DateTime? NgayVao { get; set; }

    public DateTime? NgayRa { get; set; }

    public double? TongTien { get; set; }

    public string? MauSac { get; set; }

    public string? LoaiVe { get; set; }
}
