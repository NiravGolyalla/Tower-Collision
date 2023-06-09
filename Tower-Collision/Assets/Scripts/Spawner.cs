using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject prefab;
    // [SerializeField]Transform end;
    [SerializeField]float rate = 2f;
    
    void Start(){
        StartCoroutine(SapwnRate());
    }
    public IEnumerator SapwnRate(){
        while(true){
            yield return new WaitForSeconds(rate);
            GameObject f = Instantiate(prefab,EnemyPathManager.path[0].position,Quaternion.identity);
            // f.GetComponent<Enemy>().Setup(end);
        }
    }
}