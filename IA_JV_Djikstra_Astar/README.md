# The Green Cat: A* et Dijkstra - Démonstration de Pathfinding

![Aperçu du jeu](https://github.com/user-attachments/assets/a8d562cb-1b9c-41d3-a40d-33ad2a6b3ef1)

_The Green Cat est un jeu de survie 2D où le joueur incarne une jeune femme accompagnée d’un chat alien, cherchant à échapper aux inspecteurs jusqu'à la fin du niveau._

**Projet réalisé dans le cadre du cours d'IA pour les jeux vidéo, centré sur les algorithmes de pathfinding**

L’ensemble du projet (scripts, assets visuels) a été réalisé par moi-même, à l’exception des animations de course, qui sont inspirées du pack d'assets **Ninja Adventure de Pixel Boy**. J'ai également utilisé ses sons pour la musique et les effets sonores.

Le jeu est accessible en ligne sur [Itch.io](https://ameiswhattodo.itch.io/astar-djikstra-survivor).

---

## Sommaire

- [Téléchargement](#téléchargement)
- [Aperçu](#aperçu)
- [Fonctionnalités et Contrôles](#fonctionnalités-et-contrôles)
- [Utilisation](#utilisation)
- [Structure du Projet](#structure-du-projet)
- [Suggestions d’Améliorations](#suggestions-daméliorations)

---

### Téléchargement

Pour accéder et jouer à **The Green Cat**, suivez les étapes ci-dessous :

1. **Accéder au jeu** : Téléchargez le dossier **IA_JV_Djikstra_Astar**, qui contient les dossiers nécessaires pour le projet (les autres fichiers ne sont pas requis).
2. **Importer dans Unity** : Une fois le dossier téléchargé, importez-le dans Unity.
3. **Lancer le jeu** : Ouvrez le projet dans Unity, puis appuyez sur "Play" pour démarrer le jeu.


### Aperçu

![Image du jeu](https://github.com/user-attachments/assets/4ae1818e-b192-4967-978e-62c670471108)

The Green Cat propose deux niveaux où l’objectif est d’échapper à des inspecteurs en éliminant certains d’entre eux. On retrouve deux types d’inspecteurs :
- **Inspecteurs violets** : utilisent l’algorithme A* pour traquer le joueur.
- **Inspecteurs bleus** : utilisent l'algorithme de Dijkstra.

Dans le premier niveau, seuls les sols et les murs sont présents, tandis que le deuxième niveau introduit des zones d’eau, qui ralentissent à la fois le joueur et les inspecteurs. Cela ajoute une complexité supplémentaire au pathfinding, car les zones d'eau ont un poids plus élevé.

---

### Fonctionnalités et Contrôles

- **Déplacement du joueur** : Utilisez les touches fléchées.
- **Algorithmes de pathfinding** : A* pour les ennemis violets et Dijkstra pour les ennemis bleus.
- **Mécanique de dialogue** : Avancez dans le dialogue avec la barre d’espace.
- **Système de collision** : Empêche le joueur et les ennemis de traverser les murs.
- **Animations** : Déplacements dans quatre directions pour le joueur et les ennemis, ainsi qu'animations de mort pour les ennemis.
- **Interface utilisateur** : Compteur de score, barre de vie, menus de pause, d’échec et de fin de jeu.
- **Effets sonores et musique** : Sons pour les actions principales (dégâts, collisions, etc.) et musique d’ambiance.
- **Système de réinitialisation** : Possibilité de recommencer le niveau en cours avec une régénération des ennemis et obstacles.

### Utilisation

- **Lancer le jeu** : Cliquez sur "Play" dans l’éditeur Unity pour tester le jeu.
- **Contrôles** :
  - **Déplacement** : Utilisez les flèches directionnelles.
  - **Dialogue** : Avancez dans les dialogues avec la barre d’espace.
  - **Pause** : Appuyez sur Échap pour mettre en pause ou reprendre.
  - **Réinitialisation** : Cliquez sur "Restart" dans le menu de pause pour recommencer le niveau.

---

### Structure du Projet

- **`Assets/Scripts`** : Scripts C# pour les comportements et fonctionnalités du jeu.
- **`Assets/Prefabs`** : Objets réutilisables, comme les ennemis et les tuiles.
- **`Assets/Scenes`** : Scènes du jeu.
- **`Assets/Musics`** : Effets sonores et musiques.
- **`Assets/sprite-test-random`** : Sprites et animations.

---

### Suggestions d’Améliorations

Bien que le jeu soit pleinement fonctionnel, certaines améliorations potentielles ont été identifiées pour l'optimiser :

- **Pathfinding amélioré** : Ajouter une option pour une poursuite constante au lieu d'une traque intermittente.
- **Gestion des apparitions** : Empêcher les ennemis d'apparaître dans les murs ou trop près du joueur.
- **Transitions sonores** : Améliorer la fin des effets sonores pour les rendre plus naturels lors de la destruction d’objets.
- **Animations du joueur** : Lisser les animations pour une meilleure fluidité.
- **Ajout de niveaux** : Intégrer davantage de niveaux pour diversifier le gameplay.
- **Amélioration de la gestion des dialogues** : Éviter de réafficher tous les dialogues lors d'une réinitialisation de niveau.

--- 
