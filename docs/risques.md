# Registre des risques — Exam Conduite

Échelles : P et I de 1 à 3. Criticité = P × I
(1-2 faible, 3-4 moyenne, 6-9 élevée). Revu à chaque sprint planning.

## Synthèse (de la criticité la plus haute à la plus basse)

| ID | Risque | Criticité | Responsable |
|----|--------|-----------|-------------|
| R1 | Versions de Unity différentes | 6 (élevée) | William |
| R3 | Réapparition au mauvais point de contrôle | 6 (élevée) | Lucas |
| R4 | Temps insuffisant pour BF-02 et BF-03 | 6 (élevée) | Erwan |
| R6 | Intégration des objets rechargeables | 4 (moyenne) | Christine |
| R2 | Conflit dans `CarController.cs` | 3 (moyenne) | William |
| R5 | Résultats non affichés à la fin de la course | 3 (moyenne) | Christine |

## R1 — Versions de Unity différentes

- **Risque :** Parce que le projet original utilisait Unity 2020.3 et que l'équipe travaille maintenant avec Unity 6000.3.24f1, il est possible qu'un membre ouvre ou enregistre le projet avec une autre version, ce qui entraînerait des erreurs de compatibilité, des modifications automatiques de fichiers ou l'impossibilité d'ouvrir certaines scènes.
- **BF touchés :** Tout le projet
- **Catégorie :** Technique
- **P :** 2 · **I :** 3 · **Criticité :** 6 (élevée)
- **Stratégie :** Réduire
- **Mitigation :** La version exacte `6000.3.24f1` est inscrite dans le README et chaque membre confirme dans l'issue R1 qu'il a installé et ouvert le projet avec cette version.
- **Déclencheur :** Unity propose de mettre le projet à niveau ou modifie `ProjectVersion.txt` lors de l'ouverture du projet.
- **Contingence :** Fermer Unity sans enregistrer, revenir au dernier commit fonctionnel de la branche principale et installer `6000.3.24f1` avec Unity Hub avant de rouvrir le projet.
- **Responsable :** William
- **Statut :** Ouvert

## R2 — Conflit dans CarController.cs

- **Risque :** Parce que BF-01 nécessite de modifier `CarController.cs`, il est possible qu'un autre membre modifie le même fichier pour sa fonctionnalité, ce qui entraînerait un conflit de fusion ou la perte d'une partie du code.
- **BF touchés :** BF-01
- **Catégorie :** Technique
- **P :** 1 · **I :** 3 · **Criticité :** 3 (moyenne)
- **Stratégie :** Réduire
- **Mitigation :** William est le seul membre qui modifie directement `CarController.cs`; toute modification demandée par un autre membre est décrite dans une issue et intégrée par William dans une branche dédiée.
- **Déclencheur :** —
- **Contingence :** —
- **Responsable :** William
- **Statut :** Ouvert

## R3 — Réapparition au mauvais point de contrôle

- **Risque :** Parce que BF-03 doit replacer le véhicule au dernier point de contrôle activé, il est possible que la voiture réapparaisse au mauvais endroit après une chute ou une sortie de piste, ce qui empêcherait le joueur de poursuivre correctement la course.
- **BF touchés :** BF-02, BF-03
- **Catégorie :** Technique
- **P :** 2 · **I :** 3 · **Criticité :** 6 (élevée)
- **Stratégie :** Réduire
- **Mitigation :** Lucas teste et documente dans l'issue R3 trois scénarios : réapparition après une sortie de piste, après une chute et après le passage de deux points de contrôle consécutifs.
- **Déclencheur :** Un des trois tests replace la voiture ailleurs qu'au dernier point de contrôle validé.
- **Contingence :** Désactiver temporairement la réapparition au dernier point de contrôle et replacer la voiture au point de départ jusqu'à ce que l'enregistrement du dernier point valide soit corrigé.
- **Responsable :** Lucas
- **Statut :** Ouvert

## R4 — Temps insuffisant pour BF-02 et BF-03

- **Risque :** Parce que plusieurs fonctionnalités essentielles doivent être développées et intégrées avant la remise finale, il est possible que le temps prévu pour terminer et tester les points de contrôle (BF-02) et la réapparition (BF-03) soit insuffisant, ce qui entraînerait un retard du gameplay et pourrait compromettre la remise du projet.
- **BF touchés :** BF-02, BF-03
- **Catégorie :** Planification
- **P :** 2 · **I :** 3 · **Criticité :** 6 (élevée)
- **Stratégie :** Réduire
- **Mitigation :** Erwan, responsable de BF-02, découpe les points de contrôle en tâches vérifiables dans l'issue BF-02; Lucas, responsable de BF-03, fait de même pour la réapparition dans l'issue BF-03. Ils font ensuite valider ensemble une version jouable des deux fonctionnalités par un autre membre au plus tard le 22 novembre 2026.
- **Déclencheur :** Le 22 novembre 2026, BF-02 ou BF-03 n'est pas démontrable dans une course complète.
- **Contingence :** Terminer d'abord BF-02, puis simplifier BF-03 afin que la voiture réapparaisse au point de départ au lieu du dernier point de contrôle.
- **Responsable :** Erwan
- **Statut :** Ouvert

## R5 — Résultats non affichés à la fin de la course

- **Risque :** Parce que BF-20 dépend du chronomètre (BF-18) et de la détection de la fin de la course, il est possible que ces fonctionnalités ne soient pas correctement reliées, ce qui empêcherait l'affichage des résultats à la fin de la course.
- **BF touchés :** BF-18, BF-20
- **Catégorie :** Exigences
- **P :** 1 · **I :** 3 · **Criticité :** 3 (moyenne)
- **Stratégie :** Réduire
- **Mitigation :** Christine ajoute un test d'intégration manuel vérifiable dans l'issue BF-20 : terminer une course, confirmer l'ouverture du panneau et comparer le temps final affiché avec le chronomètre.
- **Déclencheur :** —
- **Contingence :** —
- **Responsable :** Christine
- **Statut :** Ouvert

## R6 — Intégration des objets rechargeables

- **Risque :** Parce que les objets rechargeables utilisent le gameplay (BF-04), l'animation (BF-08), l'effet visuel (BF-12) et le son (BF-16), il est possible qu'un membre modifie un élément sans prévenir les autres, ce qui entraînerait des incompatibilités pendant l'intégration et ralentirait le développement.
- **BF touchés :** BF-04, BF-08, BF-12, BF-16
- **Catégorie :** Communication
- **P :** 2 · **I :** 2 · **Criticité :** 4 (moyenne)
- **Stratégie :** Réduire
- **Mitigation :** Christine maintient un seul prefab d'objet rechargeable et ajoute dans l'issue BF-04 une liste à cocher confirmant l'intégration du gameplay, de l'animation, de l'effet visuel et du son avant la fusion.
- **Déclencheur :** —
- **Contingence :** —
- **Responsable :** Christine
- **Statut :** Ouvert
