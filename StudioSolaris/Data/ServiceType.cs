namespace StudioSolaris.Data
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Service> Services { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}