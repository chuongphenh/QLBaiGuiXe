using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class LoaiVe
{
    public int Id { get; set; }

    public string? TenLoaiVe { get; set; }

    public double? GiaVe { get; set; }

    public virtual ICollection<VeXe> VeXes { get; set; } = new List<VeXe>();
}
