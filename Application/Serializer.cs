using System.Text.Json;
using Application.Core;
using Application.Models;

namespace Application;
public class Serializer<T>
{
    private string _fileName;

    public Serializer()
    {
        _fileName = typeof(T).ToString();
    }

    public void serialize(List<T> data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(Directory.GetCurrentDirectory() + $"\\{_fileName}.json", json);
    }

    public Result<List<T>> deserialize()
    {
        string fileName = $"{_fileName}.json";
        try
        {
            string jsonString = File.ReadAllText(fileName);
            List<T>? data = JsonSerializer.Deserialize<List<T>>(jsonString);
            if (data is not null)
                return Result<List<T>>.Success(data);
        }
        catch (FileNotFoundException)
        {
            Result<List<T>>.Failure($"{_fileName}.json is not found.");
        }
        return Result<List<T>>.Failure("Failed to deserialize data.");
    }
}