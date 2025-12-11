using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNews.Models;

namespace MyNews.ViewComponents
{
    public class MenuBar : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MyNewsContext newsContext = new MyNewsContext();
            var Categories = await newsContext.TblCategory.Include(i => i.TblNews).ToListAsync();
            return View(Categories);
        }

    }

    public class HeadNews : ViewComponent
        {
            public async Task<IViewComponentResult> InvokeAsync()
            {
                MyNewsContext newsContext = new MyNewsContext();
                var headNews = await newsContext.TblNews.Include(i => i.Category).FirstOrDefaultAsync(i => i.ViewCount > 10);
                return View(headNews);
            }
        }
    }

public class NewsBlock : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(TblNews news)
    {
        return View(news);
    }
}

public class ShortNewsBlock : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MyNewsContext newsContext = new MyNewsContext();
        var PoliticNews = await newsContext.TblNews.Where(i => i.CategoryId == 1).OrderByDescending(i => i.DatePublished).Take(4).ToListAsync();
        var economyNews = await newsContext.TblNews.Where(i => i.CategoryId == 5).OrderByDescending(i => i.DatePublished).Take(4).ToListAsync();
        (List<TblNews> Politic, List<TblNews> Economy) result = new(PoliticNews, economyNews);
        return View(result);
    }
}

public class TagBlock : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MyNewsContext newsContext = new MyNewsContext();
        var tags = await newsContext.TblKeyword.ToListAsync();
        return View(tags);
    }   
}

/*public class comment : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

    }

}
*/