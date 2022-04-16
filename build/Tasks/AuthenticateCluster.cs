using System;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Authenticate Cluster")]
[IsDependentOn(typeof(AuthenticateServicePrincipal))]
public class AuthenticateCluster : AsyncFrostingTask<HelmBuildContext>
{
  public override bool ShouldRun(HelmBuildContext context)
  {
    return context.BuildEnvironment is not "dev";
  }

  public override async Task RunAsync(HelmBuildContext context)
  {
    var result = await CliWrap.Cli.Wrap("az")
      .WithArguments(new[]
      {
        "aks",
        "get-credentials",
        $"--resource-group {context.Settings.Azure.Cluster.ResourceGroup}",
        $"--name {context.Settings.Azure.Cluster.Name}"
      }, false)
      .ExecuteBufferedAsync();

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}