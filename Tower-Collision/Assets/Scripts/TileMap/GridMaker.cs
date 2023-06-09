using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class GridMaker : MonoBehaviour
{
    [SerializeField]Tile placeable,road,waypoint;
    [SerializeField]float width,height,b_width,b_height;
    [SerializeField]string layout = "@@@@@@@@_@@2aa3@@_0a1@@4a5_@@@@@@@@_@@@@@@@@";
    Dictionary<Vector3,Tile> tiles;
    [SerializeField]GameObject towerprefab;

    Color original_color;
    [SerializeField]Color highlight;
    GameObject lastHighlightedObject = null;
    


    void Start(){
        tiles = new Dictionary<Vector3, Tile>();
        GenerateGrid();
    }

    void DestroyGrid(){
        foreach (var tile in tiles.Values){
            if(tile != null){
                Destroy(tile.gameObject);
            }
        }
        tiles.Clear();
    }

    void Update(){
        GameObject curr = MouseManager.instance.getMouseHit();
        if(curr == null){
            if(lastHighlightedObject != null){
                lastHighlightedObject.GetComponent<Renderer>().material.color = original_color;
                lastHighlightedObject = null;
            }
            return;
        }
        
        
        if(curr != lastHighlightedObject){
            if(lastHighlightedObject != null){
                lastHighlightedObject.GetComponent<Renderer>().material.color = original_color;
            }
            original_color = curr.GetComponent<Renderer>().material.color;
            curr.GetComponent<Renderer>().material.color = (curr.GetComponent<Renderer>().material.color+Color.white)/2;
            lastHighlightedObject = curr;
        }
        
        if(Input.GetMouseButton(0) && curr != null){
            PlaceableTile ptile = curr.GetComponent<PlaceableTile>();
            if(ptile != null && !ptile.place){
                GameObject s = Instantiate(towerprefab,ptile.getSpawnPoint().position,Quaternion.identity);    
                s.transform.parent = ptile.transform;
                ptile.place = true;
            }   
        }
    }
    void GenerateGrid(){
        DestroyGrid();
        int x = 0;
        int y = 0;
        foreach(char i in layout){
            Tile newtile;
            // print(i);
            if(i == '@'){
                newtile = Instantiate(placeable,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity,transform);    
            } 
            else if(i == '_'){
                y += 1;
                x = 0;
                continue;
            }
            else if(i == 'a'){
                newtile = Instantiate(road,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity,transform);        
            } else{
                newtile = Instantiate(waypoint,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity,transform);
                newtile.GetComponent<WayPointTile>().index = (int)i-48;
            }
            newtile.name = $"Tile{x}{y}";                
            newtile.transform.localScale = new Vector3(b_width,1,b_height);
            tiles[new Vector3(x*b_width,-0.5f,y*b_height)] = newtile;
            x++;
        }
        Camera.main.transform.position = new Vector3((width*b_width)/2-0.5f*b_width,Camera.main.transform.position.y,Camera.main.transform.position.z); 
    }

    public Tile GetTile(Vector3 pos){return (tiles.TryGetValue(pos, out var tile)) ? tile:null;}
}
