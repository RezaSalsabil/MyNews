using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNews.Models;

namespace MyNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyNewsContext _context;
        public HomeController(MyNewsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliderNews = await _context.TblNews.Where(i => i.IsSlide).OrderByDescending(i => i.DatePublished).Take(5).ToListAsync();
            var LatestNews = await _context.TblNews.Include(i => i.Category).OrderByDescending(i => i.DatePublished).Take(9).ToListAsync();
            var PoliticNews = await _context.TblNews.Where(i => i.CategoryId == 1).OrderByDescending(i => i.DatePublished).Take(4).ToListAsync();
            var economyNews = await _context.TblNews.Where(i => i.CategoryId == 5).OrderByDescending(i => i.DatePublished).Take(4).ToListAsync();
            var mostViewed = await _context.TblNews.Include(i => i.Category).OrderByDescending(i => i.ViewCount).Take(4).ToListAsync();
            var tags = await _context.TblKeyword.ToListAsync();

            (List<TblNews> slider, List<TblNews> latest , List<TblNews> Politic,List<TblNews> Economy, List<TblNews> mostviewed , List<TblKeyword> tags) result = new(sliderNews, LatestNews, PoliticNews, economyNews, mostViewed, tags);
            return View(result);
        }
          public async Task<IActionResult> Profile()
        {
            return View();
        }

        public IActionResult contactUs()
        {
            return View();
        }

        public async Task<IActionResult> notFound()
        {
            return View();
        }

        public async Task<IActionResult> singleNews(int id)
        {
            var News = await _context.TblNews.Include(i => i.Category).Include(i => i.Keyword).FirstOrDefaultAsync(i => i.Id == id);
            var mostViewed = await _context.TblNews.Include(i => i.Category).OrderByDescending(i => i.ViewCount).Take(4).ToListAsync();
            var relatedNews = await _context.TblNews.Where(i => i.CategoryId == News.CategoryId).OrderByDescending(i => i.DatePublished).Take(3).ToListAsync();
            var LatestNews = await _context.TblNews.Include(i => i.Category).OrderByDescending(i => i.DatePublished).Take(5).ToListAsync();
            var comments = await _context.TblComment.Where(i => i.NewsId == News.Id).ToListAsync();
            if (News == null) return RedirectToAction("notFound");
            var tags = await _context.TblKeyword.ToListAsync();
            (TblNews news, List<TblNews> relatedNews, List<TblNews> mostviewed, List<TblNews> latestNews, List<TblComment> Comments, List<TblKeyword> tags) result = new (News, relatedNews, mostViewed, LatestNews, comments, tags);
            return View(result);
        }


        public async Task<IActionResult> Archive(int pageNumber = 1, string category = "", string keyword = "")
        {

            var allNews = await _context.TblNews.Include(i => i.Category).Include(i=>i.Keyword).ToListAsync();
            var MostViewed = await _context.TblNews.Include(i => i.Category).OrderByDescending(i => i.ViewCount).Take(5).ToListAsync();
            if (!string.IsNullOrEmpty(category))
            {
                allNews = allNews.Where(i => i.Category.Name == category).ToList();
                ViewData["category"] = category;
            }


            if (!string.IsNullOrEmpty(keyword))
            {
                allNews = allNews.Where(i => i.Keyword.Any(j => j.Name == keyword)).ToList();
                ViewData["Keyword"] = keyword;
            }

            int pageCount = (int)Math.Ceiling((decimal)allNews.Count / 10);
            ViewData["pageCount"] = pageCount;
            ViewData["activePage"] = pageNumber;
            allNews = allNews.Skip((pageNumber - 1) * 12).Take(12).ToList();
            (List<TblNews> News, List<TblNews> MV) result = new(allNews, MostViewed);
            return View(result);
        }

 


    } 
}
    