using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApplication.HttpServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AdminApplication.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookServices _context;

        public BooksController(BookServices context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllBooks());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDTO = await _context.FindBook(id);
            if (bookDTO == null)
            {
                return NotFound();
            }

            return View(bookDTO);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Description,AuthorId,Year")] BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateNewBooks(bookDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDTO);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDTO = await _context.FindBook(id);
            if (bookDTO == null)
            {
                return NotFound();
            }
            return View(bookDTO);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Content,Description,AuthorId,Year")] BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateBook(bookDTO);
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookDTO);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDTO = await _context.FindBook(id);
            if (bookDTO == null)
            {
                return NotFound();
            }

            return View(bookDTO);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bookDTO = await _context.FindBook(id);
            await _context.Remove(bookDTO);
            return RedirectToAction(nameof(Index));
        }
    }
}
