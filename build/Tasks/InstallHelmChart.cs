using System.IO;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Install Helm Module")]
[IsDependentOn(typeof(PreviewHelmChart))]
public class InstallHelmChart : AsyncFrostingTask<HelmBuildContext>
{
  public override bool ShouldRun(HelmBuildContext context)
  {
    return !context.Preview;
  }

  public override async Task RunAsync(HelmBuildContext context)
  {
    BufferedCommandResult result = await ScriptRunner.RunAsync("helm", $"upgrade --create-namespace --namespace {context.Arguments.GetArgument(WellKnownVariables.NameSpace)} -i {context.ProjectName.ToLower()} ./dist --values ./dist/values.yaml");
    await File.WriteAllTextAsync($"./artifacts/{context.ProjectName.ToLower()}.yml", result.StandardOutput);
    context.Log.Write(Verbosity.Normal, LogLevel.Information, "Manifest written to ./artifacts");
  }
}