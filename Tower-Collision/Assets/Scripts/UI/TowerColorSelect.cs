using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerColorSelect : MonoBehaviour
{
    public static DamageTypes clickedValue = DamageTypes.Red;
    public Button button1;
    public Button button2;
    public Button button3;
    private int currentButtonIndex = 0;
    private List<(DamageTypes,Button)> indexValues;


    void Start()
    {
        indexValues = new List<(DamageTypes,Button)>{(DamageTypes.Red,button1),(DamageTypes.Blue,button2),(DamageTypes.Green,button3)};
        button1.onClick.AddListener(() => OnButtonClick(DamageTypes.Red,button1));
        button2.onClick.AddListener(() => OnButtonClick(DamageTypes.Blue,button2));
        button3.onClick.AddListener(() => OnButtonClick(DamageTypes.Green,button3));
    }

    void OnButtonClick(DamageTypes value,Button b)
    {
        clickedValue = value;
        button1.image.color = Color.grey;
        button2.image.color = Color.grey;
        button3.image.color = Color.grey;
        b.image.color = Color.white + Color.grey;
        if(clickedValue == DamageTypes.Red){
            currentButtonIndex = 0;
        }
        if(clickedValue == DamageTypes.Blue){
            currentButtonIndex = 1;
        }
        if(clickedValue == DamageTypes.Green){
            currentButtonIndex = 2;
        }
    }

    void Update()
    {
        // Check for 'A' and 'D' key presses to toggle between buttons
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentButtonIndex = (currentButtonIndex - 1 + 3) % 3;
            OnButtonClick(indexValues[currentButtonIndex].Item1,indexValues[currentButtonIndex].Item2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentButtonIndex = (currentButtonIndex + 1) % 3;
            OnButtonClick(indexValues[currentButtonIndex].Item1,indexValues[currentButtonIndex].Item2);
        }
    }

}
