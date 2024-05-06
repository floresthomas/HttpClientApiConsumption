namespace HttpClientConsumeApi.Models
{
        public class DataModel
        {
            public Datum[] data { get; set; }
            public Links links { get; set; }
        }

        public class Links
        {
            public string self { get; set; }
            public string current { get; set; }
            public string next { get; set; }
            public string last { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string type { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Attributes
        {
            public string name { get; set; }
            public string description { get; set; }
            public bool hypoallergenic { get; set; }
        }
}
