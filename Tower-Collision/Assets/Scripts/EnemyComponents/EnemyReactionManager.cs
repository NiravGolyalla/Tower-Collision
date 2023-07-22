using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactionManager : ReactionManager
{
    protected override IEnumerator Steam(){
        // healthModifer += ElementsInteractions.steamMultipler;
        yield return new WaitForSeconds(ElementsInteractions.steamTimer);
        // healthModifer -= ElementsInteractions.steamMultipler;
    }
    protected override IEnumerator Burn(){
        // healthModifer += ElementsInteractions.steamMultipler;
        yield return new WaitForSeconds(ElementsInteractions.steamTimer);
        // healthModifer -= ElementsInteractions.steamMultipler;
    }
    protected override IEnumerator Root(){
        // healthModifer += ElementsInteractions.steamMultipler;
        yield return new WaitForSeconds(ElementsInteractions.steamTimer);
        // healthModifer -= ElementsInteractions.steamMultipler;
    }
    protected override IEnumerator Illuminate(){
        yield return null;
    }
    protected override IEnumerator Eclipse(){
        yield return null;
    }
}
