//Console.WriteLine("Hello world!");

using MLNetSample;

//Load sample data
var sampleData = new MLModel1.ModelInput() {
    Latitude = 36.00083F,
    Longitude = 135.98580F
};
//Load model and predict output
var result = MLModel1.Predict(sampleData);
Console.WriteLine(result.Value);

//var testCoordinates = new[] {
//			new MLModel1.ModelInput { Latitude = 36.258288F, Longitude = 136.284582F },
//			new MLModel1.ModelInput { Latitude = 36.274912F, Longitude = 136.329279F },
//			new MLModel1.ModelInput { Latitude = 36.115784F, Longitude = 136.436160F },
//			new MLModel1.ModelInput { Latitude = 35.921390F, Longitude = 136.887256F },
//			new MLModel1.ModelInput { Latitude = 35.801149F, Longitude = 136.819202F },
//			new MLModel1.ModelInput { Latitude = 35.837652F, Longitude = 136.695777F },
//			new MLModel1.ModelInput { Latitude = 35.810712F, Longitude = 136.505430F },
//			new MLModel1.ModelInput { Latitude = 35.742470F, Longitude = 136.561360F },
//			new MLModel1.ModelInput { Latitude = 35.802909F, Longitude = 136.316039F },
//			new MLModel1.ModelInput { Latitude = 35.665852F, Longitude = 136.343790F },
//			new MLModel1.ModelInput { Latitude = 35.571157F, Longitude = 136.081772F },
//			new MLModel1.ModelInput { Latitude = 35.503405F, Longitude = 136.098920F },
//			new MLModel1.ModelInput { Latitude = 35.482062F, Longitude = 135.943618F },
//			new MLModel1.ModelInput { Latitude = 35.333335F, Longitude = 135.606685F },
//			new MLModel1.ModelInput { Latitude = 35.420561F, Longitude = 135.548270F },
//			new MLModel1.ModelInput { Latitude = 35.473941F, Longitude = 135.436580F },
//			new MLModel1.ModelInput { Latitude = 35.532702F, Longitude = 135.462497F },
//			new MLModel1.ModelInput { Latitude = 35.557382F, Longitude = 135.465656F },
//			new MLModel1.ModelInput { Latitude = 36.047508F, Longitude = 136.414003F },
//			new MLModel1.ModelInput { Latitude = 35.437255F, Longitude = 135.641224F },
//			new MLModel1.ModelInput { Latitude = 35.451298F, Longitude = 135.805629F },
//			new MLModel1.ModelInput { Latitude = 35.535321F, Longitude = 135.986298F },
//			new MLModel1.ModelInput { Latitude = 35.606896F, Longitude = 136.129897F },
//			new MLModel1.ModelInput { Latitude = 35.798155F, Longitude = 136.297373F },
//			new MLModel1.ModelInput { Latitude = 35.868748F, Longitude = 136.665986F },
//			new MLModel1.ModelInput { Latitude = 36.054450F, Longitude = 136.587643F },
//			new MLModel1.ModelInput { Latitude = 36.202522F, Longitude = 136.260118F }
//		};

//testCoordinates.Select(coordinate => MLModel1.Predict(coordinate).Value)
//			   .ToList()
//			   .ForEach(Console.WriteLine);
