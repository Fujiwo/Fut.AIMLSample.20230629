/****** Object:  Table [dbo].[fukuiweather1959-2018]    Script Date: 2019/02/28 15:16:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[fukuiweather1959-2018](
	[Id] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[AverageTemperature_celsius] [float] NOT NULL,
	[LowestTemperature_celsius] [float] NOT NULL,
	[HighestTemperature_celsius] [float] NOT NULL,
	[AverageHumidity_percentage] [float] NULL,
	[AverageLocalPressure_hPa] [float] NULL,
	[AverageVaporPressure_hPa] [float] NULL,
	[AverageWindSpeed_ms] [float] NULL,
	[TotalPrecipitationAmount_mm] [float] NOT NULL,
	[DeepestSnow_cm] [float] NULL,
	[DayLength_hour] [float] NULL,
	[AverageCloudCover_tithe] [float] NULL,
	[WeatherOverview_6_18] [nvarchar](max) NULL,
	[WeatherOverview_18_6] [nvarchar](max) NULL,
 CONSTRAINT [PK_fukuiweather1959-2018] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

