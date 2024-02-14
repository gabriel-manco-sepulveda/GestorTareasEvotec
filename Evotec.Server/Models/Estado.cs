using System;
using System.Collections.Generic;

namespace Evotec.Server.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();
}
