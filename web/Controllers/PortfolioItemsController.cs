using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastracture;

namespace web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly DataContext _context;

        public PortfolioItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: PortfolioItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioItems.ToListAsync());
        }

        // GET: PortfolioItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectName,Description,ImageUrl,Id")] PortfolioItem portfolioItem)
        {
            if (ModelState.IsValid)
            {
                portfolioItem.Id = Guid.NewGuid();
                _context.Add(portfolioItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioItem);
        }

        // GET: PortfolioItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems.FindAsync(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            return View(portfolioItem);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectName,Description,ImageUrl,Id")] PortfolioItem portfolioItem)
        {
            if (id != portfolioItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(portfolioItem.Id))
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
            return View(portfolioItem);
        }

        // GET: PortfolioItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var portfolioItem = await _context.PortfolioItems.FindAsync(id);
            _context.PortfolioItems.Remove(portfolioItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _context.PortfolioItems.Any(e => e.Id == id);
        }
    }
}
