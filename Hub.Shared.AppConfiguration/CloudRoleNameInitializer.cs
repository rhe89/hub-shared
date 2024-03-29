using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Hub.Shared.AppConfiguration;

public class CloudRoleNameInitializer : ITelemetryInitializer
{
    private readonly string _roleName;

    public CloudRoleNameInitializer(string roleName)
    {
        _roleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
    }

    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.Cloud.RoleName = _roleName;
        telemetry.Context.Cloud.RoleInstance = _roleName;
    }
}