CREATE TABLE [dbo].[Weather](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [deviceId] [varchar](50) NULL,
    [pressure] [float] NULL,
    [temperature] [float] NULL,
    [humidity] [float] NULL,
    [EventProcessedUtcTime] [char](30) NULL,
    [EventEnqueuedUtcTime] [char](30) NULL,
    [score] [float] NULL,
       CONSTRAINT [PK_Weather] PRIMARY KEY CLUSTERED
    (
        [Id]
    )
)
