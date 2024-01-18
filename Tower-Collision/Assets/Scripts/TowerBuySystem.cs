using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerBuySystem : MonoBehaviour
{
    [SerializeField] GameObject towerprefab;
    [SerializeField] HealthCanvas healthCanvas;
    [SerializeField] TMP_Text text;
    public LinkedList<TowerSystem> towersActive = new LinkedList<TowerSystem>();
    int money = 5;
    
    void Update(){
        LinkedListNode<TowerSystem> head = towersActive.First;
        LinkedListNode<TowerSystem> placeholder;
        while (head != null)
        {
            placeholder = head.Next;
            if (head.Value == null)
            {
                towersActive.Remove(head);
            }
            head = placeholder;
        }
    }

    public void KillEnemy(){
        money += 1;
        text.SetText(money.ToString());
    }

    public void PlaceTower(PlaceableTile ptile){
        if (ptile != null && ptile.place == null && money > 0){
            GameObject s = Instantiate(towerprefab, ptile.getSpawnPoint().position, Quaternion.identity);
            s.transform.parent = ptile.transform;
            TowerSystem system = s.GetComponent<TowerSystem>();
            system.StartSystem();
            ptile.place = system;
            towersActive.AddLast(system);
            healthCanvas.AddHealthBar(system);
            money -= 1;
            text.SetText(money.ToString());
        }
    }


}
