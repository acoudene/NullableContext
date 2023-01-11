namespace NullableContext
{


  public class NullableContextTest
  {

#nullable enable
    [Fact]
    public void EnableContextTest_On_Nullable_Assignment()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
                                              /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>

      string nullCoData = myClass.MyNullableData ?? "DefaultValue";
      nullCoData = myClass.MyData ?? "DefaultValue";

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    } 

    [Fact]
    public void EnableContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; /// Generate CS8600 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8600)#possible-null-assigned-to-a-nonnullable-reference"/>

      myData = myClass.MyNullableData!; // No warning due to ! operator but you have to be sure about given nullable reference
    }

    [Fact]
    public void EnableContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); /// Generate CS8602 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>

      myClass.MyNullableData!.GetHashCode(); // No warning due to ! operator but you have to be sure about given nullable dereference
    }

    [Fact]
    public void EnableContextTest_Call_Old_Or_Disable_Class()
    {
      DisableMyClass myClass = new DisableMyClass();
      int hashCode = myClass.MyData.GetHashCode(); // No warning in migration for example :(
      
      // Should be still corrected by this :(
      if (myClass.MyData != null)
        hashCode = myClass.MyData.GetHashCode();
    }
#nullable restore

#nullable enable annotations
    [Fact]
    public void AnnotationsContextTest_On_Nullable_Assignment()
    {
      EnableMyClass? myClass = new EnableMyClass(); // No warning, ? is allowed

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void AnnotationsContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning
      myData = myClass.MyNullableData!; // No warning, ! operator has no effect
    }

    [Fact]
    public void AnnotationsContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); // No warning
      myClass.MyNullableData!.GetHashCode(); // No warning, ! operator has no effect
    }
#nullable restore

#nullable disable
    [Fact]
    public void DisableContextTest_On_Nullable_Assignment()
    {
      EnableMyClass? myClass = new EnableMyClass(); // Generate CS8632 warning on ? operator
      // CS8632 not documented but the text: The annotation for nullable reference types should only be used in code within a '#nullable' context.

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void DisableContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning
      myData = myClass.MyNullableData!; // No warning, ! operator has no effect
    }

    [Fact]
    public void DisableContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      EnableMyClass myClass = new EnableMyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); // No warning
      myClass.MyNullableData!.GetHashCode(); // No warning, ! operator has no effect
    }
#nullable restore


#nullable enable warnings
    [Fact]
    public void WarningContextTest_On_Nullable_Assignment()
    {
      WarningMyClass? myClass = new WarningMyClass(); // Generate CS8632 warning on ? operator
      // CS8632 not documented but the text: The annotation for nullable reference types should only be used in code within a '#nullable' context.

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning on nullable Assignmment

      myClass.MyNullableData = "NotNull";

      myData = myClass.MyNullableData; // No warning
    }

    [Fact]
    public void WarningContextTest_On_Nullable_Assignment_And_Forgiving_Operator()
    {
      WarningMyClass myClass = new WarningMyClass();

      // MyNullableData is declared as string?
      string myData = myClass.MyNullableData; // No warning on nullable Assignmment
      myData = myClass.MyNullableData!; // No warning due to ! operator but you have to be sure about given nullable dereference
    }

    [Fact]
    public void WarningContextTest_On_Null_Dereference_And_Forgiving_Operator()
    {
      WarningMyClass myClass = new WarningMyClass();

      // MyNullableData is declared as string?
      myClass.MyNullableData.GetHashCode(); /// Generate CS8602 warning 
      /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8602)#possible-dereference-of-null"/>
      
      myClass.MyNullableData!.GetHashCode(); // No warning due to ! operator but you have to be sure about given nullable dereference
    }


    [Fact]
    public void WarningContextTest_On_Method_Call()
    {
      WarningMyClass myClass = new WarningMyClass();
      string data = null; // No warning 
      myClass.DoSomeThing(data);
    }
#nullable restore
  }
}