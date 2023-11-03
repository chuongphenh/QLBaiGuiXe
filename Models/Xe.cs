using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class Xe
{
    public int Id { get; set; }

    public int? IdLoaiXe { get; set; }

    public string? TenXe { get; set; }

    public string? BienSo { get; set; }

    public string? MauSac { get; set; }

    public string? DongXe { get; set; }

    public int? IdUser { get; set; }

    public virtual LoaiXe? IdLoaiXeNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<VeXe> VeXes { get; set; } = new List<VeXe>();
}
