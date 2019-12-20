using GDSHelpers.TestSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace GDSHelpers.TestSite.Controllers
{
    [Route("tags")]
    public class TagHelpersController : Controller
    {
        [Route("{tagView:regex([[a-zA-Z0-9]]*)}")]
        public IActionResult Tag(string tagView)
        {
            return View(tagView);
        }

        public IActionResult TextBoxTest2()
        {
            ChildOfSampleModel vm = new ChildOfSampleModel();
            return View("../TagHelpers/TextBox", vm);
        }
    }
}