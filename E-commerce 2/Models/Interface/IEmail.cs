namespace E_commerce_2.Models.Interface
{
    public interface IEmail
    {
        public Task SendEmailAsync(string email, string subject, string Message);

    }
}
