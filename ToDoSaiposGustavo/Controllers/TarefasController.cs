using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoSaiposGustavo.Models;

namespace ToDoSaiposGustavo.Controllers
{
    public class TarefasController : Controller
    {
        private readonly Contexto _context;

        public TarefasController(Contexto context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarefa.ToListAsync());
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Descricao,NomeResponsavel,EmailResponsavel")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Descricao,NomeResponsavel,EmailResponsavel,Status,QtdPendente")] Tarefa tarefa)
        {
            if (id != tarefa.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.ID))
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
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tarefas/Concluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Concluir(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            tarefa.Status = 1;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tarefas/Reativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            tarefa.Status = 0;
            tarefa.QtdPendente++;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult EstouSemTarefas(string dogFact1, string dogFact2, string dogFact3)
        {
            Tarefa tarefa1 = new Tarefa();
            tarefa1.Descricao = dogFact1;
            tarefa1.NomeResponsavel = "Eu";
            tarefa1.EmailResponsavel = "eu@me.com";

            Tarefa tarefa2 = new Tarefa();
            tarefa2.Descricao = dogFact2;
            tarefa2.NomeResponsavel = "Eu";
            tarefa2.EmailResponsavel = "eu@me.com";

            Tarefa tarefa3 = new Tarefa();
            tarefa3.Descricao = dogFact3;
            tarefa3.NomeResponsavel = "Eu";
            tarefa3.EmailResponsavel = "eu@me.com";

            _context.Add(tarefa1);
            _context.Add(tarefa2);
            _context.Add(tarefa3);
            _context.SaveChanges();
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(true);
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.ID == id);
        }
    }
}
