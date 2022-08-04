using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Archivo SubirArchivo { get; set; }

        public class Archivo
        {
            public IFormFile archivoName { get; set; }
            public string archivo { get; set; }
            public string descripcion { get; set; }

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ImportTicketsFromHTML()
        {
            try
            {
                var path = "C:/Users/Calvario/Downloads/" + SubirArchivo.archivoName.FileName;

                var existDirectory = Directory.Exists("C:/Users/Calvario/Downloads");

                XLWorkbook ArchivoExcel = new XLWorkbook(path);//ruta del libro excel
                var HojaExcel = ArchivoExcel.Worksheet(1);//hojas del libro        

                foreach (IXLRow row in HojaExcel.Rows())//recorrer las filas
                {
                    var ticket = new Models.Ticket
                    {
                        Titulo = row.Cell(1).GetValue<String>(),
                        Descripcion = row.Cell(2).GetValue<String>(),
                        Estatus = row.Cell(3).GetValue<String>(),
                        EncargadoNombre = row.Cell(4).GetValue<String>(),
                        EncargadoApellido1 = row.Cell(5).GetValue<String>(),
                        EncargadoApellido2 = row.Cell(6).GetValue<String>(),
                        EncargadoCorreo = row.Cell(7).GetValue<String>(),
                        Id = 20
                    };

                    _context.Tickets.Add(ticket);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {

            }
            return RedirectToPage("./Privacy");
        }
    }
}