namespace StudioSolaris.Data
{
    public class Specialist
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
        public ICollection<Service> Services { get; set; }
       
    }
}