using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using DI_Service_Lifetime.Models;
using DI_Service_Lifetime.Services;

namespace DI_Service_Lifetime.Controllers;

public class HomeController : Controller
{
    private readonly IScopedGuidService _scopedGuidService1;
    private readonly IScopedGuidService _scopedGuidService2;

    private readonly ISingletonGuidService _singletonGuidService1;
    private readonly ISingletonGuidService _singletonGuidService2;

    private readonly ITransientGuidService _transientGuidService1;
    private readonly ITransientGuidService _transientGuidService2;

    public HomeController(
        IScopedGuidService scopedGuidService1,
        IScopedGuidService scopedGuidService2,
        ISingletonGuidService singletonGuidService1,
        ISingletonGuidService singletonGuidService2,
        ITransientGuidService transientGuidService1,
        ITransientGuidService transientGuidService2
    )
    {
        _scopedGuidService1 = scopedGuidService1;
        _scopedGuidService2 = scopedGuidService2;
        
        _singletonGuidService1 = singletonGuidService1;
        _singletonGuidService2 = singletonGuidService2;
        
        _transientGuidService1 = transientGuidService1;
        _transientGuidService2 = transientGuidService2;
    }

    public IActionResult Index()
    {
        StringBuilder message = new StringBuilder();
        message.Append($"transient 1 : { _transientGuidService1.GetGuid() } \n");
        message.Append($"transient 2 : { _transientGuidService2.GetGuid() } \n\n\n");
        
        message.Append($"singleton 1 : { _singletonGuidService1.GetGuid() } \n");
        message.Append($"singleton 2 : { _singletonGuidService2.GetGuid() } \n\n\n");
        
        message.Append($"scoped 1 : { _scopedGuidService1.GetGuid() } \n");
        message.Append($"scoped 2 : { _scopedGuidService2.GetGuid() } \n\n\n");
        
        return Ok(message.ToString());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}