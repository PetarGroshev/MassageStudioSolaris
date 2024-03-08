namespace StudioSolaris.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ServicesId { get; set; }
        public Service Services { get; set; }
        public string ClientsId { get; set; }
        public Client Clients { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
