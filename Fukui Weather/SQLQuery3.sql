SELECT
    deviceId,pressure,temperature,humidity,EventProcessedUtcTime,EventEnqueuedUtcTime
     ,getscore(temperature,humidity,pressure,0.0) as score
INTO
    [db-output]
FROM
    [iothub-input]
WHERE type='weather'
