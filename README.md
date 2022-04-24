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

# Agent Behaviour：
1. enemy1: This pathfinding mechanism combines WallAvoidance, SteeringBasics, MovementAIRigidbody and an automatic obstacle avoidance from the Movement AI package of Steering Behaviours. SteeringBasics, MovementAIRigidbody are the basic scripts for object movement, while WallAvoidance is used to prevent objects from colliding with walls, and automatic obstacle avoidance is added to test whether the Agent can improve its ability to avoid walls and reach its destination. 

Automatic obstacle avoidance creates detection lines on the entity while specifying the forward target for the agent, to detect the presence of walls on the left and right and in the direction of movement, there is a forward detection line and a left and right detection line on the left and right of the entity, when the forward detection line touches a wall in the process, determine whether the detection lines on the left and right sides also touch the wall, if they do not touch the wall then turn towards the side without the wall to complete the obstacle avoidance operation. If it does not touch the wall, it will turn towards the side without the wall to complete the operation of avoiding the obstacle. If there are also walls on either side, a random turn is made. 

During testing, it was found that WallAvoidance in the Movement AI package of Steering Behaviours still had difficulty in wall avoidance against randomly generated maps and was unable to specify a movement target. The original automatic obstacle avoidance technique on its own did not work well for pathfinding, especially when the object entered a corner as it was judged to have walls in front of it and to the left and right, so the two algorithms were combined and the automatic obstacle avoidance was added with a random The situation was improved by combining the two algorithms and adding a random obstacle avoidance condition to automatic obstacle avoidance, but the final result was still not as good as it could have been. 

Related scripts: WallAvoidance.cs, SteeringBasics.cs, MovementAIRigidbody.cs, MonsterMove.cs

2. Boss1：This Agent combines the NavMesh and WayPoint pathfinding methods; NavMesh is a self-contained pathfinding component of unity which, after baking the corresponding map, adds the Nav Mesh Agent component to the entity to enable the Agent to automatically identify areas on the map where it can walk. WayPoint pathfinding gives the Agent a list of action points and the Agent will follow the order of the points in this list during the pathfinding process. The Agent in Boss1 combines these two methods of pathfinding, finally the Agent is able to perform the act of patrolling on the specified path.

Also, in order to interact with the player, the agent incorporates distance detection, so that when the distance between it and the player is less than a certain value, it can detect the player, stop patrolling and chase the player at the same time.

Related scripts: EnemyNav.cs

3. Boss2: This Agent uses a finite state machine (FMS) to construct its state transitions and uses collision boxes to detect players. When a player is not in the collision box area, the player is added to the collision list and the Agent state is changed to attack (as indicated by the Agent colour changing to red), when the player leaves the collision box area, the collision list removes the player from the list and the Agent state is changed to guard (as indicated by the Agent colour changing back to blue). When there are no players in the collision box area, the Agent state always remains as Guard.

Related scripts: Boss2Behave.cs

# Players：
1. Player movement and interaction：The player moves the rigid body on the x,z axis set to apply force to move. The player touches the FakeWall and Gold entities to make them disappear, and gains bonus points when touching the Gold entity.

Related scripts: PlayerControl.cs

2. Cameras：The camera is set to be tied to the player's physical movement and follows the player when the player's physical movement takes place.

Related scripts: CameraControl.cs

# Tech Demo Execution：
After enter the scene TECH_DEMO, when click on Start the map of the maze and the second area is automatically created, while each Agent performs the relevant actions according to the settings.

Related Scene: TECH_DEMO.unity

# Video Documentation URL：
