# NullabilityTesting

Just a quick tutorial to explain how to correctly test a null reference

_in french_

# Objectifs

Montrer par des tests codés les écueils à utiliser les opérateurs != ou == pour tester une référence nulle.
Montrer les bonnes pratiques, à savoir utiliser les opérateurs `is` et `is not`, afin de tester la valeur nulle mais aussi d'autres critères via le pattern matching.

# Prérequis

Pour illustrer, la problématique nous utilisons la définition de classe ci-après.
J'ai volontairement surchargé les opérateurs afin de forcer `false` à chaque fois.

```
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
  [...]
}
```

# Ce qu'il ne faut plus faire

Il ne faut plus tester avec l'opérateur car sinon avec la définition de classe précédente, on n'aura pas le résultat voulu, à savoir tester des références :

```
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
```

# Ce qu'on peut faire mais sans être la meilleure méthode

Si on a une version de C# < 9 alors on peut utiliser la fonction ReferenceEquals qui va utiliser la définition en Object des paramètres garantissant qu'aucun opérateur n'a été surchargé.
Sinon, on peut utiliser du pattern matching en testant une instance de comparaison n'ayant pas de critères `{ }` mais permettant de vérifier qu'on veut une instance non nulle.

```
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
```

# Conclusion ou ce qu'il faut faire

Pour tester la nullabilité d'une référence il faut utiliser l'opérateur `is` ou `is not` depuis C# 9 (en version inférieure on utilisera `ReferenceEquals` comme vu précédemment)

```
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
```

# Pour aller, plus loin, le pattern matching

Il est important de souligner que le pattern matching peut permettre d'avoir une meilleure maîtrise et une meilleure clarté de ce qu'on veut tester.
Il est intéressant d'utiliser ce type d'écriture : `is { <Property>: <ExpectedValue> }` ou `is not { <Property>: <ExpectedValue> }`.
Voir la documentation Microsoft pour plus de détail.

```
[Fact]
public void SpecificNullabilityTestingUseCases()
{
  OverridedOperatorsClass overridedOperatorsClass = new OverridedOperatorsClass();

  // A good way of testing but not the most secured one
  if (overridedOperatorsClass?.Child is null)
  {
    // Do something
  }
  else
  {
    // Not expected, child instance is null
    Assert.Fail("Not expected");
  }

  // The best of way of testing using pattern matching
  if (overridedOperatorsClass is { Child : null })
  {
    // Do something
  }
  else
  {
    // Not expected, child instance is null
    Assert.Fail("Not expected");
  }

  // Instanciate Child
  overridedOperatorsClass.Child = new OverridedOperatorsClass();

  // A good way of testing but not the most secured one
  if (overridedOperatorsClass?.Child is not null)
  {
    // Do something
  }
  else
  {
    // Not expected, child instance is not null
    Assert.Fail("Not expected");
  }

  // The best of way of testing using pattern matching
  if (overridedOperatorsClass is { Child: not null })
  {
    // Do something
  }
  else
  {
    // Not expected, child instance is not null
    Assert.Fail("Not expected");
  }
}
```
