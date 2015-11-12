using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void ConfigureFormatters(HttpConfiguration config)
        {
            // JSON Formatter - Grab current settings reference to edit
            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            // Have object properties return with a lowercase first letter, which is normal behaviour for most consumers of JSON
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // When a class references itself in a class, it can make the JSON formatters explode looping constantly
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Have dates return to clients with a UTC formatting.
            // Its recommended behaviour to store all datetimes as UTC and returning dates, especially to web browsers, in a strict format is advisable. Recommended not required.
            settings.DateFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'";

            // As Newtonsoft.Json has a static function as well, that could be used in the project, make it consistent with the formatter.
            JsonConvert.DefaultSettings = () => settings;

            // Xml Formatter - Remove if you dont want clients to consume XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}