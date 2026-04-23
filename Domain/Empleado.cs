using System;

namespace TempoControl.Domain
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string Departamento { get; set; } = "";
        public string Posicion { get; set; } = "";
        public bool Activo { get; set; }

        public Empleado()
        {
            Activo = true;
        }
    }
}