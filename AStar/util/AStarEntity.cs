using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace VirtualBright.Util
{
    [Library("astar_nav_point", Description = "AStar Nav Point")]
    // [Hammer.EditorSprite("editor/info_target.vmat")]
    [Hammer.EntityTool("AStar Nav Point", "AStar")]
    public class AStarEntity : Entity
    {
        public List<AStarEntity> connected = new List<AStarEntity>();

        public override void Spawn()
        {
            base.Spawn();
            AStar.Map.Add(this);
            connected.AddRange(All.OfType<AStarEntity>().Where(x => x != this && !connected.Contains(x) && !Trace.Sphere(5f, Position + Vector3.Up * 20f, x.Position + Vector3.Up * 20f).WorldOnly().Run().Hit));
            foreach (var item in connected.ToArray())
            {
                if (item.connected.Contains(this))
                    continue;
                item.connected.Add(this);
            }
        }

        public virtual float Distance(AStarEntity other)
        {
            return Position.Distance(other.Position);
        }

        [Event.Tick]
        public void Tick()
        {
            if (!AStar.AStarDebug)
                return;
            if (this is not AStarEntity)
                return;
            DebugOverlay.Text(Position, Name, Color.White, 0, 500);
            var draw = Sandbox.Debug.Draw.Once;
            draw.WithColor(Color.Cyan).Circle(Position + Vector3.Up * 20f, Vector3.Up, 20);
            foreach (var item in connected)
            {
                draw.WithColor(AStar.Map.Contains(this) ? Color.White : Color.Red).Arrow(Position + Vector3.Up * 20f, item.Position + Vector3.Up * 20f, Vector3.Up, 8f);
            }
        }
    }
}