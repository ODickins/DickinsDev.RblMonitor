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
    public class EmailTargetsController : Controller
    {
        private readonly DataContext _context;

        public EmailTargetsController(DataContext context)
        {
            _context = context;
        }

        // GET: EmailTargets
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailTargets.ToListAsync());
        }

        // GET: EmailTargets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailTarget = await _context.EmailTargets
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (emailTarget == null)
            {
                return NotFound();
            }

            return View(emailTarget);
        }

        // GET: EmailTargets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailTargets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,EmailAddress,isActive")] EmailTarget emailTarget)
        {
            if (ModelState.IsValid)
            {
                emailTarget.Guid = Guid.NewGuid();
                _context.Add(emailTarget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailTarget);
        }

        // GET: EmailTargets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailTarget = await _context.EmailTargets.FindAsync(id);
            if (emailTarget == null)
            {
                return NotFound();
            }
            return View(emailTarget);
        }

        // POST: EmailTargets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,EmailAddress,isActive")] EmailTarget emailTarget)
        {
            if (id != emailTarget.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailTarget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailTargetExists(emailTarget.Guid))
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
            return View(emailTarget);
        }

        // GET: EmailTargets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailTarget = await _context.EmailTargets
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (emailTarget == null)
            {
                return NotFound();
            }

            return View(emailTarget);
        }

        // POST: EmailTargets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var emailTarget = await _context.EmailTargets.FindAsync(id);
            _context.EmailTargets.Remove(emailTarget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailTargetExists(Guid id)
        {
            return _context.EmailTargets.Any(e => e.Guid == id);
        }
    }
}
