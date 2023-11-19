using contactsCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace contactsCRUD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Contacts> Contacts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Contacts = await _context.Contacts.ToListAsync();
        }

        [BindProperty]
        public Contacts Contact { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid || _context.Contacts == null || Contact == null)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }
            var contacts = await _context.Contacts.FindAsync(id);

            if (contacts != null)
            {
                Contact = contacts;
                _context.Contacts.Remove(Contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var temp = Contact;

            _context.Attach(temp).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetFindContactAsync(int id)
        {
            Contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            return new JsonResult(Contact);
        }
    }
}