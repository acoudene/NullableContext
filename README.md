# NullableContext

Just a quick tutorial to explain Nullable Context.

Note: to see other best practices specifically on nullability testing for references, see my nested tests and my other documentation here: [NullableContext/NullabilityTesting.md](NullableContext/NullabilityTesting.md)

_in french_

# Objectifs

Expliquer les changements apportés depuis C# 8 sur les contextes Nullable (Nullable Context).
Identifier quelques cas d'utilisation fréquents en routine ou en migration.

# Principes

Avant C# 8 et les nullable contexts, toute référence C# était naturellement nullable (nullable signifiant pouvant recevoir la valeur null).

Depuis C# 8, ce fonctionnement peut changer à la demande selon 4 modes possibles :

- **Enable/Activé** : tout est non nullable
  - Aucune référence n'est nullable sauf si on la déclare avec l'opérateur `?`
  - Le compilateur va détecter en WARNINGS les assignements ou les déréférencements potentiellement à null dans le code pour correction.
  - On pourra utiliser l'opérateur `!` (forgiving operator) devant une référence que l'on certifie ne pas recevoir de valeur à null alors qu'un warning est émis (permet d'éviter de faux warnings).

- **Annotations** : tout est non nullable, idem Enable/Activé sauf 
  - Pas de warnings de compilation, pas d'analyse de code sur ce point par le compilateur
  - L'usage de l'opérateur `!` n'a aucun effet.

- **Disable/Desactivé** : tout est nullable
  - Comme avant C# 8, on ne change rien, tout est nullable.
  - L'usage de l'opérateur `?` génère un warning.
  - L'usage de l'opérateur `!` n'a aucun effet.

- **Warning/Avertissement** : tout est nullable, idem Disable/Désactivé sauf
  - Un warning est généré seulement lorsqu'un déréférencement peut avoir potentiellement une valeur null.
  - Tout ajout de code peut ensuite générer un warning si risque d'utilisation d'une référence nulle
  - L'usage de l'opérateur `!` est opérationnel. 
  - Les références peuvent être nulles mais les membres d'une classe sont non nullables dans toutes les méthodes de la classe sauf si l'opérateur `?`est appliqué.

# Domaine d'application

## Au niveau Projet 

Ces modes sont applicables au niveau du projet :

![image.png](/Images/01_Item.png)

## Au niveau des classes

On peut également appliquer ces modes de manière chirurgicale dans le code.

```
#nullable enable // Sets the nullable annotation context and nullable warning context to enable.
#nullable disable // Sets the nullable annotation context and nullable warning context to disable.
#nullable restore // Restores the nullable annotation context and nullable warning context to the project settings.
#nullable disable warnings // Set the nullable warning context to disable.
#nullable enable warnings // Set the nullable warning context to enable.
#nullable restore warnings // Restores the nullable warning context to the project settings.
#nullable disable annotations // Set the nullable annotation context to disable.
#nullable enable annotations // Set the nullable annotation context to enable.
#nullable restore annotations // Restores the annotation warning context to the project settings.
```

# Exemples

J'ai mis un Code source disponible ici : https://github.com/acoudene/NullableContext
afin d'illustrer tous les cas génériques générés par l'utilisation des différents modes avec quelques combinatoires de modes.

## Précision générale 

Les opérateurs fonctionnent toujours de la même façon quel que soit le mode choisi. Par exemple, l'opérateur null-coalescence ?? a un comportement inchangé quel que soit le mode. Car OUI, **une référence non nullable peut toujours recevoir la valeur null** suivant les scénarii ou les dépendances choisies.

![image.png](/Images/02_Item.png)

### Exemple de gestion de la nullabilité avec affectation de valeur par défaut :

![image.png](/Images/03_Item.png)

## Nullable Context: Enable

### Appel dans des blocks

![image.png](/Images/04_Item.png)

### Initialisation de classe :

![image.png](/Images/05_Item.png)

## Nullable Context: Annotations

### Appel dans des blocks

![image.png](/Images/06_Item.png)

## Nullable Context: Disable

### Appel dans des blocks

![image.png](/Images/07_Item.png)

## Nullable Context: Warning

### Appel dans des blocks

![image.png](/Images/08_Item.png)

### Initialisation de classe :

![image.png](/Images/09_Item.png)

# Migration 

Si on décide d'activer le contexte de nullabilité dans une nouvelle assembly qui utilise une classe avec le contexte désactivé ou provenant d'une assembly ayant le contexte désactivé, alors aucun warning ne sortira, il faudra bien continuer à tester la nullabilité...

En effet, ci-dessous la classe **DisableClass** possède une propriété MyData déclarée en string donc vue comme non nullable pour la classe appelante au contexte nullable activé alors qu'elle peut avoir la valeur nulle sans warning.

![image.png](/Images/10_Item.png)

# Conclusion et conseils

La gestion des nullables est une belle amélioration en terme de qualité de code. Par contre, il est toujours possible d'introduire des références nulles si on ne prête pas attention aux warnings en mode Enable/Warning ou si les warnings sont désactivés, ou encore, plus problématique, si on récupère une classe avec un contexte désactivé (ancienne classe par exemple).
**Mes conseils :** 

Il est important de prendre conscience qu'une référence déclarée comme non nullable peut donc recevoir la valeur nulle sans erreur à l'exécution.

Partant de ce principe, il ne faut pas changer ses habitudes et :
- continuer à corriger tous les warnings dont les nouveaux générés par le Nullable Context choisi.
- continuer à tester la nullabilité des références malgré tout, pour gérer tous les cas particuliers comme des warnings non pris en compte dans une classe non maîtrisée (assembly legacy) ou encore en phase de migration, etc...
- privilégier l'usage des helpers Microsoft comme Guard pour tester des préconditions incluant la nullability : https://learn.microsoft.com/en-us/windows/communitytoolkit/developer-tools/guard
- migrer progressivement tout le code du mode Disable vers Enable en suivant les étapes dans l'ordre : Disable, Warning, Annotations, et enfin Enable.

# Annexe - passage de warning à error sur les cas de nullabilité 

Pour forcer cette correction de warning et améliorer la qualité du code, on pourrait imaginer de transformer les warnings sur la nullabilité en erreur en changeant les settings du projet comme ceci :

![image.png](/Images/11_Item.png)

Ou en version texte : CS8600;CS8601;CS8602;CS8603;CS8604;CS8613;CS8614;CS8618;CS8619;CS8620;CS8622;CS8625;CS8629;CS8632;CS8633;CS8767


# Références : 
- https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-contexts
- https://learn.micro1oft.com/en-us/dotnet/csharp/nullable-migration-strategies

