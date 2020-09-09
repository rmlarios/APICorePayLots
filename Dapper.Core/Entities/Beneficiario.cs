using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Core.Entities
{
    public class Beneficiarios
    {
        public Beneficiarios()
        {
           Asignaciones = new Collection<Asignaciones>();
        }
        [Key]        
        [Required]
        public int IdBeneficiario { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        public string CedulaIdentidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        [InverseProperty("IdBeneficiarioNavigation")]
        public ICollection<Asignaciones> Asignaciones { get; set; }
    }
}