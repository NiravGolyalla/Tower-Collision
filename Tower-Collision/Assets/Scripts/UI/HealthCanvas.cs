using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField]ObjUI Info;
    private Camera cam;
    public LinkedList<Tuple<StateMachine,ObjUI>> allUIBars = new LinkedList<Tuple<StateMachine,ObjUI>>();
    
    void Start(){
        cam = Camera.main;
    } 

    void Update(){
        LinkedListNode<Tuple<StateMachine,ObjUI>> head = allUIBars.First;
        LinkedListNode<Tuple<StateMachine,ObjUI>> placeholder;
        while(head != null){
            placeholder = head.Next;
            if(head.Value.Item1 == null){
                Destroy(head.Value.Item2.gameObject);
                allUIBars.Remove(head);
            } else{
                head.Value.Item2.transform.position = cam.WorldToScreenPoint(head.Value.Item1.uiPlacement.position);
            }
            
            head = placeholder;
        }
    }

    public void AddHealthBar(StateMachine m){
        ObjUI x = Instantiate(Info,transform);
        x.Bind(m.healthSubSystem);
        Tuple<StateMachine,ObjUI> things = new Tuple<StateMachine,ObjUI>(m,x);
        allUIBars.AddLast(things);
    }
}
