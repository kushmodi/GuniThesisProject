using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Thesis.Web.Data;
using Thesis.Web.Models;

namespace Thesis.Web.Controllers
{
    public class SubmissionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SubmissionDetailsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IConfiguration config)
           {
            _config = config;
            webHostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: SubmissionDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubmissionDetail.Include(s => s.Project).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> studentIndex()
        {
            var applicationDbContext = _context.SubmissionDetail.Include(s => s.Projects).Include(s => s.ProjectId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubmissionDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionDetail = await _context.SubmissionDetail
                .Include(s => s.Project)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SubmissionId == id);
            if (submissionDetail == null)
            {
                return NotFound();
            }

            return View(submissionDetail);
        }

        // GET: SubmissionDetails/Create
        public IActionResult Create()
        {
            var temp = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["User"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SubmissionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubmissionId,ProjectId,SubmissionDescription,CompletionPercentage,SubmissionDueDate,SubmissionDate,SubmissionFile")] SubmissionDetail submissionDetail)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(submissionDetail.SubmissionFilePath);
               
                string fileextention = Path.GetExtension(submissionDetail.SubmissionFile.FileName);
                string FilePath = filename + DateTime.Now.ToString("yymmssfff") + fileextention;
                submissionDetail.SubmissionFilePath = filename + DateTime.Now.ToString("yymmssfff") + fileextention;
                string path = Path.Combine(wwwRootPath, "Document", filename);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await submissionDetail.SubmissionFile.CopyToAsync(filestream);
                }
                _context.Add(submissionDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(studentIndex));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", submissionDetail.ProjectId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName", submissionDetail.Id);
            return View(submissionDetail);
        }

        // GET: SubmissionDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionDetail = await _context.SubmissionDetail.FindAsync(id);
            if (submissionDetail == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", submissionDetail.ProjectId);
           var idupper = submissionDetail.Id.ToString();
            ViewData["Id"] = submissionDetail.Id;
            ViewData["User"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();
            return View(submissionDetail);
        }

        // POST: SubmissionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubmissionId,ProjectId,SubmissionDescription,SubmissionDueDate,SubmissionDate,SubmissionFilePath")] SubmissionDetail submissionDetail)
        {
            if (id != submissionDetail.SubmissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submissionDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionDetailExists(submissionDetail.SubmissionId))
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
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", submissionDetail.ProjectId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName", submissionDetail.Id);
            return View(submissionDetail);
        }

        // GET: SubmissionDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submissionDetail = await _context.SubmissionDetail
                .Include(s => s.Project)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SubmissionId == id);
            if (submissionDetail == null)
            {
                return NotFound();
            }

            return View(submissionDetail);
        }

        // POST: SubmissionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submissionDetail = await _context.SubmissionDetail.FindAsync(id);
            _context.SubmissionDetail.Remove(submissionDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionDetailExists(int id)
        {
            return _context.SubmissionDetail.Any(e => e.SubmissionId == id);
        }
    }
}
