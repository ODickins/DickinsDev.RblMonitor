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
    public class DNSBLController : Controller
    {
        private readonly DataContext _context;

        public DNSBLController(DataContext context)
        {
            _context = context;
        }

        // GET: DNSBL
        public async Task<IActionResult> Index()
        {
            return View(await _context.DNSBLs.ToListAsync());
        }

        // GET: DNSBL/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dNSBL = await _context.DNSBLs
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (dNSBL == null)
            {
                return NotFound();
            }

            return View(dNSBL);
        }

        // GET: DNSBL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DNSBL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,RblName,ZoneName,isActive")] DNSBL dNSBL)
        {
            if (ModelState.IsValid)
            {
                dNSBL.Guid = Guid.NewGuid();
                _context.Add(dNSBL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dNSBL);
        }

        // GET: DNSBL/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dNSBL = await _context.DNSBLs.FindAsync(id);
            if (dNSBL == null)
            {
                return NotFound();
            }
            return View(dNSBL);
        }

        // POST: DNSBL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,RblName,ZoneName,isActive")] DNSBL dNSBL)
        {
            if (id != dNSBL.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dNSBL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DNSBLExists(dNSBL.Guid))
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
            return View(dNSBL);
        }

        // GET: DNSBL/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dNSBL = await _context.DNSBLs
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (dNSBL == null)
            {
                return NotFound();
            }

            return View(dNSBL);
        }

        // POST: DNSBL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dNSBL = await _context.DNSBLs.FindAsync(id);
            _context.DNSBLs.Remove(dNSBL);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DNSBLExists(Guid id)
        {
            return _context.DNSBLs.Any(e => e.Guid == id);
        }
    }
}
