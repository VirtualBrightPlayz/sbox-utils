# sbox-utils
A collection of utility scripts for s&amp;box gamemode devs

## Other People's work

I will link similar projects to this (i.e., not a full gamemode, but a set of utility scripts).

Feel free to create an issue requesting me to add a git repo.


## AStar/NavGraph

- A Navigation Graph that uses AStar (as far I can tell, it could be a modified version of AStar) for pathfinding
- Supports runtime graph changes (aka, hammer isn't required)

### No Example Yet
Just look at the `USAGE.md` and `API.md` for more info

## HelpUI

- Adds the current controls and what they do in the bottom left corner, similar to Minecraft for consoles

### Example

- Notes
- - Run this in Simulate, Tick, Frame or something similar
- - Avoid running this on the server, keep it on the client

```cs
HelpUI.AddEntry(InputButton.Use, "Use");
```
