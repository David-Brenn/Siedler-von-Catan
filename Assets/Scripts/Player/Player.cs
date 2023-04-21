using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//q: how can i reach code outside of my folder?
//a: use namespace
//add namespace





public class Player : MonoBehaviour
{
    public string playerName;
    public Camera playerCamera;
    // Player's cards 
    public List<Card> handcards;
    public List<LandscapeCard> landscapeCards;
    public List<Transform> landscapeTransforms;
    public List<Card> streetCards;
    public List<Transform> streetTransforms;
    public List<Card> cityCards;
    public List<Transform> cityTransforms;
    // Player's resources
    public int sheep = 1;
    public int clay = 1;
    public int wood = 1;
    public int hay = 1;
    public int gold = 1;
    public int ore = 1;

    public int victoryPoints = 2;

    public bool checkDice = false;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (LandscapeCard landscape in landscapeCards)
        {
            landscape.addResource(1);
            landscape.transform.position = landscapeTransforms[i].position;
            i++;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dice.diceInstance.isMoving)
        {

            StartCoroutine(waitForDice());

        }
        if (checkDice)
        {
            eventNumberDice();
        }
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = playerCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.gameObject != null){
                    GameObject clickedObject = hit.collider.gameObject;
                    string tag = hit.collider.gameObject.tag;
                    if(tag == "Deck"){
                        if (GameManager.instance.currentPlayer == this){
                            drawCardFromDeck(clickedObject);
                        } else {
                            showWarning("It's not your turn!");
                        }
                        
                    }
                }
            }
        }
    }

    IEnumerator waitForDice()
    {
        while (dice.diceInstance.isMoving)
        {
            yield return new WaitForSeconds(0.5f);
        }
        checkDice = true;
    }

    void eventNumberDice()
    {
        checkDice = false;
        foreach (LandscapeCard landscape in landscapeCards)
        {
            Debug.Log(landscape.name);
            Debug.Log("Dice Value =" + dice.diceInstance.diceValue);
            if (landscape.diceValue == dice.diceInstance.diceValue)
            {
                int oldValue = landscape.resource;
                landscape.addResource(1);
                Debug.Log("Active Landscape = " + landscape.name);
                Debug.Log("Resource Value = " + landscape.resource);
                int difValue = landscape.resource - oldValue;
                switch (landscape.type)
                {
                    case LandscapeTypes.hay:
                        hay = hay + difValue;
                        break;
                    case LandscapeTypes.clay:
                        clay = clay + difValue;
                        break;
                    case LandscapeTypes.ore:
                        ore = ore + difValue;
                        break;
                    case LandscapeTypes.sheep:
                        sheep = sheep + difValue;
                        break;
                    case LandscapeTypes.wood:
                        wood = wood + difValue;
                        break;
                    case LandscapeTypes.gold:
                        gold = gold + difValue;
                        break;
                }

            }
        }
    }

    void eventEventDice()
    {

    }


    void playHandCard()
    {


    }

    void choseBuildingPlace(){

    }

    void drawCardFromDeck(GameObject deck){
        Deck deckScript = deck.GetComponent<Deck>();
        GameObject card = deckScript.drawCard();
    //        handcards.Add(card.GetComponent<Card>());
        //add card to canvas
        card.transform.SetParent(GameObject.Find("Canvas").transform);
        
    }

    void showWarning(string message){
        Debug.Log(message);
    }
}

