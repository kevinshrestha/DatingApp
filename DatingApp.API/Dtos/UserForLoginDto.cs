namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        //API will check what's in our database
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}