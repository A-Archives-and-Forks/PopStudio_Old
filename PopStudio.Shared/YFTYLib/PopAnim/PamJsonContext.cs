using System.Text.Json.Serialization;

namespace PopStudio.PopAnim
{
    [JsonSerializable(typeof(PopAnimInfo))]
    internal partial class PamJsonContext : JsonSerializerContext
    {
        private static bool _initialized = false;

        public static PamJsonContext Instance
        {
            get
            {
                if (!_initialized)
                {
                    _initialized = true;
                    Default.Options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    Default.Options.WriteIndented = true;
                    Default.Options.AllowTrailingCommas = true;
                }
                return Default;
            }
        }
    }
}