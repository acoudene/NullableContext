namespace NullableContext
{


  public class NullableContextTest
  {

#nullable enable
    [Fact]
    public void EnableContextTest_Warning_On_Nullable_Assignment()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>
                                              
      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void EnableContextTest_Warning_On_Nullable_Assignment_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>

      myData = myClass.MyNullableData!; // No warning due to ! operator but you have to be sure about given nullable reference
    }

    [Fact]
    public void EnableContextTest_Warning_On_Null_Dereference_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); /// Generate CS8602 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>

      myClass.MyNullableData!.GetHashCode(); // No warning due to ! operator but you have to be sure about given nullable dereference
    }
#nullable restore

#nullable disable
    [Fact]
    public void DisableContextTest_Warning_On_Nullable_Dereference()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void DisableContextTest_Warning_On_Nullable_Dereference_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning
      myData = myClass.MyNullableData!; // No warning, ! operator has no effect
    }

    [Fact]
    public void DisableContextTest_Warning_On_Null_Dereference_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); // No warning
      myClass.MyNullableData!.GetHashCode(); // No warning, ! operator has no effect
    }
#nullable restore
  }
}