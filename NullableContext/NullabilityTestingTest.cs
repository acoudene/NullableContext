namespace NullableContext;

public class NullabilityTestingTest
{
  class OverridedOperatorsClass
  {
    public OverridedOperatorsClass Child { get; set; }

    public static bool operator ==(OverridedOperatorsClass left, OverridedOperatorsClass right)
    {
      return false;
    }

    public static bool operator !=(OverridedOperatorsClass left, OverridedOperatorsClass right)
    {
      return false;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (ReferenceEquals(obj, null))
      {
        return false;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }

  [Fact]
  public void BadWayOfTestingNull()
  {
    OverridedOperatorsClass overridedOperatorsClass = null;

    // Bad way of testing, here return false...
    if (overridedOperatorsClass == null)
    {
      // Not expected: should not go there...
      Assert.Fail("Not expected due to equality operators overrided");
    }

    // Instanciate 
    overridedOperatorsClass = new OverridedOperatorsClass();

    // Other bad way of testing
    if (overridedOperatorsClass != null)
    {
      // Not expected: should not go there...
      Assert.Fail("Not expected due to equality operators overrided");
    }
  }

  [Fact]
  public void BetterWayOfTestingNullButNotGoodOnes()
  {
    OverridedOperatorsClass overridedOperatorsClass = null;

    // A better way of testing but not the best one
    if (ReferenceEquals(overridedOperatorsClass, null))
    {
      // Do something
    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // Another better way of testing using pattern matching, { } means instance exists
    if (overridedOperatorsClass is not { })
    {
      // Do something
    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // Instanciate
    overridedOperatorsClass = new OverridedOperatorsClass();

    // A better way of testing but not the best one
    if (!ReferenceEquals(overridedOperatorsClass, null))
    {
      // Do something
    }
    else
    {
      // Not expected, instance is not null
      Assert.Fail("Not expected");
    }

    // Another better way of testing using pattern matching
    if (overridedOperatorsClass is { })
    {
      // Do something
    }
    else
    {
      // Not expected, instance is not null
      Assert.Fail("Not expected");
    }
  }

  [Fact]
  public void BestWayOfTestingNull()
  {
    OverridedOperatorsClass overridedOperatorsClass = null;

    // The best way of testing
    if (overridedOperatorsClass is null)
    {
      // Do something
    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // Instanciate
    overridedOperatorsClass = new OverridedOperatorsClass();

    // The best way of testing
    if (overridedOperatorsClass is not null)
    {
      // Do something
    }
    else
    {
      // Not expected, instance is not null
      Assert.Fail("Not expected");
    }
  }

  [Fact]
  public void SpecificNullabilityTestingUseCases()
  {
    OverridedOperatorsClass overridedOperatorsClass = new OverridedOperatorsClass();

    // A good way of testing but not the most secured one
    if (overridedOperatorsClass?.Child is null)
    { 
    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // The best of way of testing using pattern matching
    if (overridedOperatorsClass is { Child : null })
    {

    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // Instanciate Child
    overridedOperatorsClass.Child = new OverridedOperatorsClass();

    // A good way of testing but not the most secured one
    if (overridedOperatorsClass?.Child is not null)
    {
    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }

    // The best of way of testing using pattern matching
    if (overridedOperatorsClass is { Child: not null })
    {

    }
    else
    {
      // Not expected, instance is null
      Assert.Fail("Not expected");
    }
  }
}