using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]List<GameObject> prefab;
    [SerializeField]int index = 0;
    [SerializeField] float delay = 5f;
    [SerializeField] float wavedelay = 30f;
    [SerializeField] float spawnNumber = 1f;
    [SerializeField] HealthCanvas healthCanvas;
    [SerializeField] GridMaker grid;
    [SerializeField] TowerBuySystem buy;
    List<string> waves = new List<string>{
    "001 001",
    "0011 0011 0011",
    "1012 012 0102",
    "00100 00102 00102",
    "10102 10012 00022",
    "11122 00022 00122",
    "20010 01001 20013",
    "30101 30100 31001",
    "30012 30102 32001"
    };

    void Start()
    {
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
        yield return new WaitForSeconds(5f);
        while(index < waves.Count){
            List<Transform>path = GeneratePaths();
            foreach(char c in waves[index]){
                if(c == ' '){
                    path = GeneratePaths();
                } else{
                    GameObject f = Instantiate(prefab[c-'0'], path[0].position, Quaternion.identity,gameObject.transform);
                    f.GetComponent<AIMovementSubSystem>().setPath(path);
                    healthCanvas.AddHealthBar(f.GetComponent<EnemySystem>());
                    f.GetComponent<EnemySystem>().onDeath += buy.KillEnemy;
                }
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(wavedelay);
            index += 1;
        }
    }
}
