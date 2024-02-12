using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET_CYBER_ASP_ENTITY.Data;
using NET_CYBER_ASP_ENTITY.Data.Migrations;
using NET_CYBER_ASP_ENTITY.Models;

namespace NET_CYBER_ASP_ENTITY.Controllers
{
    [Authorize]
    public class UserMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _session;

        public UserMoviesController(ApplicationDbContext context, UserManager<AppUser> session)
        {
            _context = context;
            _session = session;
        }

        public async Task<IActionResult> AddFavorite(int id)
        {
            Movie movie = await _context.Movies.FindAsync(id);
            AppUser user = await _session.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null || movie == null)
            {
                return NotFound();
            }

            bool existFavorite = user.Movies.Any(m => m.Id == id);

            if (!existFavorite)
            {
                user.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _session.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);



            return View(user.Movies.ToList());
        }

    }
}