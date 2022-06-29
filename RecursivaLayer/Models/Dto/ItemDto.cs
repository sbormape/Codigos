using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaLayer.Models.Dto
{
    public class ItemDto
    {
        public string Equipo { get; set; }
        public int CantidadSocios { get; set; }
        public int EdadPromedio { get; set; }
        public int EdadMenor { get; set; }
        public int EdadMayor { get; set; }
    }
}