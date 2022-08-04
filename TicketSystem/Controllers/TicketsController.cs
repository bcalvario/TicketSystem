using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;
using ExcelDataReader;
using System.IO;
using System.Linq;
using System.Collections;
using ClosedXML.Excel;

namespace TicketSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Archivo SubirArchivo { get; set; }

        public class Archivo
        {
            public string descricion { get; set; }
            public IFormFile archivo { get; set; }
        }

        // GET: Tickets with status Index
        public async Task<IActionResult> Index()
        {
              return _context.Tickets != null ? 
                          View(await _context.Tickets.ToListAsync()) :
                          Problem("No se encontro el ticket");
        }

        // GET: Tickets with status Open
        public async Task<IActionResult> Open()
        {
            return _context.Tickets != null ?
                        View(await _context.Tickets.Where(s => s.Estatus.Trim().ToLower() == "abierto").ToListAsync()) :
                        Problem("No se encontro el ticket");
        }

        // GET: Tickets with status InProgress
        public async Task<IActionResult> InProgress()
        {
            return _context.Tickets != null ?
                        View(await _context.Tickets.Where(s => s.Estatus.Trim().ToLower() == "en progreso").ToListAsync()) :
                        Problem("No se encontro el ticket");
        }

        // GET: Tickets with status Done
        public async Task<IActionResult> Done()
        {
            return _context.Tickets != null ?
                        View(await _context.Tickets.Where(s => s.Estatus.Trim().ToLower() == "resuelto").ToListAsync()) :
                        Problem("No se encontro el ticket");
        }

        // GET: Tickets with status Back
        public async Task<IActionResult> Back()
        {
            return _context.Tickets != null ?
                        View(await _context.Tickets.Where(s => s.Estatus.Trim().ToLower() == "necesita reembolso").ToListAsync()) :
                        Problem("No se encontro el ticket");
        }

        // Post: Import tickets from Excel
        public async Task<IActionResult> ImportTicketsFromHTML()
        {
            try
            {
                var path = "C:/Users/Calvario/Downloads/Tickets.xlsx";

                XLWorkbook ArchivoExcel = new XLWorkbook(path);
                var HojaExcel = ArchivoExcel.Worksheet(1);

                var isFirstRow = true;

                foreach (IXLRow row in HojaExcel.Rows())
                {
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    var ticket = new Models.Ticket
                    {
                        Titulo = row.Cell(1).GetValue<String>(),
                        Descripcion = row.Cell(2).GetValue<String>(),
                        Estatus = row.Cell(3).GetValue<String>(),
                        EncargadoNombre = row.Cell(4).GetValue<String>(),
                        EncargadoApellido1 = row.Cell(5).GetValue<String>(),
                        EncargadoApellido2 = row.Cell(6).GetValue<String>(),
                        EncargadoCorreo = row.Cell(7).GetValue<String>(),
                    };

                    _context.Tickets.Add(ticket);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {

            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Estatus,EncargadoNombre,EncargadoApellido1,EncargadoApellido2,EncargadoCorreo")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Estatus,EncargadoNombre,EncargadoApellido1,EncargadoApellido2,EncargadoCorreo")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticket.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //return View(ticket);
        }

        // GET: Tickets/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("No se encontro el ticket");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
