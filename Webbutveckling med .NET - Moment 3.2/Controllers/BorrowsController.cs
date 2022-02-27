#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webbutveckling_med_.NET___Moment_3._2.Data;
using Webbutveckling_med_.NET___Moment_3._2.Models;

namespace Webbutveckling_med_.NET___Moment_3._2.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly CDContext _context;

        public BorrowsController(CDContext context)
        {
            _context = context;
        }

        // GET: Borrows
        public async Task<IActionResult> Index()
        {
            return View(await _context.Borrow.Include(x => x.Album).ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow
                .FirstOrDefaultAsync(m => m.BorrowId == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // GET: Borrows/Create
        public IActionResult Create()
        {
            //Bring back all the albums
            var albums = _context.Album.ToListAsync().Result;
            //Bring back all the borrowed info
            var borrowedInfo = _context.Borrow.ToListAsync().Result;

            //List of all the borrows album ids
            var borrowedIds = borrowedInfo.Select(x => x.Album.AlbumId).ToList();
            
            //Check if album doesn't exist in borrows albums
            var unBorrowedAlbums = albums.Where(x => !borrowedIds.Contains(x.AlbumId)).ToList();

            //Viewbag of unborrows albums
            ViewBag.Albums = unBorrowedAlbums;
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Firstname,Lastname,AlbumId")] BorrowView borrow)
        {
            if (ModelState.IsValid)
            {
                //Bring back album based on id
                var album = await _context.Album.FindAsync(borrow.AlbumId);

                //Initiate new database model (Borrow)
                var borrowDb = new Borrow
                {
                    //Set BorrowView to Borrow model
                    Firstname = borrow.Firstname,
                    Lastname = borrow.Lastname,                   
                    Album = album,
                };

                //add to context
                _context.Add(borrowDb);
                //save
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowId,Date,Firstname,Lastname")] Borrow borrow)
        {
            if (id != borrow.BorrowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowExists(borrow.BorrowId))
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
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow
                .FirstOrDefaultAsync(m => m.BorrowId == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrow = await _context.Borrow.FindAsync(id);
            _context.Borrow.Remove(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowExists(int id)
        {
            return _context.Borrow.Any(e => e.BorrowId == id);
        }
    }
}
