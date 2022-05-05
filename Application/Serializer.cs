using System.Text.Json;
using Application.Core;
using Application.Models;

namespace Application;
public class JsonSerializer<T>
{
    public string? FileName { get; set; }
    public void serialize(T data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(Directory.GetCurrentDirectory() + $"\\{FileName}.json", json);
    }

    public T? deserialize()
    {
        string fileNameWithExtension = $"{FileName}.json";
        try
        {
            string jsonString = File.ReadAllText(fileNameWithExtension);
            T? data = JsonSerializer.Deserialize<T>(jsonString);
            return data;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"{fileNameWithExtension} is not found.");
        }
    }
}