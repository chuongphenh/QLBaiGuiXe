using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class LoaiXe
{
    public int Id { get; set; }

    public string? TenLoaiXe { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
