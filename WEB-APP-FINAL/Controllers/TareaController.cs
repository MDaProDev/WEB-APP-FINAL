using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WEB_APP_FINAL.Data;
using WEB_APP_FINAL.Models;

namespace WEB_APP_FINAL.Controllers
{
    public class TareaController : Controller
    {
        private readonly AppDbContext _context;
        public TareaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tareas = await _context.Tareas.Include(p => p.Producto).ToListAsync();
            return View(tareas);

        }

        public IActionResult Create()
        {
            ViewData["Producto"] = new SelectList(_context.Productos, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind ("Id, Description, Date, Done, ProductoId")] Tarea tarea)
        {
            if(ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarea);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) return NotFound();

            var tarea = await _context.Tareas.FirstOrDefaultAsync(tarea => tarea.Id == Id);

            if (tarea == null) return NotFound();

            return View(tarea);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var tarea = await _context.Tareas.FindAsync(Id);
            if(tarea != null)
            {
                _context.Remove(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarea);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            ViewData["Producto"] = new SelectList(_context.Productos, "Id", "Name");

            if (Id == null) return NotFound();

            var tarea = await _context.Tareas.FindAsync(Id);
            if (Id == null) return NotFound();

            return View(tarea);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id, Description, Date, Done, ProductoId")] Tarea tarea)
        {
            if (Id != tarea.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();

                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(e => e.Id == Id)) return NotFound();
                    else
                        throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarea);
        }
    }
}
