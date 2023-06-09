using System.Collections;
using System.Collections.Generic;

public struct Damage {
    public float amount;
    public Elements element;

    public Damage(float _amount,Elements _element){
        amount = _amount;
        element = _element;
    }
}

