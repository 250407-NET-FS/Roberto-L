using System.Text.Json;
using GameTracker.Models;

namespace GameTracker.Repositories;

public class JsonWorkerRepository : IWorkerRepository
{
    private readonly string _filePath;

    public JsonWorkerRepository()
    {
        _filePath = Path.Combine("./5-Data-Files/workers.json");
    }

    public List<Worker> GetAllWorkers()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Worker>();

            using var stream = File.OpenRead(_filePath);
            return JsonSerializer.Deserialize<List<Worker>>(stream) ?? new List<Worker>();
        }
        catch
        {
            throw new Exception("Failed to retrive workers");
        }
    }

    public Worker? GetWorkerById(string id)
    {
        if (id is null) return null;

        var workers = GetAllWorkers();
        return workers.FirstOrDefault(w => w.Id == id);
    }

    public void AddWorker(Worker worker)
    {
        var workers = GetAllWorkers();
        workers.Add(worker);
        SaveWorkers(workers);
    }

    public void UpdateWorker(Worker worker)
    {
        var workers = GetAllWorkers();
        var index = workers.FindIndex(w => w.Id == worker.Id);
        if (index >= 0)
        {
            workers[index] = worker;
            SaveWorkers(workers);
        }
        else throw new Exception("Worker not found");
    }

    private void SaveWorkers(List<Worker> workers)
    {
        try
        {
            var json = JsonSerializer.Serialize(workers, new JsonSerializerOptions { WriteIndented = true });
            if (string.IsNullOrEmpty(_filePath))
                throw new Exception("File path is not set.");
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            throw new Exception("Failed to save workers to file.");
        }
    }
}