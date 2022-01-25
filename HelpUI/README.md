# HelpUI API


## `use_help_ui`

- Notes:
- - static ClientVar that toggles the HelpUI
- Type: `bool`
- C#: `HelpUI.UseHelpUI`


## `AddEntry(InputButton, string)`

- Notes:
- - static void to add or update an entry
- - run this every frame you want the UI to show
- - Entries take 0.25 seconds to remove itself after no calls to show the entry
- C#: `HelpUI.AddEntry(button, action)`


## `IsBound(InputButton)`

- Notes:
- - Only tested on XBox One Controller and Keyboard/Mouse, but should work for other setups
- - returns true if the specified InputButton is bound to an action
- - Gives errors outside of the Client and Menu, do not run on the server
- C#: `HelpUI.IsBound(button)`