using System.Text.Encodings.Web;
using System.Text.Json;

namespace PopStudio.PopAnim
{
    internal class PamJson
    {
        public static PopAnimInfo Decode(string inFile)
        {
            using (BinaryStream bs = new BinaryStream(inFile, FileMode.Open))
            {
                return JsonSerializer.Deserialize(bs, PamJsonContext.Default.PopAnimInfo);
            }
        }

        public static void Encode(PopAnimInfo pam, string outFile)
        {
            using (BinaryStream bs = new BinaryStream(outFile, FileMode.Create))
            {
                JsonSerializer.Serialize(bs, pam, PamJsonContext.Default.PopAnimInfo);
            }
        }
    }
}
