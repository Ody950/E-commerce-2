using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using E_commerce_2.Models.Services;
using E_commerce_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using E_commerce_2.Models.Services;
using E_commerce_2.Data;
using System.ComponentModel;




public class ProductServiceTests
{
    [Fact]
    public async Task CreateProduct_ShouldUploadBlobAndSaveProduct()
    {
        // Arrange
        var product = new Product();
        var formFile = new Mock<IFormFile>();
        var stream = new MemoryStream();
        formFile.Setup(f => f.FileName).Returns("test.jpg");
        formFile.Setup(f => f.ContentType).Returns("image/jpeg");
        formFile.Setup(f => f.OpenReadStream()).Returns(stream);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
        { "ConnectionStrings:StorageAccount", "\"DefaultEndpointsProtocol=https;AccountName=storageaccountltuc4;AccountKey=ZPiDX1ySlj8uBdlTuKNVdP7YEYboczdd85vdMOG5QiZOY54aCWzT0MgnP/bmU8tFxeCkjimzEByG+AStE+/cig==;EndpointSuffix=core.windows.net\"" } // Replace with the actual connection string
            })
            .Build();

        var context = new Mock<TheMarketDBContext>(); // Replace with your actual DbContext type

        var service = new ProductService(context.Object, configuration);

        var container = new BlobContainerClient("StorageAccount", "images"); // Create the container client

        // Act
        var createdProduct = await service.CreateProduct(product, formFile.Object);

        
        // Assert
        Assert.NotNull(createdProduct);
        Assert.NotNull(createdProduct.ImageURL);

        // Verify that the blob was uploaded
        var x = createdProduct.ImageURL;
        var blobName = Path.GetFileName(x.ToString()); // Extract the blob name from the URI
        var blobClient = container.GetBlobClient(blobName.ToString()); // Convert the blobName to a string
        var blobExists = await blobClient.ExistsAsync();
        Assert.True(blobExists);



        // Verify that the product was saved
        context.Verify(c => c.Add(product), Times.Once);
        context.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
}
