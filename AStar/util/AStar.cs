using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace VirtualBright.Util
{
    public static partial class AStar
    {
        public static List<AStarEntity> Map = new List<AStarEntity>();
        public static Func<AStarEntity, AStarEntity, float> StockH = (a, b) => a == null || b == null ? 0f : MathF.Min(a.Distance(b), b.Distance(a));

        [ServerVar("astar_debug")]
        public static bool AStarDebug { get; set; } = false;

        // THIS IS A DEBUG FUNCTION
        [AdminCmd("astar_test")]
        public static void GetPath()
        {
            float time = 10f;
            var start = Rand.FromList(Map);
            var end = Rand.FromList(Map);
            var items = GetPath(start.Position, end.Position);
            DebugOverlay.Line(start.Position, end.Position, Color.Blue, time, false);
            for (int i = 0; i < items.Length - 1; i++)
            {
                DebugOverlay.Line(items[i] + Vector3.Up * 20f, items[i+1] + Vector3.Up * 20f, Color.Red, time, false);
            }
            foreach (var item in items)
            {
                Log.Info(item.ToString());
            }
        }

        public static Vector3[] GetPath(Vector3 start, Vector3 end)
        {
            return GetPath(GetNode(start, Map.ToArray()), GetNode(end, Map.ToArray()), StockH, Map.ToArray());
        }

        public static AStarEntity GetNode(Vector3 pos, AStarEntity[] map)
        {
            float dist = float.MaxValue;
            AStarEntity ent = null;
            foreach (var item in map)
            {
                if (Trace.Sphere(0.5f, pos + Vector3.Up * 2f, item.Position + Vector3.Up * 2f).WorldOnly().Run().Hit)
                    continue;
                float d = pos.Distance(item.Position);
                if (dist > d || ent == null)
                {
                    dist = d;
                    ent = item;
                }
            }
            return ent;
        }

        public static Vector3[] GetPath(AStarEntity start, AStarEntity end, Func<AStarEntity, AStarEntity, float> h, AStarEntity[] map)
        {
            if (start == null || end == null)
                return new Vector3[0];
            Dictionary<AStarEntity, float> queue = new Dictionary<AStarEntity, float>();
            queue.Add(start, 0f);
            Dictionary<AStarEntity, AStarEntity> cameFrom = new Dictionary<AStarEntity, AStarEntity>();
            Dictionary<AStarEntity, float> costSoFar = new Dictionary<AStarEntity, float>();
            cameFrom.Add(start, null);
            costSoFar.Add(start, 0f);
            AStarEntity current = null;
            int count = 0;
            while (count < 10000 && queue.Count != 0)
            {
                count++;
                current = queue.OrderByDescending(x => x.Value).FirstOrDefault().Key;
                queue.Remove(current);

                if (current == end)
                {
                    break;
                }

                bool found = false;
                foreach (var next in current.connected)
                {
                    float newCost = costSoFar[current] + h(current, next);
                    if ((!cameFrom.ContainsKey(next)) || newCost < costSoFar[next])
                    {
                        if (costSoFar.ContainsKey(next))
                            costSoFar[next] = newCost;
                        else
                            costSoFar.Add(next, newCost);
                        float priority = 0f;// + h(next, end);
                        if (queue.ContainsKey(next))
                            queue[next] = priority;
                        else
                            queue.Add(next, priority);
                        if (cameFrom.ContainsKey(next))
                            cameFrom[next] = current;
                        else
                            cameFrom.Add(next, current);
                        found = true;
                    }
                }
                /*if (!found)
                {
                    var prev = cameFrom[current];
                    if (prev != null)
                    {
                        if (queue.ContainsKey(prev))
                            queue[prev] = float.MaxValue;
                        else
                            queue.Add(prev, float.MaxValue);
                    }
                }*/
            }
            /*foreach (var key in cameFrom.Keys)
            {
                Log.Info($"{key} {cameFrom[key]}");
            }*/
            List<Vector3> path = new List<Vector3>();
            // Log.Info(current == end);
            // Log.Info(count);
            path.Add(current.Position);
            AStarEntity cur = cameFrom[current];
            while (cur != null && cameFrom.ContainsKey(cur))
            {
                path.Add(cur.Position);
                cur = cameFrom[cur];
            }
            return path.ToArray();
        }
    }
}