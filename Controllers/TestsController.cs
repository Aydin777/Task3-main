using Microsoft.AspNetCore.Mvc;
using Task3.Services;

namespace Task3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestsController : ControllerBase
{
    private readonly ICalculateService _calculateService1;
    private readonly ICalculateService _calculateService2;

    public TestsController(ICalculateService calculateService1, ICalculateService calculateService2)
    {
        _calculateService1 = calculateService1;
        _calculateService2 = calculateService2;
    }

    [HttpGet]
    public IActionResult Get()
        => Ok(_calculateService1.Calculate(10, 20) + _calculateService2.Calculate(10, 20));
}
