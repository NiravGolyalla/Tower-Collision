using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject prefab;
    // [SerializeField]Transform end;
    [SerializeField]float rate = 10f;
    
    public static List<Transform> path = new List<Transform>();

    void Start(){
        StartCoroutine(SapwnRate());
    }
    public IEnumerator SapwnRate(){
        while(true){
            yield return new WaitForSeconds(rate);
            GameObject f = Instantiate(prefab,path[0].position,Quaternion.identity);
            f.GetComponent<EnemySystem>().path = path;
            f.GetComponent<EnemySystem>().target = path[0];
        }
    }
}
