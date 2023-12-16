[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/dMUm1NVd)  
# Story
You got diabetes.  
  
The very same thing that caused the deaths of both your mom and your grandma. It's like a family curse. But you don't feel like dying just yet.  
You decided to take matters into your own hands and embarked on a journey of revenge to kill everything sweet and cure your diabetes.  
  
You ventured into the Food World — an alternate reality where everything you have ever eaten transformed into gruesome monsters. Using the power of salty weapons, you slayed every sweet monster you encountered.  
And now, you reached your final destination — the Labyrinth Castle, where the detestable Sweet Emperor resides.  
  
You have but a single goal ahead of you: Kill the Sweet Emperor and put an end to this farce.  
# Mechanics
## UI
- Status box
- Inventory
- Text box - contains narration or dialogue, also contains action buttons
- Equipped weapons visible on the screen - can be hovered for stat info
## Stats
- Health (HP)
- Diabetes level (DL)
    - Fills up by fighting monsters or consuming sweet items
    - The player dies if DL reaches 100%
## Items
### Weapon
- Can be equipped or stored in the inventory
- Stats:
    - Damage
    - Critical chance
### Shield
- Can be equipped or stored in the inventory
- Stats:
    - Block chance
### Consumables
- Stored in the inventory, can be eaten at any time
- Sweet consumables heal the player, but increase DL (depends on the "sweetness" stat)
- Salty consumables decrease DL (depends on "saltiness" stat)
## Fights
- Turn-based combat
- Two attack options:
    - Normal atttack: always hits, deals moderate damage
    - Attempt critical hit: can either succeed or fail (depends on weapon stats, base chance 50%), deals great damage if it lands
- Enemies attack automatically
- Chance to block enemy attacks (depends on shield stats)
## Enemies
- Regular monsters
- Bosses - Each boss is unique, drops items
## Navigation
- Moving to new locations by clicking on doors (or other things)
- Clickable things marked by arrows
## Inventory
- Max four items (not counting equipped items)
- Items can be used or dropped (item is lost forever if dropped)
