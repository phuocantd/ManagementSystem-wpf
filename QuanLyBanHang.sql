create database QuanLyBanHang
go

use QuanLyBanHang
go

create table Unit
(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max)
)
go

create table Category
(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max)
)
go

create table Customer
(	
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max),
	AddressCus nvarchar(max),
	Phone nvarchar(20),
	Mail nvarchar(max),
	MoreInfo nvarchar(max)
)
go

create table Sale
(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max),
	PercentSale int default 0
)
go

create table Transport
(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Price BigInt default 0
)

create table Product
(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Counts int default 0,
	Price BigInt default 0,
	ID_Unit int not null,
	ID_Category int not null

	foreign key(ID_Unit) references Unit(ID),
	foreign key (ID_Category) references Category(ID)
)
go

create table Bill
(
	ID int identity(1,1) primary key,
	DateBill datetime,
	ID_Customer int,
	ID_Sale int,
	ID_Transport int,
	SumPrice BigInt default 0,

	foreign key(ID_Customer) references Customer(ID),
	foreign key(ID_Sale) references Sale(ID),
	foreign key(ID_Transport) references Transport(ID),
)
go

create table BillDetail
(
	ID int identity(1,1) primary key,
	ID_Bill int not null,
	ID_Product int not null,
	SumCount int default 0,
	SumPrice BigInt default 0

	foreign key(ID_Bill) references Bill(ID),
	foreign key(ID_Product) references Product(ID),
)
go

