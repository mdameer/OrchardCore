using Microsoft.AspNetCore.Http;
using System.Linq;
using OrchardCore.Security.Options;

namespace OrchardCore.Security.Services;

public class ContentSecurityPolicyHeaderPolicyProvider : HeaderPolicyProvider
{
    private string _policy;

    public override void InitializePolicy()
    {
        var policies = Options.ContentSecurityPolicy?.ToList() ?? [];

        if (Options.EnableContentSecurityPolicyReporting &&
            !string.IsNullOrWhiteSpace(Options.ContentSecurityPolicyReportUri))
        {
            policies.Add($"{ContentSecurityPolicyValue.ReportUri} {Options.ContentSecurityPolicyReportUri}");
        }

        if (policies.Count > 0)
        {
            _policy = string.Join(SecurityHeaderDefaults.PoliciesSeparator, policies);
        }
    }

    public override void ApplyPolicy(HttpContext httpContext)
    {
        if (_policy != null)
        {
            httpContext.Response.Headers[SecurityHeaderNames.ContentSecurityPolicy] = _policy;
        }
    }
}
