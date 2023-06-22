using MLNetSample;

//Load sample data
var sampleData = new MLModel1.ModelInput() {
    Latitude  =  36.00083F,
    Longitude = 135.98580F
};

//Load model and predict output
var result = MLModel1.Predict(sampleData);
Console.WriteLine(result.Value);
