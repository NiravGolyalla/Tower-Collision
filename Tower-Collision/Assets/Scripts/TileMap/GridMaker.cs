using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridMaker : MonoBehaviour
{
    [SerializeField]Tile placeable,road,waypoint;
    [SerializeField]float width,height,b_width,b_height;
    [SerializeField]string layout = "00000000_00000000_21111112_00000000_00000000";
    
    Dictionary<Vector2,Tile> tiles = new Dictionary<Vector2, Tile>();
    void Start(){
        GenerateGrid();
    }

    void DestroyGrid(){
        foreach (var tile in tiles.Values){
            if(tile != null){
                StartCoroutine(Destroy(tile.gameObject));
            }
            
        }
        tiles.Clear();
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    void Update(){
        if(Input.GetMouseButton(0)){

        }
    }

    void GenerateGrid(){
        DestroyGrid();
        int x = 0;
        int y = 0;
        foreach(char i in layout){
            Tile newtile;
            print(i);
            if(i == '0'){
                newtile = Instantiate(placeable,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity);    
            } 
            else if(i == '1'){
                newtile = Instantiate(road,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity);    
            }
            else if(i == '2'){
                newtile = Instantiate(waypoint,new Vector3(x*b_width,-0.5f,y*b_height),Quaternion.identity);    
            } else{
                y += 1;
                x = 0;
                continue;
            }
            newtile.name = $"Tile{x}{y}";                
            newtile.transform.localScale = new Vector3(b_width,1,b_height);
            tiles[new Vector2(x,y)] = newtile;
            x++;
        }
        Camera.main.transform.position = new Vector3((width*b_width)/2-0.5f*b_width,Camera.main.transform.position.y,Camera.main.transform.position.z); 
    }

    public Tile GetTile(Vector2 pos){return (tiles.TryGetValue(pos, out var tile)) ? tile:null;}
}
