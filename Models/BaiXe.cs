using System;
using System.Collections.Generic;

namespace QLBaiGuiXe.Models;

public partial class BaiXe
{
    public int Id { get; set; }

    public string? TenBaiXe { get; set; }

    public int? SoChoXeDapMay { get; set; }

    public int? SoChoTrongXeDapMay { get; set; }

    public int? SoChoOTo { get; set; }

    public int? SoChoTrongOTo { get; set; }
}
