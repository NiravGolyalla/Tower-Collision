using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerSubSystem : MonoBehaviour
{
    private StateMachine mainSystem;
    List<StateMachine> blocking = new List<StateMachine>();
    public float blockAmount {get; private set;}    
    
    void Awake(){
        mainSystem = GetComponent<StateMachine>();
    }

    void Start(){
        if(mainSystem.GetType().Equals(typeof(TowerSystem))){
            blockAmount = ((TowerStatsLoader)mainSystem.statLoader).GetBlockAmount();    
        }
    }
    void Update(){
        blocking.RemoveAll(item => item==null);
    }

    public bool checkBlockStatus(){
        return blockAmount > blocking.Count;
    }

    public void BlockUnit(StateMachine target){
        blocking.Add(target);
    }
}
