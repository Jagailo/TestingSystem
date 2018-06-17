namespace StudentTestingSystem.Areas.Student.Models.Profile
{
    public class ProfileDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public int GroupNumber { get; set; }
        public string FullName  => FirstName + " " + LastName;
    }
}