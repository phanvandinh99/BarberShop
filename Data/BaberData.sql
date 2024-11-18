USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BaberData')
BEGIN
    ALTER DATABASE BaberData SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BaberData;
END
go
create database BaberData
go
use BaberData
go
create table NhanVien
(
	TaiKhoanNV varchar(50) primary key,
	MatKhauNV nvarchar(50) not null,
	HoTen nvarchar(100) not null,
	GioiTinh int default(0), -- 0: nam, 1: nữ
	SDT varchar(10) null,
	DiaChi nvarchar(500),
	TrangThai int default(0), -- 0: Hoạt động, 1: Khóa
)
go
insert into NhanVien (TaiKhoanNV, MatKhauNV, HoTen, GioiTinh, SDT, DiaChi, TrangThai) 
values ('Admin', 'Abc123', N'Ban Quản Trị', 0, '0975254562', N'Huế', 0);
go
create table KhachHang
(
	TaiKhoanKH varchar(50) primary key,
	MatKhauKH nvarchar(50) not null,
	HoTen nvarchar(100) not null,
	GioiTinh int default(0), -- 0: nam, 1: nữ
	SDT varchar(10) null,
	DiaChi nvarchar(500),
	TrangThai int default(0), -- 0: Hoạt động, 1: Khóa
)
go
insert into KhachHang (TaiKhoanKH, MatKhauKH, HoTen, GioiTinh, SDT, DiaChi, TrangThai) 
values ('Nam', 'Abc123', N'Lê Hoàng Nam', 0, '0975254555', N'Huế', 0);
go
create table LoaiSanPham
(
	MaLoaiSanPham int identity (1,1) primary key,
	TenLoaiSanPham nvarchar(100) ,
	SoLuong int null,
)
go
insert into LoaiSanPham (TenLoaiSanPham, SoLuong) values (N'Keo Vuốt Tóc',4);
insert into LoaiSanPham (TenLoaiSanPham, SoLuong) values (N'Dầu gội',0);
insert into LoaiSanPham (TenLoaiSanPham, SoLuong) values (N'Tông Đơ',0);
insert into LoaiSanPham (TenLoaiSanPham, SoLuong) values (N'Kéo',0);
go
create table SanPham
(
	MaSanPham int identity(1,1) primary key,
	HinhAnh nvarchar(MAX) null,
	TenSanPham nvarchar(200) not null,
	GiaBan money not null,
	SoLuong int null,
	DaBan int null,
	TrangThai int null,
	MaLoaiSanPham int,

	foreign key (MaLoaiSanPham) references LoaiSanPham(MaLoaiSanPham),
)
go
insert into SanPham (HinhAnh, TenSanPham, GiaBan, SoLuong, DaBan, TrangThai, MaLoaiSanPham) 
values (N'AnhMacDinh.png', N'Reuzel Blue Pomade 340g', 100000, 10, 0, 0, 1);
insert into SanPham (HinhAnh, TenSanPham, GiaBan, SoLuong, DaBan, TrangThai, MaLoaiSanPham) 
values (N'AnhMacDinh.png', N'Reuzel Blue Pomade 300g ', 100000, 10, 0, 0, 1);
insert into SanPham (HinhAnh, TenSanPham, GiaBan, SoLuong, DaBan, TrangThai, MaLoaiSanPham) 
values (N'AnhMacDinh.png', N'Reuzel Blue Pomade 30g ', 100000, 10, 0, 0, 1);
insert into SanPham (HinhAnh, TenSanPham, GiaBan, SoLuong, DaBan, TrangThai, MaLoaiSanPham) 
values (N'AnhMacDinh.png', N'Reuzel Blue Pomade 10g ', 100000, 10, 0, 0, 1);
go
create table DichVu
(
	MaDichVu int identity (1,1) primary key,
	TenDichVu nvarchar(100) ,
	DonGia money not null,
	ThoiGian varchar,
)
go
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Cắt Tóc Nam', 70000, 40);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Gội Đầu', 30000, 20);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Ráy Tai', 40000, 30);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Cạo Mặt', 20000, 20);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Ráy Tai', 40000, 30);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Lột Mụn Cám', 60000, 30);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Tẩy Tóc', 100000, 60);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Nhuộm Tóc', 300000, 120);
insert into DichVu (TenDichVu, DonGia, ThoiGian) values (N'Phục Hồi Tóc Hư Tổn', 300000, 60);
go
create table ChiNhanh
(
	MaChiNhanh int identity (1,1) primary key,
	TenChiNhanh nvarchar(200) not null,
	DicChi nvarchar(max),
	SDT varchar(10),
)
go
insert into ChiNhanh (TenChiNhanh, DicChi, SDT) values (N'Vương Barber CS1', N'122 Trần Cao Vân, Hồ CHí Minh', '0971010234');
insert into ChiNhanh (TenChiNhanh, DicChi, SDT) values (N'Vương Barber CS2', N'34 Lý Thái Tổ, Hồ CHí Minh', '0971010235');
insert into ChiNhanh (TenChiNhanh, DicChi, SDT) values (N'Vương Barber CS4', N'19 Phạm Văn Đồng, Hồ CHí Minh', '0971010236');
go
create table LichHen
(
	MaLichHen int identity(1,1) primary key,
	SDT varchar(10) not null,
	HoTen nvarchar(100) not null,
	ThoiGianDat Datetime not null,
	NgayDat Datetime not null,
	TongTien money null,
	TrangThai Tinyint not null, -- 0: Đang đặt, 1: Đã duyệt, 2: Đã Hủy, 3: Đã Thanh Toán

	TaiKhoanNV varchar(50),
	MaChiNhanh int,
	TaiKhoanKH varchar(50),

	foreign key (TaiKhoanNV) references NhanVien(TaiKhoanNV),
	foreign key (MaChiNhanh) references ChiNhanh(MaChiNhanh),
	foreign key (TaiKhoanKH) references KhachHang(TaiKhoanKH),
)
go
create table SuDungDichVu
(
	MaDichVu int,
	MaLichHen int,
	primary key (MaDichVu, MaLichHen),
	ThanhTien money not null,
	ThoiGianHuy Datetime not null,
	Huy Bit,

	foreign key (MaDichVu) references DichVu(MaDichVu),
	foreign key (MaLichHen) references LichHen(MaLichHen),
)
go
create table DonHang
(
	MaDonHang int identity(1,1) primary key,
	TenNguoiNhan nvarchar(100) not null,
	NgayDat date default(getdate()),
	DiaChi nvarchar(100) not null,
	SDT varchar(10) not null,
	ThanhPho nvarchar(100) not null,
	Quan nvarchar(100) not null,
	Phuong nvarchar(100) not null,
	NgayGiao date null,
	TongTien float not null,
	TrangThai int default(0), --0: duyệt đơn, 1: đang giao, 2 đã giao, 3 đã hủy
	TaiKhoanKH varchar(50) not null, 

	foreign key(TaiKhoanKH) references KhachHang(TaiKhoanKH),
)
go
create table ChiTietDonHang
(
	MaDonHang int,
	MaSanPham int,
	primary key (MaDonHang, MaSanPham),
	SoLuongMua int not null,
	ThanhTien money not null,

	foreign key (MaDonHang) references DonHang(MaDonHang),
	foreign key (MaSanPham) references SanPham(MaSanPham),
)
go