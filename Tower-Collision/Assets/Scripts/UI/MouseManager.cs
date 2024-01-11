    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    public LayerMask tileLayer;
    public static Vector3 pos;
    public static MouseManager instance;

    void Awake(){
        if (instance != null) { 
            Destroy(this);
            return;
        }
        instance = this;
    }

    public Vector3 getMouseLocation()
    {
        Ray currPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cast;

        if(Physics.Raycast(currPos,out cast,100f,tileLayer)){
            return cast.point;
        }
        return Vector3.zero;
    }
    
    public GameObject getMouseHit()
    {
        if(EventSystem.current.IsPointerOverGameObject()){
            return null;
        }
        Ray currPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cast;

        if(Physics.Raycast(currPos,out cast,100f,tileLayer)){
            return cast.collider.gameObject;
        }
        return null;
    }
}
