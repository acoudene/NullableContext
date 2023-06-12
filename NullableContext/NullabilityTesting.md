# NullabilityTesting

Just a quick tutorial to explain how to correctly test a null reference

_in french_

# Objectifs

Montrer par des tests cod�s les �cueils � utiliser les op�rateurs != ou == pour tester une r�f�rence nulle.
Montrer les bonnes pratiques, � savoir utiliser les op�rateurs `is` et `is not`, afin de tester la valeur nulle mais aussi d'autres crit�res via le pattern matching.

# Pr�requis

Pour illustrer, la probl�matique nous utilisons la d�finition de classe ci-apr�s.
J'ai volontairement surcharg� les op�rateurs afin de forcer `false` � chaque fois.

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

Il ne faut plus tester avec l'op�rateur car sinon avec la d�finition de classe pr�c�dente, on n'aura pas le r�sultat voulu, � savoir tester des r�f�rences :

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

# Ce qu'on peut faire mais sans �tre la meilleure m�thode

Si on a une version de C# < 9 alors on peut utiliser la fonction ReferenceEquals qui va utiliser la d�finition en Object des param�tres garantissant qu'aucun op�rateur n'a �t� surcharg�.
Sinon, on peut utiliser du pattern matching en testant une instance de comparaison n'ayant pas de crit�res `{ }` mais permettant de v�rifier qu'on veut une instance non nulle.

```
[Fact]
public void BetterWayOfTestingNullButNotGoodOnes()
{
  OverridedOperatorsClass overridedOperatorsClass = nulmais sans �tre la meilleure m�thode

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

  // Another betmais sans �tre la meilleure m�thode
  
  PndOperatorsClass is not { })
  {
    // Do something
  }
  else
  {
    // Not expected, instance is null
    Assert.Fail("Not expected");
  }

  // Instanciate
  overridedOperatorsClass = new OverridedOperatorsClass(mais sans �tre la meilleure m�thode

  Pn

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

Pour tester la nullabilit� d'une r�f�rence il faut utiliser l'op�rateur `is` ou `is not` depuis C# 9 (en version inf�rieure on utilisera `ReferenceEquals` comme vu pr�c�demment)

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

Il est important de souligner que le pattern matching peut permettre d'avoir une meilleure ma�trise et une meilleure clart� de ce qu'on veut tester.
Il est int�ressant d'utiliser ce type d'�criture : `is { <Property>: <ExpectedValue> }` ou `is not { <Property>: <ExpectedValue> }`.
Voir la documentation Microsoft pour plus de d�tail.

```
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
```