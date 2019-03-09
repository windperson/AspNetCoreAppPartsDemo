using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PartClassLib.ViewComponents
{
    public class VersionHelperViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            return View(version);
        }
    }
}