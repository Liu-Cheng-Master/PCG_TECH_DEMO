using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Reference: https://github.com/SunnyValleyStudio/ProceduralMapCHessMaze
public class mapgenerator : MonoBehaviour
{

    public int[,] grid;
    public GameObject[,] gridObject;
    public int mapsize = 50;
    public GameObject Wallprefab;
    public GameObject fakeWALLprefab;
    public GameObject monsterprefab;
    public GameObject goldprefab;
    public GameObject waypointprefab;
    int numboss = 0; // Number of bosses generated in the map
    int numgold = 0; // Number of golds generated in the map

    void Start()
    {
        grid = new int[mapsize, mapsize];
        gridObject = new GameObject[mapsize, mapsize];
        Mapdesign();
        // Circularly optimized maps
        for (int i = -1; i < 3; i++)
        {
            MapOpt();
        }
        Objectplace();
    }

    // Generate a map that initially contains only walls and false walls
    public void Mapdesign()
    {
        for(int i = 0; i < mapsize; i++)
        {
            for(int j = 0; j < mapsize; j++)
            {
                if (Random.Range(0, 10) < 5)// Randomly generated walls and open spaces
                {
                    grid[i, j] = 1;
                }
                else
                {
                    grid[i, j] = 0;
                }
                // The location of the generated wall is then divided into a solid wall and a fake wall
                if (grid[i, j] == 1)
                {
                        if (Random.Range(0, 10) < 7)
                        {
                        // Placement of the wall in the corresponding position
                        gridObject[i, j] = Instantiate(Wallprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        }
                        else
                        {
                            gridObject[i, j] = Instantiate(fakeWALLprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        }                        
                }
            }
        }
    }

    //Optimizing the initialized walls
    public void MapOpt()
    {
        int[,] newgrid = (int[,])grid.Clone();

        for(int i = 0; i < mapsize; i++)
        {
            for(int j = 0; j < mapsize; j++)
            {
                if(grid[i,j] == 1) 
                {
                    if (nearcubenum(i, j) >= 4 && farcubenum(i,j)>=2)// If this is a wall and there are more than or equal to 4 walls around it, set it as an open space
                    {

                    }
                    else
                    {
                        newgrid[i, j] = 0;
                    }
                }

                else
                {
                    if(nearcubenum(i, j) >= 5)// If this is an open space with more than or equal to 5 walls around it, set it as a wall
                    {
                        newgrid[i, j] = 1;
                    }
                }
            }
        }
        // Vary the map against the values of the points
        mapchange(newgrid);
    }

    // Detects the number of objects around a point within 1 grid range
    public int nearcubenum(int x, int y)
    {
        int num = 0;
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Skip the currently determined point
                if (i==0 && j == 0)
                {
                    continue;
                }
                
                if(x + i < 0 || x + i > mapsize-1 || y+j<0 || y+j>mapsize-1)// Preventing border crossing
                {
                    num++;
                    continue;
                }

                if (grid[x + i, y + j] == 1 || grid[x + i, y + j] == 3 || grid[x + i, y + j] == 4) // Objects are considered to be around when there are walls, bosses or gold around
                {
                    num++;
                }
            }
        }
        return num;
    }

    // Detects the number of objects around a point within 2 grid range
    public int farcubenum(int x, int y)
    {
        int num = 0;
        for (int i = 2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                
                if (i == 0 && j == 0)
                {
                    continue;
                }

                if (x + i < 0 || x + i > mapsize - 1 || y + j < 0 || y + j > mapsize - 1)
                {
                    num++;
                    continue;
                }

                if (grid[x + i, y + j] == 1 || grid[x + i, y + j] ==3 || grid[x + i, y + j] == 4)
                {
                    num++;
                }
            }
        }
        return num;
    }

    // Corresponding changes to the updated map
    public void mapchange(int[,] newgird)
    {
        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                if(newgird[i,j] != grid[i, j])
                {
                    grid[i, j] = newgird[i, j];

                    if (newgird[i,j]==1 )
                    {
                        if (Random.Range(0, 10) < 7) // Guaranteed randomness of solid and fake walls

                        // Placement of the wall in the corresponding position
                        {
                            gridObject[i, j] = Instantiate(Wallprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        }
                        else
                        {
                            gridObject[i, j] = Instantiate(fakeWALLprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        }
                    }
                    else
                    {
                        Destroy(gridObject[i, j]); // Destroying walls
                    }
                }

            }
        }
    }

    // Placement of objects
    public void Objectplace()
    {
        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                if(grid[i, j]==0 && nearcubenum(i,j)==0) // An empty space with no other items within one grid of it
                {
                    if (numboss < 3) // Determine the number of bosses
                    {
                        // Placement of golds in the corresponding position
                        gridObject[i, j] = Instantiate(goldprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        grid[i, j] = 3;
                        if((i + 4 < mapsize) && (j + 4 < mapsize))
                        {
                            i += Random.Range(2, 4); // Make sure the boss and the gold are not too close together
                            j += Random.Range(2, 4); // Make sure the boss and the gold are not too close together
                        }
                        
                        // Placement of bosses in the corresponding position
                        gridObject[i, j] = Instantiate(monsterprefab, transform.position + new Vector3(i, 0, j), Quaternion.identity);
                        grid[i, j] = 4;
                        numgold++;
                        numboss++;
                        i += 20; // Prevent all the gold and bosses from gathering in the same area
                        j += 20; // Prevent all the gold and bosses from gathering in the same area
                        break;
                        
                    }
                }
            }
        }
    }
}
