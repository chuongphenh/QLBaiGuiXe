using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLBaiGuiXe.Models;

public partial class QlbaiGuiXeContext : DbContext
{
    public QlbaiGuiXeContext()
    {
    }

    public QlbaiGuiXeContext(DbContextOptions<QlbaiGuiXeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiXe> BaiXes { get; set; }

    public virtual DbSet<LoaiVe> LoaiVes { get; set; }

    public virtual DbSet<LoaiXe> LoaiXes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<SuCo> SuCos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VeXe> VeXes { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" Data Source=LAP-CN-111\\PHC_1304;Initial Catalog=QLBaiGuiXe;User ID=sa;Password=Phc1304.,..;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiXe>(entity =>
        {
            entity.ToTable("BaiXe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SoChoOTo).HasColumnName("so_cho_o_to");
            entity.Property(e => e.SoChoTrongOTo).HasColumnName("so_cho_trong_o_to");
            entity.Property(e => e.SoChoTrongXeDapMay).HasColumnName("so_cho_trong_xe_dap_may");
            entity.Property(e => e.SoChoXeDapMay).HasColumnName("so_cho_xe_dap_may");
            entity.Property(e => e.TenBaiXe)
                .HasMaxLength(200)
                .HasColumnName("ten_bai_xe");
        });

        modelBuilder.Entity<LoaiVe>(entity =>
        {
            entity.ToTable("LoaiVe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GiaVe).HasColumnName("gia_ve");
            entity.Property(e => e.TenLoaiVe)
                .HasMaxLength(200)
                .HasColumnName("ten_loai_ve");
        });

        modelBuilder.Entity<LoaiXe>(entity =>
        {
            entity.ToTable("LoaiXe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TenLoaiXe)
                .HasMaxLength(200)
                .HasColumnName("ten_loai_xe");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BienSo)
                .HasMaxLength(200)
                .HasColumnName("bien_so");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdVeXe).HasColumnName("id_ve_xe");
            entity.Property(e => e.LoaiVe)
                .HasMaxLength(200)
                .HasColumnName("loai_ve");
            entity.Property(e => e.LoaiXe)
                .HasMaxLength(200)
                .HasColumnName("loai_xe");
            entity.Property(e => e.MauSac)
                .HasMaxLength(200)
                .HasColumnName("mau_sac");
            entity.Property(e => e.NgayRa)
                .HasColumnType("datetime")
                .HasColumnName("ngay_ra");
            entity.Property(e => e.NgayVao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_vao");
            entity.Property(e => e.TenXe)
                .HasMaxLength(200)
                .HasColumnName("ten_xe");
            entity.Property(e => e.TongTien).HasColumnName("tong_tien");
        });

        modelBuilder.Entity<SuCo>(entity =>
        {
            entity.ToTable("SuCo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GhiChu)
                .HasMaxLength(200)
                .HasColumnName("ghi_chu");
            entity.Property(e => e.KinhPhi).HasColumnName("kinh_phi");
            entity.Property(e => e.TenSuCo)
                .HasMaxLength(200)
                .HasColumnName("ten_su_co");
            entity.Property(e => e.TrangThai).HasColumnName("trang_thai");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(200)
                .HasColumnName("dia_chi");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(200)
                .HasColumnName("gioi_tinh");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(200)
                .HasColumnName("mat_khau");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(200)
                .HasColumnName("so_dien_thoai");
            entity.Property(e => e.TenDayDu)
                .HasMaxLength(200)
                .HasColumnName("ten_day_du");
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(200)
                .HasColumnName("ten_tai_khoan");
        });

        modelBuilder.Entity<VeXe>(entity =>
        {
            entity.ToTable("VeXe");

            entity.HasIndex(e => e.MaVe, "UQ_VeXeMaVe").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(500)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.IdBaiXe).HasColumnName("id_bai_xe");
            entity.Property(e => e.IdLoaiVe).HasColumnName("id_loai_ve");
            entity.Property(e => e.IdXe).HasColumnName("id_xe");
            entity.Property(e => e.MaVe)
                .HasMaxLength(100)
                .HasColumnName("ma_ve");
            entity.Property(e => e.NgayDangKy)
                .HasColumnType("datetime")
                .HasColumnName("ngay_dang_ky");
            entity.Property(e => e.NgayHetHan)
                .HasColumnType("datetime")
                .HasColumnName("ngay_het_han");
            entity.Property(e => e.NgayRa)
                .HasColumnType("datetime")
                .HasColumnName("ngay_ra");
            entity.Property(e => e.NgayVao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_vao");
            entity.Property(e => e.TongTien).HasColumnName("tong_tien");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.IdLoaiVeNavigation).WithMany(p => p.VeXes)
                .HasForeignKey(d => d.IdLoaiVe)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VeXe_LoaiVe");

            entity.HasOne(d => d.IdXeNavigation).WithMany(p => p.VeXes)
                .HasForeignKey(d => d.IdXe)
                .HasConstraintName("FK_VeXe_Xe");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.ToTable("Xe");

            entity.HasIndex(e => e.BienSo, "unique_bien_so").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BienSo)
                .HasMaxLength(200)
                .HasColumnName("bien_so");
            entity.Property(e => e.DongXe)
                .HasMaxLength(200)
                .HasColumnName("dong_xe");
            entity.Property(e => e.IdLoaiXe).HasColumnName("id_loai_xe");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.MauSac)
                .HasMaxLength(200)
                .HasColumnName("mau_sac");
            entity.Property(e => e.TenXe)
                .HasMaxLength(200)
                .HasColumnName("ten_xe");

            entity.HasOne(d => d.IdLoaiXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.IdLoaiXe)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Xe_LoaiXe");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Xe_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
