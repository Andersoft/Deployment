using System;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Authenticate Service Principal")]
public sealed class AuthenticateServicePrincipal : AsyncFrostingTask<HelmBuildContext>
{
  public override async Task RunAsync(HelmBuildContext context)
  {
    var result = await CliWrap.Cli.Wrap("az")
      .WithArguments(new[]
      {
        "login",
        "--service-principal",
        $"--username {context.Settings.Azure.ServicePrincipal.Username}",
        $"--password {context.Settings.Azure.ServicePrincipal.Password}",
        $"--tenant {context.Settings.Azure.ServicePrincipal.Tenant}"
      }, false)
      .ExecuteBufferedAsync();

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}