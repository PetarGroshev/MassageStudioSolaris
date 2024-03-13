using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MassageStudioSolarisProject.Data
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceTypesId { get; set; }
        public ServiceType ServiceTypes { get; set; }
        public string Decription { get; set; }
        public int SpecialistsId { get; set; }
        public Specialist Specialists { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
