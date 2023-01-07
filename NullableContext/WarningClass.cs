namespace NullableContext
{
#nullable enable warnings
  public class WarningClass
  {
    public string MyData { get; set; }

    public void DoSomeThing()
    {
      int hashCode = MyData.GetHashCode(); // No warning due to the principle of not null consideration for members inside method 

      string otherData = null;
      int otherHashCode = otherData.GetHashCode(); /// Generate CS8602 warning 
                                                   /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>
    }
  }
#nullable restore
}
