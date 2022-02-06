# ShpiderGame
## Premise
You are a spider.

You must kill Basement Dweller (currently represented by a red capsule).

You kill him by sabotaging the **green** objects.

All **red** objects kill you on touch.

## Controls
* W - move forward
* A/D - turn left/right

## Setup Instructions
### Running the exe (recommended)
1. Go to [Releases](https://github.com/steven-vd/ShpiderGame/releases).
2. Download the newest release (zip)
3. Extract the zip-archive
### Running from Unity
1. Open a terminal/command-prompt in the directory you want the project to live
2. Enter the following command (you need to have [git](https://git-scm.com/download/win) installed)
```
git clone https://github.com/steven-vd/ShpiderGame.git
```
3. Open Unity Hub
4. Download Unity version 2021.2.10f1 or newer
5. Open the project with the "Open"-button from Unity Hub

## Known issues/bugs (and possible solutions)
* Sometimes, when moving from one surface to another, the player will rapidly flip between the two surfaces. If this happens, try getting to the correct surface, then turning until you're no longer looking at the surface you came from
* Player may fall when trying to climb onto desk surface. If this happens, make sure you are orthogonal to the surface's edge
* Player can still sometimes fall through the map, though this is rare
* On Windows, the light gets funky when turned off (this is only a graphical glitch)
