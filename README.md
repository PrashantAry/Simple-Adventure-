Please run this program in vscode. 
Use CMD to run the program. 

dotnet build         // This compiles your code
dotnet run           // This runs your compiled code


Simple Adventure - Our Game

"Simple Adventure" is a scaled-down version of the original with several simplifying characteristics. For example:

Instead of allowing the user to type in any command, a menu of possible actions is provided
The "world" is much, much, much smaller (although it could be scaled up pretty easily)
The object of the game is for the player to gain entrance into a town. The entrance is guarded by a big, invincible Guard that won't let the player in until the player can prove they are trustworthy.

The map for Simple Adventure is... well... pretty simple:

             THE RIVER
              (Drink)
                 ^
                 |
                 V
  THE           THE            THE
 WOODS <---> CROSSROADS <---> BRIDGE
(Sword)          ^           (Goblin)
                 |
                 V
           THE TOWN GATES
              (Guard)
                 |
                 V
              THE TOWN
             (The Goal)
The player moves around the map discovering things and defeating monsters until the Guard deems him trustworthy enough to enter the town - thus winning the game.

Of course, the real goal of Simple Adventure is to give you a rich collection of classes and objects to string together into a working game.
