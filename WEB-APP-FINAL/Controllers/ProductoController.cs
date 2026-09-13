using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WEB_APP_FINAL.Data;
using WEB_APP_FINAL.Models;

namespace WEB_APP_FINAL.Controllers
{
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();
            return View(productos);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, Stock, Tareas")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(Id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id, Name, Price, Stock, Tareas")] Producto producto)
        {
            if (Id != producto.Id) return NotFound();
            if (ModelState.IsValid)
            if (producto.Price<0.0)
            {
                try
                {
                    _context.Productos.Update(producto);
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
            return View(producto);           
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) return NotFound();

            var producto = await _context.Productos.FirstOrDefaultAsync(producto => producto.Id == Id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var producto = await _context.Productos.FindAsync(Id);

            if (producto != null)
            {
                _context.Remove(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }
        
        public IActionResult Detail(int Id)
        {
            Producto producto = _context.Productos.FirstOrDefault(producto => producto.Id == Id);
           
            return View(producto);
        }
    }
}
