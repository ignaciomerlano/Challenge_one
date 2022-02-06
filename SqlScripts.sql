CREATE DATABASE [Challenge_oneDB]

USE [Challenge_oneDB]
GO

create table slots(
	Id Int identity(1,1) primary key,
	SlotId uniqueidentifier,
	Number int,
	IsAvailable bit,
	UpdatedDate DateTime,
	CreatedDate DateTime,
)
GO

create table reservations(
	Id Int identity(1,1) primary key,
	ReservationId uniqueidentifier,
	CarLicensePlate varchar(50),
	CarType varchar(50),
	CarColor varchar(50),
	SlotId Int FOREIGN KEY (SlotId) REFERENCES slots(Id),
	CheckIn DateTime,
	CheckOut DateTime,
	Cost decimal(12,2),
	UpdatedDate DateTime,
	CreatedDate DateTime,
)
GO

create table AppSettings(
	Id Int identity(1,1) primary key,
	AppKey varchar(255),
	AppValue varchar(255),
	UpdatedDate DateTime,
	CreatedDate DateTime,
)
GO

----

create procedure UpdateReservation
(
	@ReservationId uniqueidentifier,
	@CarLicensePlate varchar(50),
	@CarType varchar(50),
	@CarColor varchar(50),
	@SlotId Int,
	@CheckIn DateTime,
	@CheckOut DateTime,
	@Cost decimal(12,2),
	@UpdatedDate DateTime,
	@CreatedDate DateTime
)
AS
Begin
	update reservations
	set 
		CarLicensePlate = @CarLicensePlate,
		CarType = @CarType,
		CarColor = @CarColor,
		SlotId = @SlotId,
		CheckIn = @CheckIn,
		CheckOut = @CheckOut,
		Cost = @Cost,
		UpdatedDate = @UpdatedDate,
		CreatedDate = @CreatedDate
	where ReservationId = @ReservationId
END
GO

----------

create procedure AddReservation
(
	@ReservationId uniqueidentifier,
	@CarLicensePlate varchar(50),
	@CarType varchar(50),
	@CarColor varchar(50),
	@SlotId Int,
	@CheckIn DateTime,
	@CheckOut DateTime = null,
	@Cost decimal(12,2) = null,
	@UpdatedDate DateTime,
	@CreatedDate DateTime
)
AS
Begin
	insert into reservations (ReservationId, CarLicensePlate, CarType, CarColor, SlotId, CheckIn, CheckOut, Cost,UpdatedDate, CreatedDate)
	values(@ReservationId, @CarLicensePlate, @CarType, @CarColor, @SlotId, @CheckIn, @CheckOut, @Cost,@UpdatedDate, @CreatedDate)
END
GO

----------

CREATE procedure AddSlot
(
	@SlotId uniqueidentifier,
	@Number varchar(50),
	@IsAvailable bit,
	@CreatedDate DateTime,
	@UpdatedDate DateTime
)
AS
Begin
	insert into slots(SlotId, Number, IsAvailable, CreatedDate, UpdatedDate)
	values(@SlotId, @Number, @IsAvailable, @CreatedDate, @UpdatedDate)
END
GO

----

CREATE procedure UpdateSlot
(
	@SlotId uniqueidentifier,
	@Number varchar(50),
	@IsAvailable bit,
	@CreatedDate DateTime,
	@UpdatedDate DateTime
)
AS
Begin
	update slots
	set 
		Number = @Number,
		IsAvailable = @IsAvailable,
		UpdatedDate = @UpdatedDate,
		CreatedDate = @CreatedDate
	where SlotId = @SlotId
END
GO

-----

CREATE procedure GetReservationById
(
	@Id int
)
AS
Begin
	select *
	from reservations r
	where r.Id = @Id
END
GO

-----

CREATE procedure GetSlotById
(
	@Id int
)
AS
Begin
	select *
	from slots s
	where s.Id = @Id
END
GO

----

CREATE procedure GetSlotByGuidId
(
	@Id uniqueidentifier 
)
AS
Begin
	select *
	from slots s
	where s.SlotId = @Id
END
GO

----

CREATE procedure GetSlots
AS
Begin
	select *
	from slots
END
GO

-----

CREATE procedure GetAppSettingValueByKey
(
	@AppKey varchar(255)
)
AS
Begin
	select *
	from AppSettings a
	where a.AppKey = @AppKey
END
GO

-----