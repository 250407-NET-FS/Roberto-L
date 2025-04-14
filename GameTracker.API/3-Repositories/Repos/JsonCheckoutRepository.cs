using System.Text.Json;
using GameTracker.Models;

namespace GameTracker.Repositories;

public class JsonCheckoutRepository : ICheckoutRepository
{
    private readonly string _filePath;

    public JsonCheckoutRepository()
    {
        _filePath = Path.Combine("./5-Data-Files/checkouts.json");
    }

    public List<Checkout> GetAllCheckouts()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Checkout>();

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Checkout>>(stream) ?? new List<Checkout>();
        }

        catch
        {
            throw new Exception("Failed to retrive checkout.");
        }
    }

    public Checkout? GetCheckoutById(string Id)
    {
        if (Id is null) return null;
        var checkouts = GetAllCheckouts();
        return checkouts.FirstOrDefault(c => c.Id == Id);
    }

    public void AddCheckout(Checkout checkout)
    {
        var checkouts = GetAllCheckouts();
        checkouts.Add(checkout);
        SaveCheckouts(checkouts);
    }

    private void SaveCheckouts(List<Checkout> checkouts)
    {
        try
        {
            var json = JsonSerializer.Serialize(checkouts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            throw new Exception("Failed to save checkouts to file.");
        }
    }
}