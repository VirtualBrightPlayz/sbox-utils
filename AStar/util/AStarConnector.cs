using System.Linq;
using Sandbox;

namespace VirtualBright.Util
{
    [Library("astar_nav_connector", Description = "AStar Nav Connector")]
    // [Hammer.EditorSprite("editor/info_target.vmat")]
    [Hammer.EntityTool("AStar Nav Connector", "AStar")]
    public class AStarConnector : AStarEntity
    {
        [Property]
        [FGDType("target_destination")]
        public string OtherConnector { get; set; }

        public override void Spawn()
        {
            connected.AddRange(FindAllByName(OtherConnector).OfType<AStarConnector>());
            base.Spawn();
        }

        public override float Distance(AStarEntity other)
        {
            if (other is AStarConnector)
                return 0f;
            return base.Distance(other);
        }
    }
}