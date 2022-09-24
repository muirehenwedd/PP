using System.Text.Json;

namespace Data;

internal class Configuration
{
    public static Configuration GetConfiguration() =>
        JsonSerializer.Deserialize<Configuration>(new FileInfo("config.json").OpenRead(),
            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase})!;


    public int N { get; set; }
    public ValueGenerationMode ValueGeneration { get; set; }
    public double FillValue { get; set; }

    public enum ValueGenerationMode
    {
        RandomValues = 1,
        FillValues = 2
    }
}