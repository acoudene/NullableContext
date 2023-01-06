using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableContext
{
  public class MyClass
  {    
    public string MyData { get; set; }

#nullable enable
    public string? MyNullableData { get; set; }
#nullable restore

    public MyClass()
    {
      MyData = "NotNull";
    }
  }
}
