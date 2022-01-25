# sbox-utils
A collection of utility scripts for s&amp;box gamemode devs


## HelpUI

- Adds the current controls and what they do in the bottom left corner, similar to Minecraft for consoles

### Example

- Notes
- - Run this in Simulate, Tick, Frame or something similar
- - Avoid running this on the server, keep it on the client

```cs
HelpUI.AddEntry(InputButton.Use, "Use");
```