using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remates.Api.Domain.Entites
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idUsuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Column("apellido")]
        [MaxLength(50)]
        public string? Apellido { get; set; }
        
        [Column("direccion")]
        [MaxLength(50)]
        public string? Direccion { get; set; }
        
        [Column("ciudad")]
        [MaxLength(50)]
        public string? Ciudad { get; set; }
        
        [Column("email")]
        [Required, MaxLength(50)]
        public string Email { get; set; }
        
        [Column("contrasena")]
        [Required, MaxLength(50)]
        public string Contrasena { get; set; }

        // Propiedad de navegación para representar la relación con Productos
        public ICollection<Producto> Productos { get; set; }
    }
}
