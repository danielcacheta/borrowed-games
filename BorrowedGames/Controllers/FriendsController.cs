using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BorrowedGames.Models;
using BorrowedGames.Data.Repositories.Interfaces;

namespace BorrowedGames.Controllers
{
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
            var friends = _repository.FindAll();
            return View(friends);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var friend = _repository.Find(id);

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
        public async Task<IActionResult> Create([Bind("Name,Phone")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(friend);
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var friend = _repository.Find(id);
            if (friend == null)
                return NotFound();

            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Phone")] Friend friend)
        {
            if (id != friend.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(friend);
                    _repository.Save();
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
            var friend = _repository.Find(id);
            if (friend == null)
                return NotFound();

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var friend = _repository.Find(id);
            _repository.Delete(friend);
            _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(long id)
        {
            return _repository.Find(id) != null;
        }
    }
}
