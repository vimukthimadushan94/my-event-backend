namespace my_event_backend.Dtos
{
    public class UpdateProfileDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
