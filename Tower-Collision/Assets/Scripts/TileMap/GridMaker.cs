using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

// [ExecuteInEditMode]
public class GridMaker : MonoBehaviour
{
    [SerializeField] Tile placeable, road, waypoint;
    [SerializeField] float b_width, b_height;
    char[,] towerDefenseMap = new char[20, 20]{
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H'},
    {'H', 'E', '_', '_', '_', '_', '_', 'W', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', 'W', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'E', '_', '_', '_', '_', '_', 'W', '_', 'W', '_', '_', '_', '_', '_', '_', '_', '_', 'W', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'W', '_', '_', 'W', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'W', '_', 'W', '_', '_', '_', 'W', '_', '_', 'W', 'H'},
    {'H', 'H', 'H', 'H', 'U', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'W', '_', '_', '_', '_', '_', '_', 'W', '_', '_', '_', '_', '_', '_', 'W', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', '_', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'U', 'H'},
    {'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H', 'H'}};

    public List<(int, int)> starts = new List<(int, int)>();
    public List<(int, int)> ends = new List<(int, int)>();
    public Dictionary<(int, int), Tile> tiles{get;private set;}
    public Dictionary<(int,int), List<(int,int)>> AdjacencyMatrix = new Dictionary<(int, int), List<(int, int)>>();
    [SerializeField] GameObject towerprefab;
    Color original_color;
    [SerializeField] Color highlight;
    GameObject lastHighlightedObject = null;
    public LinkedList<TowerSystem> towersActive = new LinkedList<TowerSystem>();
    private NavMeshSurface surface;
    [SerializeField] HealthCanvas healthCanvas;

    void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        tiles = new Dictionary<(int, int), Tile>();
        GenerateGrid();
        GenerateAdjacencyMatrix();
    }

    void Start()
    {
        surface.BuildNavMesh();
    }

    void DestroyGrid()
    {
        foreach (var tile in tiles.Values)
        {
            if (tile != null)
            {
                Destroy(tile.gameObject);
            }
        }
        tiles.Clear();
        starts.Clear();
        ends.Clear();
    }

    void Update()
    {
        LinkedListNode<TowerSystem> head = towersActive.First;
        LinkedListNode<TowerSystem> placeholder;
        while (head != null)
        {
            placeholder = head.Next;
            if (head.Value == null)
            {
                towersActive.Remove(head);
            }
            head = placeholder;
        }
        GameObject curr = MouseManager.instance.getMouseHit();
        if (curr == null)
        {
            if (lastHighlightedObject != null)
            {
                lastHighlightedObject.GetComponent<Renderer>().material.color = original_color;
                lastHighlightedObject = null;
            }
            return;
        }


        if (curr != lastHighlightedObject)
        {
            if (lastHighlightedObject != null)
            {
                lastHighlightedObject.GetComponent<Renderer>().material.color = original_color;
            }
            original_color = curr.GetComponent<Renderer>().material.color;
            curr.GetComponent<Renderer>().material.color = (curr.GetComponent<Renderer>().material.color + Color.white) / 2;
            lastHighlightedObject = curr;
        }

        if (Input.GetMouseButton(0) && curr != null)
        {
            PlaceableTile ptile = curr.GetComponent<PlaceableTile>();
            if (ptile != null && ptile.place == null)
            {
                GameObject s = Instantiate(towerprefab, ptile.getSpawnPoint().position, Quaternion.identity);
                s.transform.parent = ptile.transform;
                s.GetComponent<TowerSystem>().StartSystem();
                ptile.place = s.GetComponent<TowerSystem>();
                towersActive.AddLast(s.GetComponent<TowerSystem>());
                healthCanvas.AddHealthBar(s.GetComponent<TowerSystem>());
                // surface.BuildNavMesh();
            }
        }
    }
    void GenerateGrid()
    {
        DestroyGrid();
        for (int i = 0; i < towerDefenseMap.GetLength(0); i++)
        {
            for (int j = 0; j < towerDefenseMap.GetLength(1); j++)
            {
                char tile = towerDefenseMap[i, j];
                Tile newtile;
                if (tile == 'H')
                {
                    newtile = Instantiate(placeable, new Vector3(j * b_width, -0.5f, -i * b_height), Quaternion.identity, transform);
                }
                else if (tile == '_')
                {
                    newtile = Instantiate(road, new Vector3(j * b_width, -0.5f, -i * b_height), Quaternion.identity, transform);
                }
                else if (tile == 'E')
                {
                    newtile = Instantiate(waypoint, new Vector3(j * b_width, -0.5f, -i * b_height), Quaternion.identity, transform);
                    starts.Add((i, j));
                }
                else if (tile == 'U')
                {
                    newtile = Instantiate(waypoint, new Vector3(j * b_width, -0.5f, -i * b_height), Quaternion.identity, transform);
                    ends.Add((i, j));
                }
                else
                {
                    newtile = Instantiate(waypoint, new Vector3(j * b_width, -0.5f, -i * b_height), Quaternion.identity, transform);
                }
                newtile.name = $"Tile{j}{i}";
                newtile.transform.localScale = new Vector3(b_width, 1, b_height);
                tiles[(i,j)] = newtile;
            }
        }
        Camera.main.transform.position = new Vector3((towerDefenseMap.GetLength(0) * b_width) / 2 - 0.5f * b_width, 50f, -1f * ((towerDefenseMap.GetLength(0) * b_width) - 0.5f * b_width));
    }

    private void GenerateAdjacencyMatrix()
    {
        Queue<(int, int)> tovisit = new Queue<(int, int)>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>
        {
            (-1, -1)
        };
        foreach((int,int)pos in starts){
            tovisit.Enqueue(pos);
        }
        while (tovisit.Count > 0)
        {
            (int, int) curr = tovisit.Dequeue();
            if (visited.Contains(curr))
            {
                continue;
            }
            visited.Add(curr);
            List<(int, int)> neighbors = new List<(int, int)>{
                LocatePoint((curr.Item1 - 1, curr.Item2), "north"),
                LocatePoint((curr.Item1 + 1, curr.Item2), "south"),
                LocatePoint((curr.Item1, curr.Item2 + 1), "east"),
                LocatePoint((curr.Item1, curr.Item2 - 1), "west")
            };
            foreach((int,int) n in neighbors){
                if(n.Item1 == -1 || n.Item2 == -1){
                    continue;
                }
                if(!AdjacencyMatrix.ContainsKey(curr)){
                    AdjacencyMatrix[curr] = new List<(int, int)>();
                }
                AdjacencyMatrix[curr].Add(n);
                tovisit.Enqueue(n);
            }
        }
        // foreach (var entry in AdjacencyMatrix)
        // {
        //     var key = entry.Key;
        //     Debug.Log($"Point: {key.Item1}, {key.Item2}");

            
        //     var neighbors = entry.Value;

        //     foreach (var neighbor in neighbors)
        //     {
        //         Debug.Log($"  Neighbor: {neighbor.Item1}, {neighbor.Item2}");
        //     }
        // }
    }

    private (int, int) LocatePoint((int, int) loc, string direction)
    {
        if (0 <= loc.Item1 && loc.Item1 < towerDefenseMap.GetLength(0) && 0 <= loc.Item2 && loc.Item2 < towerDefenseMap.GetLength(1))
        {
            if (towerDefenseMap[loc.Item1, loc.Item2] == 'E' || towerDefenseMap[loc.Item1, loc.Item2] == 'W' || towerDefenseMap[loc.Item1, loc.Item2] == 'U')
            {
                return (loc.Item1, loc.Item2);
            }
            if (towerDefenseMap[loc.Item1, loc.Item2] == '_')
            {
                if (direction == "north")
                {
                    return LocatePoint((loc.Item1 - 1, loc.Item2), direction);
                }
                if (direction == "south")
                {
                    return LocatePoint((loc.Item1 + 1, loc.Item2), direction);
                }
                if (direction == "east")
                {
                    return LocatePoint((loc.Item1, loc.Item2 + 1), direction);
                }
                if (direction == "west")
                {
                    return LocatePoint((loc.Item1, loc.Item2 - 1), direction);
                }
            }
        }
        return (-1, -1);
    }


    public Tile GetTile((int,int) pos) { return (tiles.TryGetValue(pos, out var tile)) ? tile : null; }
}
