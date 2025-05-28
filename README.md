# OC-Rendus-P8

# TourGuide - Optimisation et Intégration Continue

## Description

Ce projet a été réalisé dans le cadre de la formation **OpenClassrooms**. Il s'agit d'un projet d’optimisation logicielle et d’intégration continue sur une application existante, développée en C# avec ASP.NET Core.

**TourGuide** est une application de simulation touristique qui permet de suivre les emplacements des utilisateurs, d’interagir avec des attractions, et de calculer des récompenses.
Le projet visait à :

* **Stabiliser les tests existants**, dont certains échouaient de manière aléatoire.
* **Améliorer les performances globales** de l'application.
* **Mettre en place une pipeline CI avec GitHub Actions**.

## Technologies utilisées

* **.NET 7**
* **ASP.NET Core**
* **xUnit** (tests unitaires)
* **GitHub Actions** (intégration continue)
* **System.Diagnostics / Logging**
* **Parallelism, Task-based async/await programming**

## Scénario

Vous intégrez une équipe technique chargée de maintenir et d'optimiser une application ASP.NET Core nommée **TourGuide**.
Plusieurs tests unitaires affichaient un comportement non-déterministe : certains échouaient de façon aléatoire à cause de problématiques de concurrence et de lourdeurs algorithmiques.

Votre mission :

* Identifier les causes de ces échecs.
* Refactoriser le code pour en améliorer la **complexité cyclomatique** et la **gestion de la concurrence**.
* Intégrer les bonnes pratiques de **programmation asynchrone** avec `async/await`, `Task`, `Parallel.ForEach`, etc.
* Ajouter une **pipeline CI complète avec GitHub Actions**.

## Installation et exécution

### Prérequis

* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Un éditeur tel que [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Étapes

1. **Cloner le dépôt**

```bash
git clone https://github.com/votre-utilisateur/tourguide.git
cd tourguide
```

2. **Restaurer les dépendances**

```bash
dotnet restore TourGuide.sln
```

3. **Lancer l’application**

```bash
dotnet run --project TourGuide
```

4. **Exécuter les tests**

```bash
dotnet test
```

5. **Générer les fichiers de publication**

```bash
dotnet publish TourGuide.sln -c Release -o publish
```

## Fonctionnalités principales

* Application de simulation touristique avec suivi de position, attractions et récompenses.
* Refactorisation complète des composants critiques pour améliorer la performance et la stabilité.
* Tests unitaires fiables et maintenables avec **xUnit**.
* Tests de performance (désactivés par défaut — voir section suivante).
* Intégration continue via **GitHub Actions**.

## Tests de performance

Deux tests de performance sont inclus dans le projet. Ils sont désactivés par défaut pour éviter de ralentir la CI/CD.

Pour les exécuter, **supprimez l'attribut `Skip`** dans les fichiers de test :

```csharp
[Fact(Skip = "Delete Skip when you want to pass the test")]
public async Task HighVolumeTrackLocation()

[Fact(Skip = "Delete Skip when you want to pass the test")]
public async Task HighVolumeGetRewards()
```

Ces tests permettent de vérifier la robustesse et les performances de l’application sous forte charge.

## Intégration Continue (GitHub Actions)

Une pipeline GitHub Actions est incluse et configurée pour :

* S’exécuter sur chaque **push** ou **pull request** vers `master` ou `dev`.
* Restaurer les dépendances, compiler la solution, exécuter les tests.
* Publier l'application et la zipper.
* Enregistrer l’artefact `TourGuide.zip` (version prête au déploiement).

### Lancer la CI manuellement

Si vous souhaitez lancer la CI manuellement, vous pouvez créer un **workflow `workflow_dispatch`** ou relancer un workflow existant depuis l'interface **GitHub > Actions**.

## Structure du projet

```
/TourGuide              => Projet principal
/TourGuideTest          => Projet de tests unitaires
/GpsUtil, RewardCentral, TripPricer => Projets externes liés
/.github/workflows      => Fichier de configuration GitHub Actions
```

## Auteur

* **Grégoire Bouteillier**

## Remarques

🚨 **Ce projet est un exercice réalisé dans le cadre de la formation OpenClassrooms. Les services simulés, les données utilisateurs et les traitements sont entièrement fictifs.**
