using System.Text.Json;
using Application.Core;
using Application.Models;

namespace Application;
public class Serializer<T>
{
    private string _fileName;

    public Serializer(string filename)
    {
        _fileName = filename;
    }

    public void serialize(T data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(Directory.GetCurrentDirectory() + $"\\{_fileName}.json", json);
    }

    public Result<T> deserialize()
    {
        string fileName = $"{_fileName}.json";
        try
        {
            string jsonString = File.ReadAllText(fileName);
            T? data = JsonSerializer.Deserialize<T>(jsonString);
            if (data is not null)
                return Result<T>.Success(data);
        }
        catch (FileNotFoundException)
        {
            Result<T>.Failure($"{_fileName}.json is not found.");
        }
        return Result<T>.Failure($"Failed to deserialize data.");
    }
}