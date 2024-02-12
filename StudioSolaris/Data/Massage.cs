namespace StudioSolaris.Data
{
    public class Massage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Service> Services { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}