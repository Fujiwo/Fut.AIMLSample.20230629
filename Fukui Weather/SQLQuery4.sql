SELECT
    deviceId,pressure,temperature,humidity,CAST(EventProcessedUtcTime as datetime) as EventProcessedUtcTime,CAST(EventEnqueuedUtcTime as datetime) as EventEnqueuedUtcTime
    ,getscore(temperature,humidity,pressure,0.0) as score
INTO
    [powerbi-output]
FROM
    [iothub-input]
WHERE type='weather'

SELECT
    deviceId,pressure,temperature,humidity,EventProcessedUtcTime,EventEnqueuedUtcTime
     ,getscore(temperature,humidity,pressure,0.0) as score
INTO
    [db-output]
FROM
    [iothub-input]
WHERE type='weather'
