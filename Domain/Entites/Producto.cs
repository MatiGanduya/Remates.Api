using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remates.Api.Domain.Entites
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idProducto")]
        public int IdProducto { get; set; }

        [Column("nombre")]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Column("precioBase", TypeName = "decimal(10,2)")]
        public decimal PrecioBase { get; set; }

        [Column("imagen")]
        public byte[]? Imagen { get; set; }

        [Column("descripcion")]
        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [Column("estado")]
        public bool? Estado { get; set; }

        // Clave foránea para la relación con Usuario
        public int IdUsuario { get; set; }

        // Propiedad de navegación hacia Usuario
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}

