using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursivaLayer.Models.Dto
{
    public class ResultadoDto
    {
        public int CantidadTotal { get; set; }
        public int EdadPromedio { get; set; }
        public List<Socio> Listado1 { get; set; }
        public List<string> Listado2 { get; set; }
        public List<ItemDto> Listado3 { get; set; }
    }
}