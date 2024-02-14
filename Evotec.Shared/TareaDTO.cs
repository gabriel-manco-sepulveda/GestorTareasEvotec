using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Evotec.Shared
{
    public class TareaDTO
    {
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Descripcion { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int IdEstado { get; set; }

        public DateTime FechaInicio { get; set; }

        public EstadoDTO? Estado { get; set; }

    }
}