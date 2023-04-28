using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck : MonoBehaviour
{
    public GameObject handCardObjectPrefab;
    public List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject drawCard(){
        Card card = cards[0];
        cards.RemoveAt(0);
        GameObject handcardObejct = Instantiate(handCardObjectPrefab);
        handcardObejct.GetComponent<HandCardObject>().card = card;
        return handcardObejct;
    }

    public GameObject choseCard(int index){
        Card card = cards[index];
        cards.RemoveAt(index);
        shuffleCards();
        return Instantiate(handCardObjectPrefab);
    }

    public void shuffleCards(){
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    
}
