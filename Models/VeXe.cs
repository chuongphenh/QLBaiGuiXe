using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class VeXe
{
    public int Id { get; set; }

    public int? IdLoaiVe { get; set; }

    public int IdXe { get; set; }

    public DateTime? NgayVao { get; set; }

    public DateTime? NgayRa { get; set; }

    public DateTime? NgayDangKy { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public string? HinhAnh { get; set; }

    public string? TrangThai { get; set; }

    public double? TongTien { get; set; }

    public string? MaVe { get; set; }

    public int? IdBaiXe { get; set; }

    public virtual LoaiVe? IdLoaiVeNavigation { get; set; }

    public virtual Xe IdXeNavigation { get; set; } = null!;
}
