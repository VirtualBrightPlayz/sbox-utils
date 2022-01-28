# AStar/NavGraph Usage

## Creating your graph in Hammer

### `astar_nav_point`
- Place `astar_nav_point` entities where you want the AI to walk
- Make sure the points are visible from each other in order for the points to automatically connect to each other

### `astar_nav_connector`
- Place them where you want, as long as one or more `astar_nav_point` can see it
- Set the `OtherConnector` property to the other point entity (must be `astar_nav_connector`)

## Debugging and testing your graph

After you have compiled your map, you will need to check if all the points are connected.

- Set the convar `astar_debug` from `0` to `1`
- - This will make lines with arrows appear slightly above the points
- - White lines mean a point is on the default map
- - Red lines are points not on the default map
- - The lines are connections to a point
- - Make sure points are bi-directional by looking at the arrows (unless mono-directional is desired)
- After checking all the points, run the `astar_test` command to choose two random points on the map
- - View the result by looking around in your world