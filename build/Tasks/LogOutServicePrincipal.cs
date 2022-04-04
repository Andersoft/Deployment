using System;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Logout Service Principal")]
[IsDependentOn(typeof(InstallHelmChart))]
public class LogOutServicePrincipal : AsyncFrostingTask<HelmBuildContext>
{
  public override async Task RunAsync(HelmBuildContext context)
  {
    var arguments = new[]
    {
      "logout",
      $"--username {context.Settings.Azure.ServicePrincipal.Username}"
    };

    BufferedCommandResult result = await ScriptRunner.RunAsync("az", arguments);

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}