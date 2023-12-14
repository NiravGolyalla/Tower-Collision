using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementsInteractions{
    public static Dictionary<Elements,Color> element_color = new Dictionary<Elements,Color>{
        {Elements.Red,Color.red},
        {Elements.Blue,Color.blue},
        {Elements.Green,Color.green},
    };    

    public static Reaction CalculateReation(Elements applied1,Elements applied2){
        if(applied1 == Elements.Red && applied2 == Elements.Blue || applied1 == Elements.Blue && applied2 == Elements.Red){
            return Reaction.Magenta;
        }
        if(applied1 == Elements.Red && applied2 == Elements.Green || applied1 == Elements.Green && applied2 == Elements.Red){
            return Reaction.Yellow;
        }
        if(applied1 == Elements.Green && applied2 == Elements.Blue || applied1 == Elements.Blue && applied2 == Elements.Green){
            return Reaction.Cyan;
        }
        return Reaction.Null;
    }

}

public enum Elements{
    Red,Blue,Green,Null
}
public enum Reaction{
    Cyan,Magenta,Yellow,Null
}




