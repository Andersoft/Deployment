using System.Text;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using CliWrap;

public static class Program
{
  public static int Main(string[] args)
  {
    return new CakeHost()
      .UseContext<HelmBuildContext>()
        .Run(args);
  }
}