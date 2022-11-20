using BETareas.Models;
using Microsoft.EntityFrameworkCore;

namespace BETareas
{
    public class TaskContext: DbContext
    {
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario(){usuarioId = 1, nombre = "Juan", apellido = "Perez", email = "asd@asd", password = "1234", rol = "admin"});
            
            modelBuilder.Entity<Usuario>(
                entity => {
                    entity.ToTable("Usuarios");
                    entity.HasKey(e => e.usuarioId);
                    entity.Property(e => e.nombre).IsRequired();
                    entity.Property(e => e.apellido).IsRequired();
                    entity.Property(e => e.email).IsRequired();
                    entity.Property(e => e.password).IsRequired();
                    entity.Property(e => e.rol).IsRequired();
                    entity.HasData(usuarios);
                }
            );

            List<Tarea> tareas = new List<Tarea>();
            tareas.Add(new Tarea { tareaId = 1, nombre = "Tarea 1", descripcion = "Descripcion 1", fecha = DateTime.Now, usuarioId = 1 });
            

            modelBuilder.Entity<Tarea>(
                entity => {
                    entity.ToTable("Tareas");
                    entity.HasKey(e => e.tareaId);                    
                    entity.HasOne(e => e.Usuario).WithMany(p => p.Tareas).HasForeignKey(d => d.usuarioId);
                    entity.Property(e => e.nombre).IsRequired();
                    entity.Property(e => e.descripcion).IsRequired();
                    entity.Property(e => e.estado).IsRequired();
                    entity.Property(e => e.fecha).IsRequired();
                    entity.HasData(tareas);
                   
                }
            );

        }
    }
}