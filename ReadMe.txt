# Kokku Software Engineer Applicant Test


## Auto Battle RPG


This application is an auto-battle RPG that has a grid with cells/tiles/boxes organized as a matrix. Your goals are to fix the program, polish the code and implement an extra feature [(as detailed below)](#extra-features).


### Requisites


- This test must be done exclusively as a console application. Do not use any engines
- The solution must compile, and the game must be playable from start to finish
- You **must** use git for source control. You can setup a repository in whichever host you prefer. Please give access to the following account: `dev@kokku.com.br`
  - Your usage of git will be evaluated
- You are free to modify the scripts as much as you want
  - Adding, removing or modifying existing scripts are all acceptable 
  - You will be evaluated on your capacity to write clean, performant and maintainable code, following healthy design principles
- Please document the changes you make to the code
  - Provide brief explanations for your changes. You don't need to document every single line you modify


### Game Rules


On startup, the game must ask the player to pick a class and a battlefield size. The enemy character must be randomly chosen. Both characters must be randomly assigned a tile in the battlefield, and take turns to try to kill their opponent. The functionality of the program will also be evaluated. Below are some rules that must be followed:


o Each character should only perform one action per turn
    o If they are within range of a target, they must attack. Otherwise, they will move toward the nearest target
    x Your [extra feature](#extra-features) may add some extra actions that can be done in a turn
o The game should work with a battlefield of any size, including a non-square matrix (6x6 or 6x10, for example)
o The game should inform the player when the battle ends, and who was victorious
o The battlefield should only be reprinted/redrawn if there have been any visual changes to it
o Only one character is allowed per tile
o Try to make the program as dynamic as possible
    o Avoid hard-coded rules such as "the player always moves first"


### Extra Features


x Every class must implement a "Status Effect" that can be applied to other characters. Examples:
    x "Bleed": Damages the affected target every turn for a limited time
    x "Knock down": Stops the affected target from doing any actions in their next turn
    x "Heal": Heals the affected target
x Every class must implement a "Special Ability" that can be randomly performed. Examples:
    x "Knockback": Knowck the target back one tile
    x "Strong attack": Deals 2x attack damage to a target
    x "Teleport": Teleports the character to any tile
    x "Invisibility": Makes the character "hidden", stopping enemies from chasing or attacking
    x "Throw rock": Character can damage a target from far away
o Instead of only 2 characters, the game must have two opposing teams. The player can still pick their character's class. When a team is completely wiped out, they lose