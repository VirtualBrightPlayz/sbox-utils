# AStar/NavGraph API

## `NavPath`

- Notes:
- - A utility class copied from Facepunch's AI demo
- - Run `NavPath.Update(Vector3 from, Vector3 to)` on tick to update the information on the current path
- - Run `NavPath.GetDirection(Vector3 position)` with the current AI position to get the direction to the next point on the path

## `Draw`

- Notes:
- - A utility class copied from Facepunch's AI demo
- - Ignore this file if you already have it
- - Used for debugging

## `AStar`

- Notes:
- - This is the main class for the actual pathfinding
- - Provides a number of utility functions and the main pathfinding function
- - - `Vector3[] GetPath(Vector3 start, Vector3 end)` for getting the path from `end` to `start`. Reverse the array for `start` to `end`.
- - - `AStarEntity GetNode(Vector3 pos, AStarEntity[] map)` for getting the nearest physically visible node.
- - - `public static Vector3[] GetPath(AStarEntity start, AStarEntity end, Func<AStarEntity, AStarEntity, float> h, AStarEntity[] map)` for getting the path between two `AStarEntity` and using a distance function of `Func<AStarEntity, AStarEntity, float> h`. Returns `end` to `start`. Reverse for `start` to `end`.

## `AStarEntity`

- Notes:
- - Just a point entity. Can be placed at runtime, and can be inherited to modify connection calculation and distance calculation.