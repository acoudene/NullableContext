namespace NullableContext
{
#nullable enable
  public class EnableMyClass
  {
    public string MyData { get; set; }


    public string? MyNullableData { get; set; }

    public EnableMyClass() // Generate a CS8618 warning due to not initialized MyData
    /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8618)#nonnullable-reference-not-initialized"/>
    {
      // To correct warning uncomment this
      // MyData = "NotNull";
    }
  }
#nullable restore

}
