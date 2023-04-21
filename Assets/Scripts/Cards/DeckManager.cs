using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardCount {
    public Card card;
    public int count;
}
public class DeckManager : MonoBehaviour
{   
    public GameObject deckPrefab;
    private static DeckManager instance;

    public static DeckManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<DeckManager>();
                if (instance == null) {
                    GameObject go = new GameObject("DeckManager");
                    instance = go.AddComponent<DeckManager>();
                }
            }
            return instance;
        }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    //A list of all cards and the corresponding amount of cards
    [Header("List of cards and amount of cards")]
    public List<CardCount> ActionCards = new List<CardCount>();

    public List<CardCount> KnightCards = new List<CardCount>();

    public List<CardCount> BuildingCards = new List<CardCount>();
    
    public List<Deck> decks = new List<Deck>();
    // Start is called before the first frame update
    void Start()
    {
        createDecks();
    }

    // Update is called once per frame
   

    /// <summary>
    /// Creats 5 decks of cards and each deck contains the same amount of cards
    /// </summary>
    public void createDecks(){
        //Create a deck with all cards.
        Deck allCards = new GameObject().AddComponent<Deck>();
        allCards.cards = new List<Card>();
        //Add all cards from the three lists to the deck allCards
        addCardCountListToDeck(ActionCards, allCards);
        addCardCountListToDeck(KnightCards, allCards);
        addCardCountListToDeck(BuildingCards, allCards);

        //Shuffle the deck
        allCards.shuffleCards();

        //Add a list to each Deck in the list decks
        foreach (Deck deck in decks)
        {
            deck.cards = new List<Card>();
        }
        //For each card in the allCards Deck add the card to a deck starting with the first deck in the list decks and then the second and so on.
        for (int i = 0; i < allCards.cards.Count; i++)
        {
            decks[i % 5].cards.Add(allCards.cards[i]);
        }
        Destroy(allCards.gameObject);
    }


    /// <summary>
    /// Adds all cards from the list cardCountList to the deck with the corresponding amount of cards.
    /// </summary>
    /// <param name="cardCountList"></param>
    /// <param name="deck"></param>
    private void addCardCountListToDeck(List<CardCount> cardCountList, Deck deck){
        foreach (CardCount cardCount in cardCountList)
        {
            for (int i = 0; i < cardCount.count; i++)
            {
                deck.cards.Add(cardCount.card);
            }
        }
    }



}
