using System.Threading.Tasks;
using CliWrap.Buffered;

public static class ScriptRunner
{
  public static Task<BufferedCommandResult> RunAsync(string tool, params string[] arguments)
  {
    return CliWrap.Cli.Wrap(tool)
      .WithArguments(arguments, false)
      .ExecuteBufferedAsync();
  }
}