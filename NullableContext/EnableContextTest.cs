namespace NullableContext
{


  public class NullableContextTest
  {

#nullable enable
    [Fact]
    public void EnableContextTest_On_Nullable_Assignment()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>
                                              
      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    } 

    [Fact]
    public void EnableContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>

      myData = myClass.MyNullableData!; // No warning due to ! operator but you have to be sure about given nullable reference
    }

    [Fact]
    public void EnableContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); /// Generate CS8602 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>

      myClass.MyNullableData!.GetHashCode(); // No warning due to ! operator but you have to be sure about given nullable dereference
    }
#nullable restore

#nullable enable annotations
    [Fact]
    public void AnnotationsContextTest_On_Nullable_Assignment()
    {
      MyClass? myClass = new MyClass(); // No warning, ? is allowed

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void AnnotationsContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning
      myData = myClass.MyNullableData!; // No warning, ! operator has no effect
    }

    [Fact]
    public void AnnotationsContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); // No warning
      myClass.MyNullableData!.GetHashCode(); // No warning, ! operator has no effect
    }
#nullable restore

#nullable disable
    [Fact]
    public void DisableContextTest_On_Nullable_Assignment()
    {
      MyClass? myClass = new MyClass(); // Generate CS8632 warning on ? operator
      // CS8632 not documented but the text: The annotation for nullable reference types should only be used in code within a '#nullable' context.

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void DisableContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning
      myData = myClass.MyNullableData!; // No warning, ! operator has no effect
    }

    [Fact]
    public void DisableContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); // No warning
      myClass.MyNullableData!.GetHashCode(); // No warning, ! operator has no effect
    }
#nullable restore


#nullable enable warnings
    [Fact]
    public void WarningContextTest_On_Nullable_Assignment()
    {
      MyClass? myClass = new MyClass(); // Generate CS8632 warning on ? operator
      // CS8632 not documented but the text: The annotation for nullable reference types should only be used in code within a '#nullable' context.

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning on nullable Assignmment

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void WarningContextTest_Warning_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning on nullable Assignmment
      myData = myClass.MyNullableData!; // No warning due to ! operator but you have to be sure about given nullable dereference
    }

    [Fact]
    public void WarningContextTest_Warning_On_Null_Dereference_And_Forgiving_Operator()
    {
      MyClass myClass = new MyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); /// Generate CS8602 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>
      
      myClass.MyNullableData!.GetHashCode(); // No warning due to ! operator but you have to be sure about given nullable dereference
    }
#nullable restore
  }
}