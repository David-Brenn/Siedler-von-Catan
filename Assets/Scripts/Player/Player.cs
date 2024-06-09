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
    //TODO: List of Cards it not right this only stores the c# script make it to GameObjects?
    public List<GameObject> handcards;
    public List<GameObject> landscapeCards;
    public List<Transform> landscapeTransforms;
    public List<GameObject> streetCards;
    public List<Transform> streetTransforms;
    public List<Card> cityCards;
    public List<Transform> cityTransforms;


    // Player's resources
    public Cost resource;

    //TODO: delete all of this resource and use the Cost struct instead
    public int sheep = 1;
    public int clay = 1;
    public int wood = 1;
    public int hay = 1;
    public int gold = 1;
    public int ore = 1;


    public int victoryPoints = 2;

    //TODO: Move this to GameManager
    public bool checkDice = false;

    public GameObject hand;
    //Add each Landscape to Possition 
    //TODO: Maybe move this to the player preset? Therefore you don't need to set the positions

    public List<GameObject> highlightedSpots = new List<GameObject>();

    public GameObject highlightPrefab;
    void Start()
    {
        int i = 0;
        foreach (GameObject landscape in landscapeCards)
        {
            //landscape.GetComponent<LandscapeCard>().addResource(1);
            //landscape.transform.position = landscapeTransforms[i].position;
            //i++;

        }
    }

    //TODO: Move the Dice Logic to the GameManager and just send its result to the player via a addResource Function()
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
                            StartCoroutine(drawCardFromDeck(clickedObject));
                        } else {
                            showWarning("It's not your turn!");
                        }
                        
                    }
                }
            }
        }
    }
    //TODO: Same as before move th GameManager
    IEnumerator waitForDice()
    {
        while (dice.diceInstance.isMoving)
        {
            yield return new WaitForSeconds(0.5f);
        }
        checkDice = true;
    }

    //TODO: Move to GameManager
    void eventNumberDice()
    {
        checkDice = false;
        foreach (GameObject landscape in landscapeCards)
        {
            LandscapeCard landscapeCardComponent = landscape.GetComponent<LandscapeCard>();
            Debug.Log(landscapeCardComponent.name);
            Debug.Log("Dice Value =" + dice.diceInstance.diceValue);
            if (landscapeCardComponent.diceValue == dice.diceInstance.diceValue)
            {
                int oldValue = landscapeCardComponent.resource;
                landscapeCardComponent.addResource(1);
                Debug.Log("Active Landscape = " + landscapeCardComponent.name);
                Debug.Log("Resource Value = " + landscapeCardComponent.resource);
                int difValue = landscapeCardComponent.resource - oldValue;
                switch (landscapeCardComponent.type)
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

    //TODO
    /// <summary>
    /// This function enabels a building overlay where you can chose a building place. 
    /// </summary>
    void choseBuildingPlace(){

    }

    /// <summary>
    /// Gets a Card from a Deck and adds it to the handcards list
    /// </summary>
    /// <param name="deck"></param>
    IEnumerator drawCardFromDeck(GameObject deck){
        Deck deckScript = deck.GetComponent<Deck>();

        GameObject card = deckScript.drawCard();
        //yield return new WaitForSeconds(2f);
        //handcards.Add(card.GetComponent<Card>());
        //add card to canvas
        //q: i used git init how can i see my code on github?
        //a: use git add . and git commit -m "message" and git push
        addCardToHand(card);
        yield return null;
    }

    //Adds a card to list and sets the position for the GameObejct as well as adjust the position of the existing card in the Hand
    void addCardToHand(GameObject card){
        handcards.Add(card);
        
        card.GetComponent<HandCardObject>().isDrawn();
    }

    /// <summary>
    /// This method is used to calculate the positions of the gameobejcts in handcards list.
    /// The gameobjects should be placed in a half circle around the hand gameobejct with a radius of handRadius.
    /// The cards should be placed in the order of the handcards list and are child objects of the hand gameobject.
    /// </summary>
    

    //TODO: use this method to display messages to the player e.g: "It's not your turn"
    void showWarning(string message){
        Debug.Log(message);
    }

    public void addVillageCard(){
        Debug.Log("Add Village Card");
        openBuildingOverlay(BuildingType.VILLAGE);

    }

    enum BuildingType{
        VILLAGE,
        CITY,
        STREET,
        RESOURCE,
        BUILDINGCARD
    }
    /// <summary>
    /// This functions opens the building overlay where you can chose a building place. It highlights the possible building places depending on the building type. 
    /// If the Type is VILLAGE only the places next to streets should be highlighted.
    /// If the Type is CITY only Villages should be highlighted.
    /// If the Type is Street only empty spots next to villages and cities should be highlighted.
    /// If the Type is RESOURCE only the corners of the villages and cities should be highlighted.
    /// If the Type is BUILDINGCARD only the spaces above and below villages and cities should be highlighted.
    /// </summary>
    /// <param name="buildingType"></param>
    void openBuildingOverlay(BuildingType buildingType){

        switch(buildingType){
            case BuildingType.VILLAGE:
                Debug.Log("Case Village");
                if (streetCards[0].GetComponent<StreetCard>().leftVillage == null){
                    Debug.Log("Left Village is null");
                    GameObject leftStreetHighlight = Instantiate<GameObject>(highlightPrefab,streetCards[0].transform);
                    leftStreetHighlight.transform.position = leftStreetHighlight.transform.position + new Vector3(-2.5f,0,0);
                    highlightedSpots.Add(leftStreetHighlight);
                } 
                Debug.Log(streetCards.Count);
                if(streetCards[streetCards.Count-1].GetComponent<StreetCard>().rightVillage == null){
                    Debug.Log("Right Village is null");
                    GameObject rightStreetHighlight = Instantiate<GameObject>(highlightPrefab,streetCards[streetCards.Count-1].transform);
                    rightStreetHighlight.transform.position = rightStreetHighlight.transform.position + new Vector3(2.5f,0,0);
                    highlightedSpots.Add(rightStreetHighlight);
                }
            break;
            case BuildingType.CITY:
            break;
            case BuildingType.STREET:
            break;
            case BuildingType.RESOURCE:
            break;
            case BuildingType.BUILDINGCARD:
            break;

        }
    }
}

