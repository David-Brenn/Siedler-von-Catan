using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this class creatable in the unity editor
[CreateAssetMenu(fileName = "KnightCard", menuName = "Card/KnightCard")]
/// <summary>
/// Scriptable object for KnightCards. Is a BuildingCard but adds knights attributes and add Knight function
/// </summary>
public class KnightCard : BuildingCard
{
    public int strength;
    public int tournamentPoints;



    public override void playCard()
    {
        //Player needs to 
        action.PlayCard();
    }

    //TODO: 

    public void addKnightToPlayer(){


        // Get Position to place the Knight

        // Instantiate Knight prefab at postiton
    }

    // TODO: 
    public void removeKnightFromPlayer(){
        // Destroy the Knight Prefab 

        // Remove the Stats from the Player

    }
}


