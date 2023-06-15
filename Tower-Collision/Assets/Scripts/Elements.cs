using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementsInteractions{
    public static float steamMultipler = 0.5f;
    public static float steamTimer = 5f;
    
    public static Dictionary<Elements,Color> element_color = new Dictionary<Elements,Color>{
        {Elements.Fire,Color.red},
        {Elements.Water,Color.blue},
        {Elements.Grass,Color.green},
        {Elements.Normal,Color.grey},
        {Elements.Dark,Color.black},
        {Elements.Light,Color.white}
    };    

    public static Reaction CalculateReation(Elements applied1,Elements applied2){
        if(applied1 == Elements.Fire && applied2 == Elements.Water || applied1 == Elements.Water && applied2 == Elements.Fire){
            return Reaction.Steam;
        }
        if(applied1 == Elements.Fire && applied2 == Elements.Grass || applied1 == Elements.Grass && applied2 == Elements.Fire){
            return Reaction.Burn;
        }
        if(applied1 == Elements.Grass && applied2 == Elements.Water || applied1 == Elements.Water && applied2 == Elements.Grass){
            return Reaction.Root;
        }
        if(applied1 == Elements.Light && applied2 == Elements.Dark){
            return Reaction.Eclipse;
        }
        if(applied1 == Elements.Dark && applied2 == Elements.Light){
            return Reaction.Illuminate;
        }
        return Reaction.Null;
    }

}

public enum Elements{
    Fire,Water,Grass,Normal,Dark,Light,Null
}
public enum Reaction{
    Steam,Root,Burn,Illuminate,Eclipse,Null
}




