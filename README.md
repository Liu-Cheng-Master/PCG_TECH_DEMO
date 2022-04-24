# PCG_TECH_DEMO

# Third party libraries and asset use：
Steering Behaviours using the Unity Movement AI package： https://github.com/sturdyspoon/unity-movement-ai \
UnityWFC library by Joseph Parker: https://selfsame.itch.io/unitywfc \
Algorithmic implementation of AI for automatic obstacle avoidance: http://www.theappguruz.com/blog/unity-3d-enemy-obstacle-awarness-ai-code-sample

# Map Generate:
1. Wave Function Collapse:Generating the first maze map area using the UnityWFC library by Joseph Parker.

Related scripts: 

2. cellular automata generator: This map generation method creates a randomly sized map at the specified gameobject location. This system first divides this area into random grid areas of 0 or 1, then generates solid or dummy wall squares at the grid of 1 and empty space at the location of 0. Each grid is then examined and if this grid is a solid wall but there are more than 4 walls in one surrounding grid and more than 2 walls in two grids, then this is turned into an open space. If this is an open space but has more than 5 walls in 1 grid around it, then it is turned into a wall. After performing the above 4 optimisations on the initial map, bosses and gold are generated on the map. When the grid is empty and there are no other items around, place gold and then traverse a random number of grids before placing bosses to prevent bosses and gold from being generated too far apart. After each set of bosses and gold is prevented, 20 grids are skipped before the next set is placed to prevent all these objects from gathering in the same area. 

Related scripts：mapgenerator.cs

 





