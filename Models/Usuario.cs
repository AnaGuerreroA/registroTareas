using System.Text.Json.Serialization;
namespace BETareas.Models
{
    public class Usuario
    {
        public int usuarioId { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tarea> Tareas { get; set; }
        
        public Usuario()
        {
            usuarioId = 0;
            nombre = string.Empty;
            apellido = string.Empty;
            email = string.Empty;
            password = string.Empty;
            rol = string.Empty;
        }

    }
}