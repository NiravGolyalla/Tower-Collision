using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using Unity.VisualScripting;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] EnemyStats[] scriptableObjects;
    [SerializeField] float rate = 5f;
    [SerializeField] HealthCanvas healthCanvas;
    [SerializeField] GridMaker grid;

    void Start()
    {
        // GeneratePaths();
        StartCoroutine(SpawnRate());
    }

    List<Transform> GeneratePaths()
    {
        List<(int, int)> things = new List<(int, int)>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        (int, int) start = grid.starts[Random.Range(0, grid.starts.Count)];
        bool other = DFS(start, things, visited);
        if (other)
        {
            things.Reverse();
            List<Transform> path = new List<Transform>();
            foreach ((int, int) y in things)
            {
                path.Add(((WayPointTile)grid.tiles[y]).walkPoint);
            }
            return path;
        }
        return null;
    }


    bool DFS((int, int) node, List<(int, int)> path, HashSet<(int, int)> visited)
    {
        if (grid.ends.Contains(node))
        {
            path.Add(node);
            visited.Add(node);
            return true;
        }
        if (visited.Contains(node))
        {
            return false;
        }
        visited.Add(node);

        if (grid.AdjacencyMatrix.ContainsKey(node))
        {
            List<(int, int)> values = new List<(int, int)>(grid.AdjacencyMatrix[node]);
            bool foundPath = false;
            while (values.Count > 0)
            {
                int randomIndex = Random.Range(0, values.Count);
                foundPath = DFS(values[randomIndex], path, visited);
                if (foundPath)
                {
                    path.Add(node);
                    return true;
                }
                values.RemoveAt(randomIndex);
            }
            return false;

        }
        return false;
    }


    public IEnumerator SpawnRate()
    {

        while (true)
        {
            yield return new WaitForSeconds(rate);
            List<Transform>path = GeneratePaths();
            for(int j = 0; j < 2; j++){
                GameObject f = Instantiate(prefab, path[0].position, Quaternion.identity,gameObject.transform);
                EnemyStats stats = scriptableObjects[Random.Range(0, scriptableObjects.Length)];
                f.GetComponent<EnemyStatsLoader>().SetStats(stats);
                f.GetComponent<EnemySystem>().StartSystem();
                f.GetComponent<AIMovementSubSystem>().setPath(path);
                healthCanvas.AddHealthBar(f.GetComponent<EnemySystem>());    
            }
            

        }
    }
}
