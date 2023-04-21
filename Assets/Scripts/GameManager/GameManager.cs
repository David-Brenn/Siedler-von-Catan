using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Class is the main class of the game. It is responsible for the game logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    public Player playerOne;
    public Player currentPlayer;


    //make this class to a singelton class
    public static GameManager instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    void Start()
    {
        currentPlayer = playerOne;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// This method creates 5 decks of cards. 
    /// Each deck contains the same amount of cards and the type of cards is not important.
    /// </summary>
    
    
}
