using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DickinsDev.RblMonitor.Data;
using DickinsDev.RblMonitor.Data.Models;

namespace DickinsDev.RblMonitor.WebUI.Controllers
{
    public class NameserversController : Controller
    {
        private readonly DataContext _context;

        public NameserversController(DataContext context)
        {
            _context = context;
        }

        // GET: Nameservers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nameservers.ToListAsync());
        }

        // GET: Nameservers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nameserver = await _context.Nameservers
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (nameserver == null)
            {
                return NotFound();
            }

            return View(nameserver);
        }

        // GET: Nameservers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nameservers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,ServerName,IPAddress,isActive")] Nameserver nameserver)
        {
            if (ModelState.IsValid)
            {
                nameserver.Guid = Guid.NewGuid();
                _context.Add(nameserver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nameserver);
        }

        // GET: Nameservers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nameserver = await _context.Nameservers.FindAsync(id);
            if (nameserver == null)
            {
                return NotFound();
            }
            return View(nameserver);
        }

        // POST: Nameservers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,ServerName,IPAddress,isActive")] Nameserver nameserver)
        {
            if (id != nameserver.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nameserver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NameserverExists(nameserver.Guid))
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
            return View(nameserver);
        }

        // GET: Nameservers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nameserver = await _context.Nameservers
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (nameserver == null)
            {
                return NotFound();
            }

            return View(nameserver);
        }

        // POST: Nameservers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nameserver = await _context.Nameservers.FindAsync(id);
            _context.Nameservers.Remove(nameserver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NameserverExists(Guid id)
        {
            return _context.Nameservers.Any(e => e.Guid == id);
        }
    }
}
