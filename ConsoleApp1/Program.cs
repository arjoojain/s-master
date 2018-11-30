using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = System.IO.File.ReadAllText(@"C:\Users\arja\Source\Repos\ConsoleApp1\ConsoleApp1\TextFile1.txt");
            //var input = Deserialize<MoveJobsInput>(text);
            //Console.WriteLine(input);

            ListJobsInResourceGroup.DBConnection();

            Console.Read();
        }



        public static T Deserialize<T>(string jsonString) where T : class
        {
            string errorPath = null;
            var serializerSettings = NewJsonSerializerSettings();
            serializerSettings.Error = (sender, args) =>
            {
                if (args.CurrentObject == args.ErrorContext.OriginalObject)
                {
                    errorPath = args.ErrorContext.Path;
                }
            };

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString, serializerSettings);
            }
            catch (JsonSerializationException jse)
            {
                throw new Exception();
            }
        }

        public static JsonSerializerSettings NewJsonSerializerSettings()
        {
            var serializerSettings = new JsonSerializerSettings();

           
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            // TODO:RP use Error for some cases?
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            serializerSettings.TypeNameHandling = TypeNameHandling.None;
            serializerSettings.MaxDepth = 10;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.DateParseHandling = DateParseHandling.DateTime;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            serializerSettings.Converters.Add(
                new StringEnumConverter() { CamelCaseText = false });
            serializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal,
                    Culture = CultureInfo.InvariantCulture,
                });
         

            return serializerSettings;
        }


        public class MoveJobsInput
        {
            public string TargetResourceGroup;
            public List<string> Resources;
        }

    }
}
