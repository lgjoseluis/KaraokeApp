using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KaraokeApp.Infrastructure.Helper.Configuration
{
    internal class AppConfigurationManager
    {
        private static AppConfigurationManager _instance;
        private readonly JObject _secrets;
        // private const string Namespace = "KaraokeApp.Infrastructure.Settings";
        // private const string FileName = "appsettings.json";

        private AppConfigurationManager()
        {
            try
            {
                // Get the assembly this code is executing in
                // Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppConfigurationManager)).Assembly;
                Assembly assembly = Assembly.GetExecutingAssembly();


                // Look up the resource names and find the one that ends with settings.json                
                string resName = assembly.GetManifestResourceNames()
                    ?.FirstOrDefault(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase));

                // Load the resource file
                // Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                Stream stream = assembly.GetManifestResourceStream(resName);

                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();

                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Unable to load secrets file");
            }
        }

        public static AppConfigurationManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConfigurationManager();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    //Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
