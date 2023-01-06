using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableContext
{
#nullable enable
  public class MyClass
  {    
    public string MyData { get; set; }


    public string? MyNullableData { get; set; }

    public MyClass() // Generate a CS8618 warning due to not initialized MyData
    /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8618)#nonnullable-reference-not-initialized"/>
    {
      // To correct warning uncomment this
      // MyData = "NotNull";
    }
  }  
#nullable restore

#nullable enable warnings
  public class Warningclass
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
