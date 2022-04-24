# PCG_TECH_DEMO

# Third party libraries and asset use：
Steering Behaviours using the Unity Movement AI package： https://github.com/sturdyspoon/unity-movement-ai \
UnityWFC library by Joseph Parker: https://github.com/mxgmn/WaveFunctionCollapse \
Algorithmic implementation of AI for automatic obstacle avoidance: http://www.theappguruz.com/blog/unity-3d-enemy-obstacle-awarness-ai-code-sample

# Map Generate:
1. Wave Function Collapse:Generating the first maze map area using the UnityWFC library by Joseph Parker. This method is based on the collapse wave function and generates maps randomly in a certain area based on the input sample prefabricated bodies and their constituent structures. This generated maze is used to test the pathfinding AI.

Related scripts: OverlapWFC.cs, Training.cs

2. Cellular Automata Generator: This map generation method creates a randomly sized map at the specified gameobject location. This system first divides this area into random grid areas of 0 or 1, then generates solid or dummy wall squares at the grid of 1 and empty space at the location of 0. Each grid is then examined and if this grid is a solid wall but there are more than 4 walls in one surrounding grid and more than 2 walls in two grids, then this is turned into an open space. If this is an open space but has more than 5 walls in 1 grid around it, then it is turned into a wall. After performing the above 4 optimisations on the initial map, bosses and gold are generated on the map. When the grid is empty and there are no other items around, place gold and then traverse a random number of grids before placing bosses to prevent bosses and gold from being generated too far apart. After each set of bosses and gold is prevented, 20 grids are skipped before the next set is placed to prevent all these objects from gathering in the same area. 

Related scripts：mapgenerator.cs

# AI auto pathfinding algorithm：
1. EnemyMove: This pathfinding mechanism combines WallAvoidance, SteeringBasics, MovementAIRigidbody and an automatic obstacle avoidance from the Movement AI package of Steering Behaviours. SteeringBasics, MovementAIRigidbody are the basic scripts for object movement, while WallAvoidance is used to prevent objects from colliding with walls, and automatic obstacle avoidance is added to test whether the Agent can improve its ability to avoid walls and reach its destination.

Automatic obstacle avoidance creates detection lines on the entity while specifying the forward target for the agent, to detect the presence of walls on the left and right and in the direction of movement, there is a forward detection line and a left and right detection line on the left and right of the entity, when the forward detection line touches a wall in the process, determine whether the detection lines on the left and right sides also touch the wall, if they do not touch the wall then turn towards the side without the wall to complete the obstacle avoidance operation. If it does not touch the wall, it will turn towards the side without the wall to complete the operation of avoiding the obstacle. If there are also walls on either side, a random turn is made.

During testing, it was found that WallAvoidance in the Movement AI package of Steering Behaviours still had difficulty in wall avoidance against randomly generated maps and was unable to specify a movement target. The original automatic obstacle avoidance technique on its own did not work well for pathfinding, especially when the object entered a corner as it was judged to have walls in front of it and to the left and right, so the two algorithms were combined and the automatic obstacle avoidance was added with a random The situation was improved by combining the two algorithms and adding a random obstacle avoidance condition to automatic obstacle avoidance, but the final result was still not as good as it could have been.

Related scripts: WallAvoidance.cs, SteeringBasics.cs, MovementAIRigidbody.cs, MonsterMove.cs




