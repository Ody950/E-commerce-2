namespace E_commerce_2.Auth.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }

    }
}