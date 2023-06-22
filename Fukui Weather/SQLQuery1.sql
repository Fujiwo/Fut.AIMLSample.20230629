SELECT
      [AverageTemperature_celsius] AS temperature
      ,[AverageHumidity_percentage] AS humidity
      ,[AverageLocalPressure_hPa] AS pressure
      ,CAST((CASE WHEN [TotalPrecipitationAmount_mm] > 0.0 THEN 1 ELSE 0 END) AS int) AS score
FROM [dbo].[fukuiweather1959-2018]
WHERE AverageTemperature_celsius IS NOT NULL AND AverageHumidity_percentage IS NOT NULL AND AverageLocalPressure_hPa IS NOT NULL AND TotalPrecipitationAmount_mm IS NOT NULL
ORDER BY Date
