# NullableContext

Just a quick tutorial to explain Nullable Context

_in french_

# Objectifs

Expliquer les changements apport�s depuis C# 8 sur les contextes Nullable (Nullable Context).
Identifier quelques cas d'utilisation fr�quents en routine ou en migration.

# Principes

Avant C# 8 et les nullable contexts, toute r�f�rence C# �tait naturellement nullable (nullable signifiant pouvant recevoir la valeur null).

Depuis C# 8, ce fonctionnement peut changer � la demande selon 4 modes possibles :

- **Enable/Activ�** : tout est non nullable
  - Aucune r�f�rence n'est nullable sauf si on la d�clare avec l'op�rateur `?`
  - Le compilateur va d�tecter en WARNINGS les assignements ou les d�r�f�rencements potentiellement � null dans le code pour correction.
  - On pourra utiliser l'op�rateur `!` (forgiving operator) devant une r�f�rence que l'on certifie ne pas recevoir de valeur � null alors qu'un warning est �mis (permet d'�viter de faux warnings).

- **Annotations** : tout est non nullable, idem Enable/Activ� sauf 
  - Pas de warnings de compilation, pas d'analyse de code sur ce point par le compilateur
  - L'usage de l'op�rateur `!` n'a aucun effet.

- **Disable/Desactiv�** : tout est nullable
  - Comme avant C# 8, on ne change rien, tout est nullable.
  - L'usage de l'op�rateur `?` g�n�re un warning.
  - L'usage de l'op�rateur `!` n'a aucun effet.

- **Warning/Avertissement** : tout est nullable, idem Disable/D�sactiv� sauf
  - Un warning est g�n�r� seulement lorsqu'un d�r�f�rencement peut avoir potentiellement une valeur null.
  - Tout ajout de code peut ensuite g�n�rer un warning si risque d'utilisation d'une r�f�rence nulle
  - L'usage de l'op�rateur `!` est op�rationnel. 
  - Les r�f�rences peuvent �tre nulles mais les membres d'une classe sont non nullables dans toutes les m�thodes de la classe sauf si l'op�rateur `?`est appliqu�.

# Domaine d'application

## Au niveau Projet 

Ces modes sont applicables au niveau du projet :

![image.png](/Images/01_Item.png)

## Au niveau des classes

On peut �galement appliquer ces modes de mani�re chirurgicale dans le code.

```
#nullable enable: Sets the nullable annotation context and nullable warning context to enable.
#nullable disable: Sets the nullable annotation context and nullable warning context to disable.
#nullable restore: Restores the nullable annotation context and nullable warning context to the project settings.
#nullable disable warnings: Set the nullable warning context to disable.
#nullable enable warnings: Set the nullable warning context to enable.
#nullable restore warnings: Restores the nullable warning context to the project settings.
#nullable disable annotations: Set the nullable annotation context to disable.
#nullable enable annotations: Set the nullable annotation context to enable.
#nullable restore annotations: Restores the annotation warning context to the project settings.
```

# Exemples

J'ai mis un Code source disponible ici : https://github.com/acoudene/NullableContext
afin d'illustrer tous les cas g�n�riques g�n�r�s par l'utilisation des diff�rents modes avec quelques combinatoires de modes.

## Pr�cision g�n�rale 

Les op�rateurs fonctionnent toujours de la m�me fa�on quel que soit le mode choisi. Par exemple, l'op�rateur null-coalescence ?? a un comportement inchang� quel que soit le mode. Car OUI, **une r�f�rence non nullable peut toujours recevoir la valeur null** suivant les sc�narii ou les d�pendances choisies.

![image.png](/Images/02_Item.png)

### Exemple de gestion de la nullabilit� avec affectation de valeur par d�faut :

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

Si on d�cide d'activer le contexte de nullabilit� dans une nouvelle assembly qui utilise une classe avec le contexte d�sactiv� ou provenant d'une assembly ayant le contexte d�sactiv�, alors aucun warning ne sortira, il faudra bien continuer � tester la nullabilit�...

En effet, ci-dessous la classe **DisableClass** poss�de une propri�t� MyData d�clar�e en string donc vue comme non nullable pour la classe appelante au contexte nullable activ� alors qu'elle peut avoir la valeur nulle sans warning.

![image.png](/Images/10_Item.png)

# Conclusion et conseils

La gestion des nullables est une belle am�lioration en terme de qualit� de code. Par contre, il est toujours possible d'introduire des r�f�rences nulles si on ne pr�te pas attention aux warnings en mode Enable/Warning ou si les warnings sont d�sactiv�s, ou encore, plus probl�matique, si on r�cup�re une classe avec un contexte d�sactiv� (ancienne classe par exemple).
**Mes conseils :** 

Il est important de prendre conscience qu'une r�f�rence d�clar�e comme non nullable peut donc recevoir la valeur nulle sans erreur � l'ex�cution.

Partant de ce principe, il ne faut pas changer ses habitudes et :
- continuer � corriger tous les warnings dont les nouveaux g�n�r�s par le Nullable Context choisi.
- continuer � tester la nullabilit� des r�f�rences malgr� tout, pour g�rer tous les cas particuliers comme des warnings non pris en compte dans une classe non ma�tris�e (assembly legacy) ou encore en phase de migration, etc...
- privil�gier l'usage des helpers Microsoft comme Guard pour tester des pr�conditions incluant la nullability : https://learn.microsoft.com/en-us/windows/communitytoolkit/developer-tools/guard
- migrer progressivement tout le code du mode Disable vers Enable en suivant les �tapes dans l'ordre : Disable, Warning, Annotations, et enfin Enable.

# Annexe - passage de warning � error sur les cas de nullabilit� 

Pour forcer cette correction de warning et am�liorer la qualit� du code, on pourrait imaginer de transformer les warnings sur la nullabilit� en erreur en changeant les settings du projet comme ceci :

![image.png](/Images/11_Item.png)

Ou en version texte : CS8600;CS8601;CS8602;CS8603;CS8604;CS8613;CS8614;CS8618;CS8619;CS8620;CS8622;CS8625;CS8629;CS8632;CS8633;CS8767


# R�f�rences : 
- https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-contexts
- https://learn.micro1oft.com/en-us/dotnet/csharp/nullable-migration-strategies

