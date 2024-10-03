using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] GameObject northFacingWallPrefab;
    [SerializeField] GameObject eastFacingWallPrefab;
    [SerializeField] float tileSize;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] int seed;
    [SerializeField] bool random;
    MeshCollider meshCollider;
    bool drawnMaze;
    Vector2Int cell = new Vector2Int(0, 0);
    GameObject[,] floors;
    bool[,] done;
    int doneCount;
    List<Vector2Int> route = new List<Vector2Int>();
    bool[,] revNorthFacingWalls;
    GameObject[,] northFacingWallsGO;
    bool[,] revEastFacingWalls;
    GameObject[,] eastFacingWallsGO;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        // Initialize the random seed if random generation is enabled
        if (random) seed = Random.Range(int.MinValue, int.MaxValue);
        Random.InitState(seed);

        // Create the floor tiles
        floors = new GameObject[mazeSize.x, mazeSize.y];
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                floors[x, y] = Instantiate(floorPrefab, transform);
                floors[x, y].transform.position = new Vector3(x * tileSize, 0, y * tileSize);
                floors[x, y].transform.localScale = new Vector3(tileSize, 1, tileSize);
            }
        }

        // Initialize north-facing walls
        revNorthFacingWalls = new bool[mazeSize.x, mazeSize.y + 1];

        // Initialize east-facing walls
        revEastFacingWalls = new bool[mazeSize.x + 1, mazeSize.y];

        done = new bool[mazeSize.x, mazeSize.y]; // Track completed cells

        UpdateMaze(); // Start the maze generation process
    }

    void UpdateMaze()
    {
        // Continue generating the maze until it is fully drawn
        while (!drawnMaze)
        {
            // Check for possible moves from the current cell
            List<Vector2Int> moves = CheckMoves();
            
            if (moves.Count != 0) // If there are valid moves
            {
                // Choose a random move from the available options
                Vector2Int chosenMove = moves[Random.Range(0, moves.Count)];
                
                // Update the wall arrays based on the chosen move
                if (chosenMove == Vector2Int.up)
                {
                    revNorthFacingWalls[cell.x, cell.y + 1] = true; // Remove north wall
                }
                else if (chosenMove == Vector2Int.down)
                {
                    revNorthFacingWalls[cell.x, cell.y] = true; // Remove south wall
                }
                else if (chosenMove == Vector2Int.right)
                {
                    revEastFacingWalls[cell.x + 1, cell.y] = true; // Remove east wall
                }
                else if (chosenMove == Vector2Int.left)
                {
                    revEastFacingWalls[cell.x, cell.y] = true; // Remove west wall
                }

                // Add the current cell to the route and mark it as done
                route.Add(cell);
                done[cell.x, cell.y] = true;
                cell += chosenMove; // Move to the chosen cell
                ++doneCount; // Increment the count of completed cells
            }
            else if (doneCount != done.Length - 1) // If there are no moves left and not all cells are done
            {
                done[cell.x, cell.y] = true; // Mark the current cell as done
                cell = route[route.Count - 1]; // Backtrack to the last cell in the route
                route.RemoveAt(route.Count - 1); // Remove the last cell from the route
            }
            else
            {
                break; // Exit the loop if all cells are done
            }
        }
        Debug.Log("instansiate");
        
        // Instantiate north-facing walls based on the wall array
        northFacingWallsGO = new GameObject[mazeSize.x, mazeSize.y + 1];
        Debug.Log(northFacingWallsGO.Length);
        for (int x = 0; x < revNorthFacingWalls.GetLength(0); ++x)
        {
            for (int y = 0; y < revNorthFacingWalls.GetLength(1); ++y)
            {
                if (!revNorthFacingWalls[x, y]) // If a north-facing wall exists
                {
                    northFacingWallsGO[x, y] = Instantiate(northFacingWallPrefab, transform); // Create wall GameObject
                    northFacingWallsGO[x, y].transform.position = new Vector3(x * tileSize, 0, y * tileSize); // Set position
                    northFacingWallsGO[x, y].transform.localScale = new Vector3(tileSize, tileSize, tileSize); // Set scale
                }
            }
        }

        // Instantiate east-facing walls based on the wall array
        eastFacingWallsGO = new GameObject[mazeSize.x + 1, mazeSize.y];
        for (int x = 0; x < revEastFacingWalls.GetLength(0); ++x)
        {
            for (int y = 0; y < revEastFacingWalls.GetLength(1); ++y)
            {
                if (!revEastFacingWalls[x, y]) // If an east-facing wall exists
                {
                    eastFacingWallsGO[x, y] = Instantiate(eastFacingWallPrefab, transform); // Create wall GameObject
                    eastFacingWallsGO[x, y].transform.position = new Vector3(x * tileSize, 0, y * tileSize); // Set position
                    eastFacingWallsGO[x, y].transform.localScale = new Vector3(tileSize, tileSize, tileSize); // Set scale
                    eastFacingWallsGO[x, y].name = "east" + x + "," + y; // Name the GameObject
                }
            }
        }
        done = null;
        revEastFacingWalls = null;
        revNorthFacingWalls = null;
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        Mesh mesh = new Mesh(); mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.CombineMeshes(combine);
        meshCollider.sharedMesh = mesh;
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Method to check possible moves from the current cell
    List<Vector2Int> CheckMoves()
    {
        List<Vector2Int> toReturn = new List<Vector2Int>(); // List to hold valid moves

        // Check if moving up is within limits and the cell above is not done
        if (InLimits(done, cell + Vector2Int.up))
        {
            if (!done[cell.x, cell.y + 1]) toReturn.Add(Vector2Int.up);
        }
        
        // Check if moving right is within limits and the cell to the right is not done
        if (InLimits(done, cell + Vector2Int.right))
        {
            if (!done[cell.x + 1, cell.y]) toReturn.Add(Vector2Int.right);
        }
        
        // Check if moving down is within limits and the cell below is not done
        if (InLimits(done, cell + Vector2Int.down))
        {
            if (!done[cell.x, cell.y - 1]) toReturn.Add(Vector2Int.down);
        }
        
        // Check if moving left is within limits and the cell to the left is not done
        if (InLimits(done, cell + Vector2Int.left))
        {
            if (!done[cell.x - 1, cell.y]) toReturn.Add(Vector2Int.left);
        }

        return toReturn; // Return the list of valid moves
    }

    // Method to check if a point is within the limits of the array
    private bool InLimits(bool[,] toTest, Vector2Int point)
    {
        // Check if the point is within the bounds of the array
        return point.x >= 0 && point.y >= 0 && point.x < toTest.GetLength(0) && point.y < toTest.GetLength(1);
    }
}
