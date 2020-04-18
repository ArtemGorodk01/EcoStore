create database EcoStore;
use EcoStore;

create table Category
(
Id int primary key,
Title nvarchar(100),
[Description] nvarchar(max),
);

create table Product
(
Id int primary key,
Price money,
Title nvarchar(200),
CategoryId int references Category(Id),
GuaranteeMonth int null,
ImageDataBase64 nvarchar(max),
);

create table [User]
(
Id int primary key,
[Role] int,
[Login] nvarchar(200),
[Phone] nvarchar(20),
[Password] nvarchar(1000),
FirstName nvarchar(100),
LastName nvarchar(100),
Country nvarchar(100),
Region nvarchar(100),
[Address] nvarchar(200),
DateOfBirth datetime,
RegistrationDate datetime,
Gender int
);

create table [Order]
(
Id int primary key,
OrderDate datetime,
Price money,
[Status] bit,
Userd int references [User](Id)
);

create table Delivery
(
OrderId int references [Order](Id),
DeliveryDate datetime,
Details nvarchar(max),
[Address] nvarchar(200),
[Status] int
);

create table ProductOrder
(
OrderId int references [Order](Id),
ProductId int references [Product](Id),
Amount int
);

create table Vendor
(
Id int primary key,
Title nvarchar(100),
[Email] nvarchar(100),
[Phone] nvarchar(20)
);

create table Store
(
Id int primary key,
[Address] nvarchar(200),
[Phone] nvarchar(20),
[Email] nvarchar(100)
);

create table Storage
(
ProductId int references Product(Id),
StoreId int references Store(Id),
VendorId int references Vendor(Id),
Amount int
);

create table [UserMarkProduct]
(
ProductId int references Product(Id),
UserId int references [User](Id),
Mark int,
Review nvarchar(200)
);
