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
    public class IPMonitorsController : Controller
    {
        private readonly DataContext _context;

        public IPMonitorsController(DataContext context)
        {
            _context = context;
        }

        

        // GET: IPMonitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.IPMonitors.ToListAsync());
        }

        // GET: IPMonitors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPMonitor = await _context.IPMonitors
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (iPMonitor == null)
            {
                return NotFound();
            }

            return View(iPMonitor);
        }

        // GET: IPMonitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IPMonitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,IPName,IPAddress,isActive,LastCheck,CheckInterval,isClean")] IPMonitor iPMonitor)
        {
            if (ModelState.IsValid)
            {
                iPMonitor.Guid = Guid.NewGuid();
                _context.Add(iPMonitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iPMonitor);
        }

        // GET: IPMonitors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPMonitor = await _context.IPMonitors.FindAsync(id);
            if (iPMonitor == null)
            {
                return NotFound();
            }
            return View(iPMonitor);
        }

        // POST: IPMonitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,IPName,IPAddress,isActive,LastCheck,CheckInterval,isClean")] IPMonitor iPMonitor)
        {
            if (id != iPMonitor.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iPMonitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IPMonitorExists(iPMonitor.Guid))
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
            return View(iPMonitor);
        }

        // GET: IPMonitors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPMonitor = await _context.IPMonitors
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (iPMonitor == null)
            {
                return NotFound();
            }

            return View(iPMonitor);
        }

        // POST: IPMonitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var iPMonitor = await _context.IPMonitors.FindAsync(id);
            _context.IPMonitors.Remove(iPMonitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IPMonitorExists(Guid id)
        {
            return _context.IPMonitors.Any(e => e.Guid == id);
        }
    }
}
