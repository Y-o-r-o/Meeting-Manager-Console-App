using System.Text.Json;
using Application.Core;
using Application.Models;

namespace Application;
public class JsonSerializer<T>: ISerializer<T>
{
    public string? FileName { get; set; }
    public void Serialize(T data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(Directory.GetCurrentDirectory() + $"\\{FileName}.json", json);
    }

    public T Deserialize()
    {
        string fileNameWithExtension = $"{FileName}.json";
        try
        {
            string jsonString = File.ReadAllText(fileNameWithExtension);
            T data = JsonSerializer.Deserialize<T>(jsonString);
            if (data is null) throw new FileLoadException();
            return data;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"{fileNameWithExtension} is not found.");
        }
    }
}