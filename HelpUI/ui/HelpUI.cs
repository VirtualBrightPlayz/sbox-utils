using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace VirtualBright.HelpUI
{
    public partial class HelpUI : Panel
    {
        [ClientVar(Name = "use_help_ui")]
        public static bool UseHelpUI { get; set; } = true;
        public static HelpUI UI { get; private set; }
        public Dictionary<InputButton, HelpUIEntry> Entries { get; set; } = new Dictionary<InputButton, HelpUIEntry>();

        public HelpUI()
        {
            UI = this;
            StyleSheet.Load("/ui/HelpUI.scss");
        }

        public override void Tick()
        {
            base.Tick();
            UI = this;
        }

        public static void AddEntry(InputButton button, string action)
        {
            UI?.AddEntryInst(button, action);
        }

        public static bool IsBound(InputButton button)
        {
            return !(string.IsNullOrWhiteSpace(Input.GetButtonOrigin(button)) || Input.GetButtonOrigin(button) == "None");
        }

        private void AddEntryInst(InputButton button, string action)
        {
            if (!UseHelpUI)
                return;
            if (Entries.TryGetValue(button, out var val))
            {
                if (val == null)
                {
                    Entries.Remove(button);
                    return;
                }
                val.UpdateAction(action);
            }
            else
            {
                var ch = new HelpUIEntry(button, action);
                Entries.Add(button, ch);
                AddChild(ch);
            }
        }
    }

	public partial class HelpUIEntry : Panel
	{
        public Label Title { get; set; }
        public Image Icon { get; set; }
        public Panel BlurBG { get; set; }
        InputButton button;
        string name;
        public RealTimeSince TimeAlive { get; set; }

        public HelpUIEntry(InputButton button, string name)
        {
            this.button = button;
            this.name = name;
            Title = Add.Label(name, "title");
            Icon = Add.Image(string.Empty, "icon");
            BlurBG = Add.Panel("blur");
            TimeAlive = 0f;
        }

        public override void Tick()
        {
            base.Tick();
            var glyph = Input.GetGlyph(button, InputGlyphSize.Small);
            Icon.Texture = glyph;
            if (glyph == null)
                Icon.Style.AspectRatio = 1f;
            else
                Icon.Style.AspectRatio = (float)glyph.Width / glyph.Height;
            BlurBG.Style.AspectRatio = Icon.Style.AspectRatio;
            Style.Display = HelpUI.IsBound(button) ? DisplayMode.Flex : DisplayMode.None;
            if (TimeAlive > 0.25f)
            {
                HelpUI.UI.Entries.Remove(button);
                Delete();
            }
        }

        public void UpdateAction(string name)
        {
            TimeAlive = 0f;
            if (this.name == name)
                return;
            this.name = name;
            Title.Text = name;
        }
	}
}