using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BlockerSubSystem : MonoBehaviour
{
    private StateMachine mainSystem;
    [SerializeField]List<StateMachine> blocking = new List<StateMachine>();
    public float blockAmount {get; private set;}    
    
    void Awake(){
        mainSystem = GetComponent<StateMachine>();
    }

    public void GetStats(){
        if(mainSystem.GetType().Equals(typeof(TowerSystem))){
            blockAmount = ((TowerStatsLoader)mainSystem.statLoader).GetBlockAmount();    
        }
    }
    void Update(){
        if(blocking.Contains(null)){
            blocking.RemoveAll(ItemCanBeNullAttribute => ItemCanBeNullAttribute == null);
        }
    }

    public bool checkBlockStatus(){
        return blockAmount > blocking.Count;
    }

    public void BlockUnit(StateMachine target){
        if(blocking.Contains(target)){
            blocking.Add(target);
        }
        
    }
}
