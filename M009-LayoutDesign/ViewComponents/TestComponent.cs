using Microsoft.AspNetCore.Mvc;

namespace M009_LayoutDesign.ViewComponents;

public class TestComponent : ViewComponent
{
	public IViewComponentResult Invoke()
	{
		return View();
	}
}
