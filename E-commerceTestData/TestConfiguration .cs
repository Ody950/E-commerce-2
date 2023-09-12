using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

public class TestConfiguration : IConfiguration
{
    private readonly IConfigurationRoot _config;

    public TestConfiguration()
    {
        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:StorageAccount", "DefaultEndpointsProtocol=https;AccountName=storageaccountltuc4;AccountKey=ZPiDX1ySlj8uBdlTuKNVdP7YEYboczdd85vdMOG5QiZOY54aCWzT0MgnP/bmU8tFxeCkjimzEByG+AStE+/cig==;EndpointSuffix=core.windows.net" } // Replace with the actual connection string
            })
            .Build();
    }

    public string this[string key]
    {
        get => _config[key];
        set => throw new NotImplementedException(); // Not needed for testing purposes
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return _config.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
        return _config.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
        return _config.GetSection(key);
    }
}
