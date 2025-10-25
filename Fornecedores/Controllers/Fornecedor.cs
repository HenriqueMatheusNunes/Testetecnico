using GestaoFornecedores.Data;
using GestaoFornecedores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoFornecedores.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly AppDbContext _context;

        public FornecedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null && foto.Length > 0)
                {
                    var ext = Path.GetExtension(foto.FileName).ToLowerInvariant();
                    if (ext != ".png")
                    {
                        ModelState.AddModelError("Foto", "A imagem deve ser PNG.");
                        return View(fornecedor);
                    }

                    var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(imagesPath))
                        Directory.CreateDirectory(imagesPath);

                    var fileName = $"{Guid.NewGuid()}.png";
                    var filePath = Path.Combine(imagesPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }

                    fornecedor.Foto = "/images/" + fileName;
                }

                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return NotFound();

            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CNPJ,Segmento,CEP,Endereco,Foto")] Fornecedor fornecedor, IFormFile? foto)
        {
            if (id != fornecedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var fornecedorDb = await _context.Fornecedores.FindAsync(id);
                if (fornecedorDb == null) return NotFound();

                // Atualiza apenas os campos editáveis
                fornecedorDb.Nome = fornecedor.Nome;
                fornecedorDb.CNPJ = fornecedor.CNPJ;
                fornecedorDb.Segmento = fornecedor.Segmento;
                fornecedorDb.CEP = fornecedor.CEP;
                fornecedorDb.Endereco = fornecedor.Endereco;

                // Se veio nova foto, substitui
                if (foto != null && foto.Length > 0)
                {
                    var ext = Path.GetExtension(foto.FileName).ToLowerInvariant();
                    if (ext != ".png")
                    {
                        ModelState.AddModelError("Foto", "A imagem deve ser PNG.");
                        return View(fornecedorDb);
                    }

                    var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(imagesPath))
                        Directory.CreateDirectory(imagesPath);

                    var fileName = $"{Guid.NewGuid()}.png";
                    var filePath = Path.Combine(imagesPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }

                    fornecedorDb.Foto = "/images/" + fileName;
                }
                // Se não veio foto, mantém a antiga

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null) return NotFound();

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
