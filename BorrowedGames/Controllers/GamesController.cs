using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BorrowedGames.Data;
using BorrowedGames.Models;
using Microsoft.AspNetCore.Authorization;
using BorrowedGames.Data.Repositories.Interfaces;

namespace BorrowedGames.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGamesRepository _repository;

        public GamesController(IGamesRepository repository)
        {
            _repository = repository;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _repository.FindAll());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var game = await _repository.Find(id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Game game)
        {
            if (!ModelState.IsValid)
                return View(game);

            try
            {
                _repository.Add(game);
                await _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //TODO: Log message
                TempData["Exception"] = ex.Message;
                return View(game);
            }
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var game = await _repository.Find(id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Game game)
        {
            if (id != game.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(game);
                    await _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                        return NotFound();
                    else
                        throw;
                }
                catch (InvalidOperationException ex)
                {
                    //TODO: Log message
                    TempData["Exception"] = ex.Message;
                    return View(game);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var game = await _repository.Find(id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var game = await _repository.Find(id);
            _repository.Delete(game);
            await _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(long id)
        {
            return _repository.Find(id) != null;
        }
    }
}
