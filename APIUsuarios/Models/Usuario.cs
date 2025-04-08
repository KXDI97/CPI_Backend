using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIUsuarios.Models
{
    public class Usuario
    {
        [Key]
        [Column("Doc_Identidad")]
        [StringLength(12)]
        public string DocIdentidad { get; set; }

        [Required]
        [Column("Nom_Usuario")]
        [StringLength(50)]
        public string NomUsuario { get; set; }

        [Required]
        [Column("Correo")]
        [StringLength(27)]
        public string Correo { get; set; }

        [Column("Num_tel")]
        [StringLength(15)]
        public string? NumTel { get; set; }

        [Required]
        [Column("Contrasenia")]
        [StringLength(15)]
        public string Contrasenia { get; set; }

        [Required]
        [Column("Cod_Rol")]
        [StringLength(10)]
        public string CodRol { get; set; }

        // Relación con la tabla Roles (si la vas a mapear)
        // public Rol Rol { get; set; } // ← Descomenta si también tienes la clase Rol
    }
}
