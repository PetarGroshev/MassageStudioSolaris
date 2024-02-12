namespace StudioSolaris.Data
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MassagesId { get; set; }
        public Massage Massages { get; set; }
        public int ServicesTypeId { get; set; }
        public ServiceType ServicesTypes { get; set; }
        public string Decription { get; set; }
        public int SpecialistsId { get; set; }
        public Specialist Specialists { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
