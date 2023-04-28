using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionScripts;

//make this class creatable in the unity editor
[CreateAssetMenu(fileName = "BuildingCard", menuName = "Card/BuildingCard")]
public class BuildingCard : Card
{
    // Start is called before the first frame update
    public Cost cost;
    public bool needsCity;

    public int victoryPoints;

    public int tradePoints;
    
    public Action action;

    public override void playCard()
    {
        addBuildingToPlayer();
        action.PlayCard();
    }

    public void addBuildingToPlayer(){
        // Get Position to place the Building

        // Instantiate Building prefab at postiton

        
    }

}   
