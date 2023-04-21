using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this class creatable in the unity editor
[CreateAssetMenu(fileName = "KnightCard", menuName = "Card/KnightCard")]
public class KnightCard : BuildingCard
{
    public int strength;
    public int tournamentPoints;



    public override void playCard()
    {
    
        action.PlayCard();
    }

    public void addKnightToPlayer(){


        // Get Position to place the Knight

        // Instantiate Knight prefab at postiton
    }
    public void removeKnightFromPlayer(){
        // Destroy the Knight Prefab 

        // Remove the Stats from the Player

    }
}


