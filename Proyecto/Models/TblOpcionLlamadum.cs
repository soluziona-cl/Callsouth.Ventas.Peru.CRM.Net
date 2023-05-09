using System;
using System.Collections.Generic;

#nullable disable

namespace CallSouth.Ventas.Peru.Models
{
    public class TblOpcionLlamadum
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int? Padre { get; set; }
        public string Agenda { get; set; }
    }
}
