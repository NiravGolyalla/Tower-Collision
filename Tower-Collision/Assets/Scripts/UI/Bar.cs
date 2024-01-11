using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    [SerializeField] private Image barSprite;
    [SerializeField] private Color barColor;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;
    private Camera cam;

    void Start(){
        cam = Camera.main;
        barSprite.color = barColor;
    }

    public void UpdateBar(float value){
        target = value;
    }

    void Update() {
        // transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        barSprite.fillAmount = Mathf.MoveTowards(barSprite.fillAmount,target,reduceSpeed*Time.deltaTime);                            
    }
}
