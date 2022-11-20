namespace BETareas.Models
{
    public class Tarea
    {
        public int tareaId { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }        
        public bool estado { get; set; }
        public DateTime fecha { get; set; }
        public int usuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
                
        public Tarea()
        {
            tareaId = 0;
            nombre = string.Empty;
            descripcion = string.Empty;
            estado = false;
            fecha = DateTime.Now;  
        }        
        
    }
}