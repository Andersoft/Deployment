using Cake.Frosting;

[TaskName("Default")]
[IsDependentOn(typeof(LogOutServicePrincipal))]
public class DefaultTask : FrostingTask<HelmBuildContext>
{
}