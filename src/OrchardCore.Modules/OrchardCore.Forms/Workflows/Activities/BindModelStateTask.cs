using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.Forms.Workflows.Activities;

public class BindModelStateTask : TaskActivity<BindModelStateTask>
{
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IStringLocalizer S;

    public BindModelStateTask(
        IHttpContextAccessor httpContextAccessor,
        IUpdateModelAccessor updateModelAccessor,
        IStringLocalizer<BindModelStateTask> localizer
    )
    {
        _updateModelAccessor = updateModelAccessor;
        _httpContextAccessor = httpContextAccessor;
        S = localizer;
    }

    public override LocalizedString DisplayText => S["Bind Model State Task"];

    public override LocalizedString Category => S["Validation"];

    public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
    {
        return Outcomes(S["Done"]);
    }

    public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
    {
        var updater = _updateModelAccessor.ModelUpdater
            ?? throw new InvalidOperationException("Cannot add model validation errors when there's no Updater present.");

        var httpContext = _httpContextAccessor.HttpContext;

        foreach (var item in httpContext.Request.Form)
        {
            // Avoid creating a new array for rawValue if there's only one value.
            object rawValue;
            if (item.Value.Count == 0)
            {
                rawValue = null;
            }
            else if (item.Value.Count == 1)
            {
                rawValue = item.Value[0];
            }
            else
            {
                rawValue = item.Value.ToArray();
            }

            updater.ModelState.SetModelValue(item.Key, rawValue, item.Value.ToString());
        }

        return Outcomes("Done");
    }
}
