namespace E_commerce_2.Models.Interface

{
    public interface IProduct
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> Create(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task Delete(int id);
    }
}