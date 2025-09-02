using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OrchardCore.Security.Controllers;

[ApiController]
[Route(ReportPath)]
[IgnoreAntiforgeryToken, AllowAnonymous]
public class ContentSecurityPolicyReportController : ControllerBase
{
    public const string ReportPath = "csp-report";

    private readonly ILogger _logger;

    public ContentSecurityPolicyReportController(ILogger<ContentSecurityPolicyReportController> logger)
        => _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Report()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        _logger.LogWarning("CSP report received: {Report}", body);
        return Ok();
    }
}

