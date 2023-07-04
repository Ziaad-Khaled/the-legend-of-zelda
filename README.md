# The Legend of Zelda: Breath of the Wild

## Project Overview

The Legend of Zelda: Breath of the Wild is an open-world action-adventure game set in the kingdom of Hyrule. Players take on the role of Link, an agile explorer, master fighter, and skilled rune user. The game focuses on exploration, combat, and puzzle-solving using various abilities and weapons.

## Game Mechanics
### Controls

- The player controls the camera with the mouse movement.
- Link's walking movements (forward, backward, right, left) are controlled using the arrow keys or "W," "A," "S," "D" keys.
- The player can sprint by holding "left-shift" along with a movement key.
- Jumping is achieved by pressing the "space-bar" key.
- Holding "space-bar" mid-air allows gliding.
- Climbing specific vertical surfaces can be done by holding "left-shift" while near the surfaces.
- Combat modes (melee and ranged) can be switched using the "Tab" key.
- Melee mode uses the sword and shield, with left-mouse button for attacking and right-mouse button for blocking.
- Ranged mode uses the bow and arrow, with right-mouse button for aiming and left-mouse button for firing.
- Abilities can be selected using the number keys 1, 2, 3, and 4 for Remote Bombs, Cryonis, Magnesis, and Stasis, respectively.
- The selected rune ability is activated using the "Q" key.
- The player can pause and resume the game by pressing the "ESC" button.

## Player Mechanics
### Health & Damage

- Link starts with 24 health points.
- Link loses health points based on the attacks' damage points.
- Falling continuously from more than 10 units results in losing all health points and displaying the "Game Over screen."

### Movement & Navigation

- Link is controlled from a 3rd person's perspective.
- Sprinting doubles Link's movement speed.
- Link can jump and glide using a paraglider.
- Climbing specific vertical surfaces allows vertical and horizontal movement.
- Link cannot perform any other actions while climbing.

### Combat

- Link has two combat modes: Melee (sword & shield) and Ranged (bow & arrow).
- Sword attacks deal 10 damage points per hit.
- Shield can block all incoming damage.
- Bow & Arrow can be used to attack enemies from afar.

### Rune Abilities

Link has access to various rune abilities:

- Remote Bomb: Throw spherical bombs that can be detonated. Deals 10 damage points to enemies within the explosion range.
- Cryonis: Create ice pillars on water surfaces.
- Stasis: Freeze moving objects for a limited time.

## Enemies

### Normal Enemies

Normal Enemies are found in the Overworld level. They are initially idly standing in camping sites in small groups. Once Link approaches their camping area or begins attacking them from a distance, the enemies are alerted and start sprinting towards Link to attack him. The enemies will continue to attack Link until either Link or all of the enemies are killed.

Alerted enemies can perform different actions such as light and fast horizontal attacks, heavy and slow vertical attacks, trying to reach Link if he is out of range, reacting to damage upon being hit, briefly waiting between attacks, or dying when their health points reach zero. The enemies' health is displayed via a health bar on top of them in world space.

There are two types of normal enemies in the game:
| Enemy Name | Health Points | Attack Damage (Horizontal) | Attack Damage (Vertical) |
|------------|---------------|---------------------------|-------------------------|
| Bokoblin   | 20            | 1                         | 3                       |
| Moblin     | 30            | 2                         | 4                       |

### Boss (Fireblight Ganon)

Fireblight Ganon is a phantom of Calamity Ganon that took control of the Divine Beasts and utilizes fire as his primary means of both defense and offense. Fireblight Ganon has 200 health points and generally roams the boss arena.

In Phase 1, Fireblight Ganon attacks Link by launching a small fireball towards him every few seconds. Each small fireball deals 2 damage points to Link upon hit and is destroyed immediately when touching a surface.

When Fireblight Ganon reaches 150 health points, he enters Phase 2. In this phase, he no longer uses the small fireball attack. Instead, he charges up a large fireball attack, which deals 5 damage points to Link upon hit. Fireblight Ganon must charge the attack for several seconds before launching it. During this charging animation, his protective flame force field is disabled, making him vulnerable to damage. After finishing the attack, his flame force field is automatically restored.


## Level Design

The game features five levels, including an Overworld level, a Shrine level, and a Boss Arena level for each implemented boss.
### 1. Overworld

- An Overworld level is where Link must explore and fight enemies before reaching the end.
- The Overworld level must include at least two camp areas where groups of normal enemies rush towards Link when he approaches or attacks them from afar. Each camp should contain both types of normal enemies, with a total of 4-5 enemies per camp.
- The Overworld level should have areas that are only accessible by climbing and gliding.
- When Link reaches the shrine entrance, he is automatically taken to the Shrine level.

### 2. Shrine

- A Shrine level requires Link to use all implemented Rune Abilities to reach a goal area.
- Falling into an endless void results in Link's death in a Shrine level.
- No enemies should be present in a Shrine level.
- All Shrine walls and objects are not climbable.
- Objects affected by abilities can include boxes, rocks, swinging sandbags, and moving platforms.
- When Link finishes a Shrine level by reaching a particular trigger area, he is automatically taken to the first Boss Arena level.

### 3.Boss Arena
- Each Boss Arena level is where Link fights one boss.
- There must be a unique Boss Arena for each implemented boss.
- The Boss Arena must provide enough space for the boss to perform all their behaviors, and for Link to attack, take cover, and evade boss attacks.
- If the boss requires specific objects to perform certain actions, the Boss Arena must include a sufficient amount of those objects.
- When Link defeats a boss, he is automatically taken to the next boss level (if it exists), or the game ends with studio credits rolling.

## Screens
### Main menu
- Play
  - New Game: Starts a new game from the Overworld level
  - Level Select: Allows the player to directly play a specific level.
- Options
  - Audio
    - Music level
    - Effects level
  - Team Credits: Lists each team member’s name and project role.
  - Asset Credits: Lists all external assets and their sources.
- Quit Game

### Pause Screen
- Resume
- Restart Level
- Quit to Main Menu

### Game Over Screen
- Restart Level
- Quit to Main Menu


## Heads-Up Display (HUD)

The Heads-Up Display (HUD) displays important information about the player's status:

- Life gauge: Represents Link's health points using hearts. Each full heart represents 2 health points, and each half heart represents 1 health point.
- Currently selected Equipment/Weapon
- Currently selected Rune Ability
- Boss health bar (Boss level(s) only): Displays the boss's health as a health bar overlaid on the top of the screen.

## Graphics
### Models

    The game requires models for the environment, characters, and weapons. Models from the original game or alternative models that meet the requirements can be used.

### Animations
#### Link Animations

- Idle
- Walking
- Sprinting
- Jumping
- Gliding
- Drawing Bow
- Releasing Bow
- Swinging Sword
- Blocking with Sword/Shield
- Throwing Bomb
- Hit Reaction
- Dying

#### Normal Enemies Animations

-Idle
- Sprint/Run
- Horizontal Attack
- Vertical Attack
- Hit Reaction
- Block Attack
- Dying

#### Boss Enemy Animations

- Idle
- Different relevant animation for every Boss attack and state
- Dying

## Audio
### Sound Settings

The game's audio should be divided into at least two independently controllable categories: Music and Sound effects (SFX).

### Sound Effects
#### Effects

- Footsteps of Link as he moves.
- Footsteps of a normal enemy as he moves.
- Footsteps of a boss enemy as he moves.

#### Feedback

- When Link is hit.
- When Link dies.
- When an enemy is hit.
- When an enemy dies.
- When an enemy weakness is hit.

#### Music

- Slow-paced track for the main and pause menus.
- Different tracks for each game level, depending on the atmosphere.

## Cheats

Implementing cheat codes is required to assist in testing different aspects of the project. The following cheat codes should be included:

- Heal: Increases Link's health by 10 health points by pressing "H".
- Toggle Invincibility: Prevents Link from taking damage when enabled by pressing "I".
- Toggle Slow Motion: Slows down the gameplay by half when enabled.
