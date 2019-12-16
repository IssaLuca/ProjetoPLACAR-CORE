using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Top10Trab.Data;
using Top10Trab.Models;

namespace Top10Trab.Controllers
{
    public class PlacarsController : Controller
    {
        private readonly DBTop10Contexto _context;

        public PlacarsController(DBTop10Contexto context)
        {
            _context = context;
        }

        // GET: Placars
        public async Task<IActionResult> Index()
        {
            var dBTop10Contexto = _context.Placares.Include(p => p.Jogador);
            return View(await dBTop10Contexto.ToListAsync());
        }

        // GET: Placars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await _context.Placares
                .Include(p => p.Jogador)
                .FirstOrDefaultAsync(m => m.PlacarID == id);
            if (placar == null)
            {
                return NotFound();
            }

            return View(placar);
        }

        // GET: Placars/Create
        public IActionResult Create()
        {
            ViewData["JogadorID"] = new SelectList(_context.Jogadores, "JogadorID", "Nome");
            return View();
        }

        // POST: Placars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlacarID,JogadorID,Data,Pontuacao")] Placar placar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JogadorID"] = new SelectList(_context.Jogadores, "JogadorID", "Nome", placar.JogadorID);
            return View(placar);
        }

        public IActionResult Melhores()
        {
            DBTop10Contexto db = new DBTop10Contexto();
            List<Placar> placares = db.Placares.OrderByDescending(p => p.Pontuacao).ToList();
            List<Placar> melhores = new List<Placar>();

            for (int i = 0; (i < placares.Count && i < 10); i++)
            {
                melhores.Add(placares[i]);
            }
            return View(melhores);
        }

        // GET: Placars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await _context.Placares.FindAsync(id);
            if (placar == null)
            {
                return NotFound();
            }
            ViewData["JogadorID"] = new SelectList(_context.Jogadores, "JogadorID", "Nome", placar.JogadorID);
            return View(placar);
        }

        // POST: Placars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlacarID,JogadorID,Data,Pontuacao")] Placar placar)
        {
            if (id != placar.PlacarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacarExists(placar.PlacarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JogadorID"] = new SelectList(_context.Jogadores, "JogadorID", "Nome", placar.JogadorID);
            return View(placar);
        }

        // GET: Placars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await _context.Placares
                .Include(p => p.Jogador)
                .FirstOrDefaultAsync(m => m.PlacarID == id);
            if (placar == null)
            {
                return NotFound();
            }

            return View(placar);
        }

        // POST: Placars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placar = await _context.Placares.FindAsync(id);
            _context.Placares.Remove(placar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacarExists(int id)
        {
            return _context.Placares.Any(e => e.PlacarID == id);
        }
    }
}
