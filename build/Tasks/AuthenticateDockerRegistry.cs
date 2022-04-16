using System;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Authenticate Docker Registry")]
[IsDependentOn(typeof(AuthenticateServicePrincipal))]
public class AuthenticateDockerRegistry : AsyncFrostingTask<HelmBuildContext>
{
  public override async Task RunAsync(HelmBuildContext context)
  {

    var result = await CliWrap.Cli.Wrap("kubectl")
      .WithArguments(new[]
      {
        $"create secret docker-registry acr-secret",
        $"--namespace {context.Arguments.GetArgument(WellKnownVariables.NameSpace)}",
        $"--docker-server={context.Arguments.GetArgument(WellKnownVariables.Registry)}.azurecr.io",
        $"--docker-username={context.Settings.Azure.ServicePrincipal.Username}",
        $"--docker-password={context.Settings.Azure.ServicePrincipal.Password}"
      }, false)
      .ExecuteBufferedAsync();

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}