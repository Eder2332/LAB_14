using System;
using System.Collections.Generic;

namespace Lab05_Ederson_Vilca.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
