using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Shooter : MonoBehaviour
{
    [SerializeField]private float range;
    [SerializeField]private float fireRate = 1f;
    private Elements element;
    public GameObject projectile;
    public Transform firingPoint;
    [SerializeField]private Transform curr_target;
    [SerializeField]private string target_tag;
    private List<Transform> targets = new List<Transform>(); 
    
    void Start(){
        StartCoroutine(FireRate());
    }

    public void setStats(float _range,float _fireRate,Elements _element){
        range = _range;
        fireRate = _fireRate;
        element = _element;
        GetComponent<SphereCollider>().radius = range;
    }

    void Update(){
        AssignTarget();
    }

    public IEnumerator FireRate(){
        while(true){
            yield return new WaitUntil(() => curr_target != null);
            GameObject f = Instantiate(projectile,firingPoint.position,Quaternion.identity);
            f.GetComponent<Projectile>().Setup(curr_target,element);
            yield return new WaitForSeconds(1f/fireRate); 
        }
    }


    private void AssignTarget(){
        int i = 0;
        int index = 0;
        float min = float.MaxValue;
        while(i < targets.Count){
            if(targets[i] != null){
                float dist = Vector3.Distance(targets[i].position,transform.position);
                min = Mathf.Min(dist,min);
                if(dist == min){
                    index= i;
                }
                i += 1;
            } else{
                targets.RemoveAt(i);
            }
        }
        if(targets.Count != 0){
            curr_target = targets[index];
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag(target_tag)){
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag(target_tag)){
            if(targets.Contains(other.transform)){
                targets.Remove(other.transform);
            }
        }
    
    }
}
