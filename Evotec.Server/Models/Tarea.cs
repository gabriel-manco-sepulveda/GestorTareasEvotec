using System;
using System.Collections.Generic;

namespace Evotec.Server.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdEstado { get; set; }

    public DateTime FechaInicio { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}