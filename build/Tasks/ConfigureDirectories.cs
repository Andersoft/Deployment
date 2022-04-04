using System.IO;
using Cake.Core;
using Cake.Frosting;

[TaskName("Configure Directories")]
[IsDependentOn(typeof(AuthenticateCluster))]
public class ConfigureDirectories : FrostingTask<HelmBuildContext>
{
  public override void Run(HelmBuildContext context)
  {
    if (Directory.Exists("./artifacts"))
    {
      Directory.Delete("./artifacts", true);
    }

    Directory.CreateDirectory("./artifacts");
  }
}