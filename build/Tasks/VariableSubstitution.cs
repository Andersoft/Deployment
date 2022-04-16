using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;
using Cake.Frosting;

[TaskName("Variable Substitution")]
[IsDependentOn(typeof(ConfigureDirectories))]
public class VariableSubstitution : AsyncFrostingTask<HelmBuildContext>
{
  public override async Task RunAsync(HelmBuildContext context)
  {
    foreach (var file in Directory.EnumerateFiles("./helm", "*", SearchOption.AllDirectories))
    {
      var substitutionToken = context.Arguments.GetArguments().ToDictionary(x => x.Key, x => x.Value.First());

      var text = await File.ReadAllTextAsync(file);
      foreach (var token in substitutionToken)
      {
        context.Log.Information("Transforming {0} to {1} in file: {2}", token.Key, token.Value, file);
        text = text.Replace($"$({token.Key})", token.Key == "projectName" ? token.Value.ToLower() : token.Value);
      }
      var newPath = Path.Combine("dist", Path.GetRelativePath("./helm", file));
      

      if (!string.IsNullOrWhiteSpace(newPath) &&!Directory.Exists(Path.GetDirectoryName(newPath)))
      {
        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
      }

      await File.WriteAllTextAsync(newPath, text);
    }
  }
}