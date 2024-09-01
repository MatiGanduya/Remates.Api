namespace Remates.Api.Domain.EntitiesDTO
{
    public class ProductoCreateDto
    {
        public string? Nombre { get; set; }
        public decimal PrecioBase { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
        public int IdUsuario { get; set; }  // Solo el ID del usuario
    }

}
