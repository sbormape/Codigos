using RecursivaLayer.Models;
using RecursivaLayer.Models.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RecursivaLayer.Controllers
{
    public class SocioController : Controller
    {
        //
        // GET: /Socio/

        public ActionResult Socios()
        {
            return View(new ResultadoDto());
        }

        [HttpPost]
        public ActionResult Socios(HttpPostedFileBase postedFile)
        {
            ResultadoDto resultadoDto = new ResultadoDto();
            List<Socio> socios = new List<Socio>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath, Encoding.GetEncoding("iso-8859-1"));
                string[] rows = csvData.Split('\n');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        socios.Add(new Socio
                        {                            
                            Nombre = row.Split(';')[0],
                            Edad = Convert.ToInt32(row.Split(';')[1]),
                            Equipo = row.Split(';')[2],
                            EstadoCivil = row.Split(';')[3],
                            NivelEstudios = row.Split(';')[4]
                        });
                    }
                }
            }
            resultadoDto.CantidadTotal = socios.Count;
            resultadoDto.EdadPromedio = (int) Math.Round(socios.Where(x => x.Equipo == "Racing").Average(x => x.Edad));
            resultadoDto.Listado1 = socios.Where(x => x.EstadoCivil == "Casado" && x.NivelEstudios == "Universitario\r").Take(100).OrderBy(x => x.Edad).ToList();
            resultadoDto.Listado2 = socios.Where(x => x.Equipo == "River").GroupBy(x => x.Nombre).Select(d => new { d.Key, size = d.Count() }).OrderBy(x => x.size).Take(5).Select(x => x.Key).ToList() ;
            List<ItemDto> listado3 = new List<ItemDto>();
            List<string> equipos = socios.Select(x => x.Equipo).Distinct().ToList();
            foreach (string equipo in equipos)
            {
                List<Socio> sociosEquipo = socios.Where(x => x.Equipo == equipo).ToList();
                ItemDto item = new ItemDto();
                item.Equipo = equipo;
                item.CantidadSocios = sociosEquipo.Count;
                item.EdadPromedio = (int) Math.Round(sociosEquipo.Average(x => x.Edad));
                item.EdadMenor = sociosEquipo.Min(x => x.Edad);
                item.EdadMayor = sociosEquipo.Max(x => x.Edad);
                listado3.Add(item);
            }

            resultadoDto.Listado3 = listado3.OrderByDescending(x => x.CantidadSocios).ToList();
            return View(resultadoDto);
        }

    }
}
