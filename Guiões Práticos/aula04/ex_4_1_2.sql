
CREATE TABLE Voos_Airport(
	[Code] [varchar] (8) NOT NULL PRIMARY KEY,
	[City] [varchar] (32) NOT NULL,
	[State] [varchar] (32) NOT NULL,
	[Name] [varchar] (64) NOT NULL,
)
GO

CREATE TABLE Voos_AirplaneType(
	[Type_Name] [varchar] (8) NOT NULL PRIMARY KEY,
	[Company] [varchar] (32) NOT NULL,
	[Max_Seats] [int] NOT NULL,
)
GO

CREATE TABLE Voos_Airplane(
	[Airplane_id] [varchar] (8) NOT NULL PRIMARY KEY,
	[Total_no_of_seats] [int] NOT NULL,
	[AirplaneType_Type_Name] [varchar] (8) NOT NULL REFERENCES [Voos_AirplaneType] ([Type_Name]),
)
GO

CREATE TABLE Voos_Flight(
	[Number] [int] NOT NULL PRIMARY KEY,
	[Airline] [varchar] (32) NOT NULL,
	[Weekdays] [varchar] (8) NOT NULL,
)
GO

CREATE TABLE Voos_FlightLeg(
	[Flight_Number] [int] NOT NULL REFERENCES [Voos_Flight] ([Number]),
	[Dep_Airport_Code] [varchar] (8) NOT NULL REFERENCES [Voos_Airport] ([Code]),
	[Arr_Airport_Code] [varchar] (8) NOT NULL REFERENCES [Voos_Airport] ([Code]),
	[Leg_no] [int] NOT NULL ,
	[Schedule_arr_time] [datetime] NOT NULL,
	[Schedule_dep_time] [datetime] NOT NULL,
	PRIMARY KEY ([Flight_Number], [Dep_Airport_Code], [Arr_Airport_Code], [Leg_no]),
)
GO

CREATE TABLE Voos_Fare(
	[Flight_Number] [int] NOT NULL REFERENCES [Voos_Flight] ([Number]),
	[Code] [varchar] (8) NOT NULL,
	[Amount] [float],
	[Restrictions] [varchar] (256),
	PRIMARY KEY ([Flight_Number], [Code]),
)
GO

CREATE TABLE Voos_LegInstance(
	[FlightLeg_Flight_Number] [int] NOT NULL,
	[FlightLeg_Dep_Airport_Code] [varchar] (8) NOT NULL,
	[FlightLeg_Arr_Airport_Code] [varchar] (8) NOT NULL,
	[FlightLeg_Leg_no] [int] NOT NULL,
	[Airplane_Airplane_id] [varchar] (8) NOT NULL,
	[Date] [datetime] NOT NULL,
	[No_of_avail_seats] [int] NOT NULL,
	[Arr_time] [datetime] NOT NULL,
	[Dep_time] [datetime] NOT NULL,
	PRIMARY KEY ([Date], [FlightLeg_Flight_Number], [FlightLeg_Dep_Airport_Code], [FlightLeg_Arr_Airport_Code], [FlightLeg_Leg_no], [Airplane_Airplane_id]),
	FOREIGN KEY ([FlightLeg_Flight_Number], [FlightLeg_Dep_Airport_Code], [FlightLeg_Arr_Airport_Code], [FlightLeg_Leg_no]) REFERENCES Voos_FlightLeg([Flight_Number], [Dep_Airport_Code], [Arr_Airport_Code], [Leg_no]),
	FOREIGN KEY ([Airplane_Airplane_id]) REFERENCES Voos_Airplane([Airplane_id]),
)
GO

CREATE TABLE Voos_Seat(
	[LegInstance_Airplane_Airplane_id] [varchar] (8) NOT NULL,
	[LegInstance_Date] [datetime] NOT NULL,
	[LegInstance_FlightLeg_Flight_Number] [int] NOT NULL,
	[LegInstance_FlightLeg_Dep_Airport_Code] [varchar] (8) NOT NULL,
	[LegInstance_FlightLeg_Arr_Airport_Code] [varchar] (8) NOT NULL,
	[LegInstance_FlightLeg_Leg_no] [int] NOT NULL,
	[Seat_no] [int] NOT NULL,
	[Costumer_name] [varchar] (128) NOT NULL,
	[Cphone] [int] NOT NULL,
	PRIMARY KEY([LegInstance_Date], [LegInstance_FlightLeg_Flight_Number], [LegInstance_FlightLeg_Dep_Airport_Code], [LegInstance_FlightLeg_Arr_Airport_Code], [LegInstance_FlightLeg_Leg_no], [LegInstance_Airplane_Airplane_id], [Seat_no]),
	FOREIGN KEY([LegInstance_Date], [LegInstance_FlightLeg_Flight_Number], [LegInstance_FlightLeg_Dep_Airport_Code], [LegInstance_FlightLeg_Arr_Airport_Code], [LegInstance_FlightLeg_Leg_no], [LegInstance_Airplane_Airplane_id]) REFERENCES Voos_LegInstance([Date], [FlightLeg_Flight_Number], [FlightLeg_Dep_Airport_Code], [FlightLeg_Arr_Airport_Code], [FlightLeg_Leg_no], [Airplane_Airplane_id]),
)
GO

CREATE TABLE Voos_CanLand(
	[Airport_Code] [varchar] (8) NOT NULL REFERENCES [Voos_Airport] ([Code]),
	[AirplaneType_Type_Name] [varchar] (8) NOT NULL REFERENCES [Voos_AirplaneType] ([Type_Name]),
	PRIMARY KEY ([Airport_Code], [AirplaneType_Type_Name]),
)
GO
