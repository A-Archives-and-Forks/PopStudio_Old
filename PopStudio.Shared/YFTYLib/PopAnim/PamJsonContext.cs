using System.Text.Json.Serialization;

namespace PopStudio.PopAnim
{
    [JsonSerializable(typeof(PopAnimInfo))]
    [JsonSourceGenerationOptions(WriteIndented = true, AllowTrailingCommas = true)]
    internal partial class PamJsonContext : JsonSerializerContext
    {
        
    }
}