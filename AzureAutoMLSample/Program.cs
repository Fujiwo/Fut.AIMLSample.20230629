using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CallRequestResponseService
{
    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double CorrectValue { get; set; }

        //string ToJson()
        //{
        //    StringBuilder stringBuilder = new();

        //    stringBuilder.Append(
        //        @"{
        //        ""Latitude"": ");
        //    stringBuilder.Append(Latitude.ToString());
        //    stringBuilder.Append(@",
        //            ""Longitude"": ");
        //    stringBuilder.Append(Longitude.ToString());
        //    stringBuilder.Append(@"
        //            }");

        //    return stringBuilder.ToString();
        //}

        class JsonCoordinates
        {
            public ACoordinates Inputs { get; set; } = new ();

            public AGlobalParameters GlobalParameters { get; set; } = new();

            public JsonCoordinates(IEnumerable<Coordinate> coordinates)
            {
                var coordinateArray = coordinates.ToArray();

                Inputs.data = new ACoordinate[coordinateArray.Length];
                for (var index = 0; index < coordinateArray.Length; index++)
                    Inputs.data[index] = new ACoordinate {
                        Latitude  = coordinateArray[index].Latitude ,
                        Longitude = coordinateArray[index].Longitude
                    };
            }

            public class ACoordinates
            {
                public ACoordinate[] data { get; set; }
            }

            public class ACoordinate
            {
                public double Latitude { get; set; }
                public double Longitude { get; set; }
            }

            public class AGlobalParameters
            {
                public string method { get; set; } = "predict";
            }
        }

        public static string ToJson(IEnumerable<Coordinate> coordinates)
        {
            var jsonCoordinate = new JsonCoordinates(coordinates);
            var jsonString = JsonSerializer.Serialize(jsonCoordinate);
            Console.WriteLine(jsonString);
            return jsonString;

            //StringBuilder stringBuilder = new();

            //stringBuilder.Append(
            //    @"{
            //      ""Inputs"": {
            //        ""data"": [");

            //var index = 0;
            //foreach (var coordinate in coordinates) {
            //    if (index++ != 0)
            //        stringBuilder.Append(",");
            //    stringBuilder.Append(coordinate.ToJson());
            //}

            //stringBuilder.Append(@"
            //        ]
            //      },
            //      ""GlobalParameters"": {
            //        ""method"": ""predict""
            //      }
            //    }");

            //Console.WriteLine(stringBuilder.ToString());
            //return stringBuilder.ToString();
        }
    }

    class Program
    {
        static Coordinate[] testCoordinates = new[] {
            new Coordinate { Latitude = 36.258288, Longitude = 136.284582 },
            new Coordinate { Latitude = 36.274912, Longitude = 136.329279 },
            new Coordinate { Latitude = 36.115784, Longitude = 136.436160 },
            new Coordinate { Latitude = 35.921390, Longitude = 136.887256 },
            new Coordinate { Latitude = 35.801149, Longitude = 136.819202 },
            new Coordinate { Latitude = 35.837652, Longitude = 136.695777 },
            new Coordinate { Latitude = 35.810712, Longitude = 136.505430 },
            new Coordinate { Latitude = 35.742470, Longitude = 136.561360 },
            new Coordinate { Latitude = 35.802909, Longitude = 136.316039 },
            new Coordinate { Latitude = 35.665852, Longitude = 136.343790 },
            new Coordinate { Latitude = 35.571157, Longitude = 136.081772 },
            new Coordinate { Latitude = 35.503405, Longitude = 136.098920 },
            new Coordinate { Latitude = 35.482062, Longitude = 135.943618 },
            new Coordinate { Latitude = 35.333335, Longitude = 135.606685 },
            new Coordinate { Latitude = 35.420561, Longitude = 135.548270 },
            new Coordinate { Latitude = 35.473941, Longitude = 135.436580 },
            new Coordinate { Latitude = 35.532702, Longitude = 135.462497 },
            new Coordinate { Latitude = 35.557382, Longitude = 135.465656 },
            new Coordinate { Latitude = 36.047508, Longitude = 136.414003 },
            new Coordinate { Latitude = 35.437255, Longitude = 135.641224 },
            new Coordinate { Latitude = 35.451298, Longitude = 135.805629 },
            new Coordinate { Latitude = 35.535321, Longitude = 135.986298 },
            new Coordinate { Latitude = 35.606896, Longitude = 136.129897 },
            new Coordinate { Latitude = 35.798155, Longitude = 136.297373 },
            new Coordinate { Latitude = 35.868748, Longitude = 136.665986 },
            new Coordinate { Latitude = 36.054450, Longitude = 136.587643 },
            new Coordinate { Latitude = 36.202522, Longitude = 136.260118 }
        };

        static void Main() => InvokeRequestResponseService().Wait();

        static async Task InvokeRequestResponseService()
        {
            var handler = new HttpClientHandler() {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler)) {
                // Request data goes here
                // The example below assumes JSON formatting which may be updated
                // depending on the format your endpoint expects.
                // More information can be found here:
                // https://docs.microsoft.com/azure/machine-learning/how-to-deploy-advanced-entry-script

                var scoreRequestContent = Coordinate.ToJson(testCoordinates);
                //Console.WriteLine(scoreRequestContent);

                //const string apiKey = "Jpo7nyDEDdjLyurtkuehinux9H3RpOck"; // Replace this with the API key for the web service
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //client.BaseAddress = new Uri("https://automl20220701.japaneast.inference.ml.azure.com/score");
				client.BaseAddress = new Uri("http://428890eb-d024-48da-95cb-c8f3016db693.japaneast.azurecontainer.io/score");

				var content = new StringContent(scoreRequestContent);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // This header will force the request to go to a specific deployment.
                // Remove this line to have the request observe the endpoint traffic rules
                content.Headers.Add("azureml-model-deployment", "default");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)
                var response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode) {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                } else {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}
