namespace E_commerce_2.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri Logo { get; set; }
        public string Description { get; set; }

        public List<CategoriesProduct> categoriesProducts { get; set; }

    }
}