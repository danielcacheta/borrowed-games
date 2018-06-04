using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BorrowedGames.Models;
using BorrowedGames.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BorrowedGames.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IFriendsRepository _repository;

        public FriendsController(IFriendsRepository repository)
        {
            _repository = repository;
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            return View(await _repository.FindAll());
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var friend = await _repository.Find(id);

            if (friend == null)
                return NotFound();

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone")] Friend friend)
        {
            if (!ModelState.IsValid)
                return View(friend);

            try
            {
                _repository.Add(friend);
                await _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                //TODO: Log message
                TempData["Exception"] = ex.Message;
                return View(friend);
            }
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var friend = await _repository.Find(id);
            if (friend == null)
                return NotFound();

            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Phone")] Friend friend)
        {
            if (id != friend.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(friend);
                    await _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var friend = await _repository.Find(id);
            if (friend == null)
                return NotFound();

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var friend = await _repository.Find(id);
            _repository.Delete(friend);
            await _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(long id)
        {
            return _repository.Find(id) != null;
        }
    }
}
