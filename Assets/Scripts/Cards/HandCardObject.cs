using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//import textmesh Pro for unity
using TMPro;


/// <summary>
/// This class is used for the card UI element. 
/// It binds the card data to the UI Objects as well as used to trigger the animation for the cards
/// </summary>
public class HandCardObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Contains the scriptalbe object with all the card data
    public Card card;

    //References to the UI objects
    public new TMP_Text name;
    public TMP_Text description; 
    public TMP_Text cost;
    public RawImage borderBox;
    public RawImage textBox;
    public Image Sprite;
    public Image TradePointSprite;
    public Image VictoryPointSprite;

    //Store the Color data for the TextBox
    public Color red;
    public Color green;
    public Color yellow;


    //For the dragging 
    private bool isDragging = false;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //Hand ref + Settings for handcard position
    public float handRadius;
    public float maxCircleAngle;

    //Defies the different Types of Colors for the Text Box
    public enum TextBoxColor {
        Red,
        Green,
        Yellow,

    }


    public void playCard()
    {
        card.playCard();
        //TODO: should also activate a playCard animation
    }

    //TODO: Card should get bigger and become first child
    public void OnHover()
    {
        
       
    }

    void Start()
    {
        choseColor();
        addTradePoints();
        addVictoryPoints();
        this.name.text = card.name;
        description.text = card.description;
        if(card is BuildingCard or KnightCard){
            BuildingCard buildingCard = (BuildingCard)card;
            cost.text = buildingCard.cost.ToString();
        }
    }
    
    
    /// <summary>
    /// This Method is responsible for the color of the text box and the border box. 
    /// The color is Red if the the card needs a city. This can be a BuildingCard or a KnightCard.
    /// The color is Green if the the card only needs a Village. This can be a BuildingCard or a KnightCard.
    /// The color is Yellow if the the card is a Action Card.
    /// Apllies the corespoding color to the text box and a litte bit darker color to the border box.
    /// </summary>
    private void choseColor(){
        if (card is BuildingCard)
        {
            BuildingCard buildingCard = (BuildingCard)card;
            if (buildingCard.needsCity)
            {
                textBox.color = red;
                borderBox.color = new Color(red.r - 0.2f, red.g - 0.2f, red.b - 0.2f);
            }
            else
            {
                textBox.color = green;
                borderBox.color = new Color(green.r - 0.2f, green.g - 0.2f, green.b - 0.2f);
            }
        }

        else if (card is ActionCard)
        {
            textBox.color = yellow;
            borderBox.color = new Color(yellow.r - 0.2f, yellow.g - 0.2f, yellow.b - 0.2f);
        }

    }

    /// <summary>
    /// Adds trade points image if the card has trade points. This can only be a BuildingCard.
    /// For each trade point add a image to the card. Add for each image a litte offset to the right so the images are not on top of each other.
    /// Each added Image is a child of the parent object from the Original Image.
    /// If the card has no trade points remove the trade point image.
    /// </summary>
    private void addTradePoints(){
        if(card is BuildingCard){
            BuildingCard buildingCard = (BuildingCard)card;
            if(buildingCard.tradePoints > 0){
                for(int i = 1; i < buildingCard.tradePoints; i++){
                    Image newImage = Instantiate(TradePointSprite, TradePointSprite.transform.parent);
                    newImage.transform.localPosition = new Vector3(TradePointSprite.transform.localPosition.x + (i * 2f), TradePointSprite.transform.localPosition.y, TradePointSprite.transform.localPosition.z);
                    //make the color of each image a bit darker 
                    newImage.color = new Color(TradePointSprite.color.r - (0.1f * i), TradePointSprite.color.g - (0.1f * i), TradePointSprite.color.b - (0.1f * i));
                    //the new image should be behind the original image
                    newImage.transform.SetSiblingIndex(TradePointSprite.transform.GetSiblingIndex());
                }
            }else{
                //deactivate the TradePointSprite
                TradePointSprite.gameObject.SetActive(false);
            }
        }else{
            TradePointSprite.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Adds Victory points image if the card has Victory points. This can only be a BuildingCard or a KnightCard.
    /// For each trade point add a image to the card. Add for each image a litte offset to the right so the images are not on top of each other.
    /// Each added Image is a child of the parent object from the Original Image.
    /// If the card has no trade points remove the trade point image.
    /// </summary>
    private void addVictoryPoints(){
        if(card is BuildingCard){
            BuildingCard buildingCard = (BuildingCard)card;
            if(buildingCard.victoryPoints > 0){
                for(int i = 1; i < buildingCard.victoryPoints; i++){
                    Image newImage = Instantiate(VictoryPointSprite, VictoryPointSprite.transform.parent);
                    newImage.transform.localPosition = new Vector3(VictoryPointSprite.transform.localPosition.x - (i * 2f), VictoryPointSprite.transform.localPosition.y, VictoryPointSprite.transform.localPosition.z);
                    //make the color of each image a bit darker 
                    newImage.color = new Color(VictoryPointSprite.color.r - (0.1f * i), VictoryPointSprite.color.g - (0.1f * i), VictoryPointSprite.color.b - (0.1f * i));
                    //the new image should be behind the original image
                    newImage.transform.SetSiblingIndex(VictoryPointSprite.transform.GetSiblingIndex());
                }
            }else{
                VictoryPointSprite.gameObject.SetActive(false);

            }
        }else{
            VictoryPointSprite.gameObject.SetActive(false);
        }
    }

    ///<summary>
    /// This method is called as soon a card is drawn from the deck
    /// Its triggers the DrawCard animation
    /// </summary>
    public void isDrawn(){
        GameObject hand = GameObject.Find("Hand");
        this.transform.SetParent(hand.transform);
        placeCardAccordingToIndex();
        
        startPosition = this.transform.localPosition;
        startRotation = this.transform.localRotation;
        Debug.Log("Start Position : "+startPosition);
        Debug.Log("Start Rotation : "+startRotation);

        
        //set the scale to 4 
        this.transform.localScale = new Vector3(4,4,1);
        //set the transform to 0
        this.transform.localPosition = new Vector3(0,400,0);
        this.transform.rotation = Quaternion.Euler(0,0,0);
        this.transform.localRotation = Quaternion.Euler(0,0,0);
        
        //use lean tween to move the card to the hand
        LeanTween.moveLocal(this.gameObject, startPosition, 2f).setDelay(1f);
        LeanTween.scale(this.gameObject, new Vector3(1,1,1), 2f).setDelay(1f);
        LeanTween.rotateLocal(this.gameObject, startRotation.eulerAngles,0.01f).setDelay(3f);
        
        
        
    }

    public void placeCardAccordingToIndex(){
        //get the the handcards of the Player
        List<GameObject> handcards = GameObject.Find("Player").GetComponent<Player>().handcards;
        //int i should be negative for the half of handcards.count, 0 for the middele and positive for the last half
        bool isOdd = handcards.Count % 2 == 1;
        int count;
        if (isOdd){
            count = handcards.Count;
        } else {
            count = handcards.Count * 2 - 1;
        }
        int i = -count/2;
        Debug.Log( "Start: " +"i: "+ i + " Count: " + count + " isOdd: " + isOdd);
        for (int l = 0; l < handcards.Count; l++) {
            float angle;
            if(i % 2 == 0 && !isOdd){
                Debug.Log("break at: " + l);
                i ++;
                l --;
                continue;
            } 

            angle = i * (maxCircleAngle / count);
            Vector3 pos = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0) * handRadius;
            handcards[l].transform.localPosition = pos;
            handcards[l].transform.localRotation = Quaternion.Euler(0, 0, -angle);
            i++;
            Debug.Log( "Whole Loop: "  + "i" + i + " Count: " + count + " isOdd: " + isOdd + " l: " + l + " angle: " + angle);
        }
        //the circle angle should be increased for each card in handcards list but sould never be more then maxCircleAngle
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isDragging || LeanTween.isTweening(this.gameObject)){
            return;
        }
        //create a copy of this element in the center of the screen
        GameObject cardCopy = Instantiate(this.gameObject, GameObject.Find("Canvas").transform);
        //set the name of the copy to "OnHoverCard"
        cardCopy.name = "OnHoverCard";
        //scale the card copy to the size 4
        cardCopy.transform.localScale = new Vector3(4,4,1);
        //set the rotation of the card to 0
        cardCopy.transform.rotation = Quaternion.Euler(0,0,0);
        //set local position to 0
        cardCopy.transform.localPosition = new Vector3(0,0,0);

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //destroy the card copy
        Destroy(GameObject.Find("OnHoverCard"));
        
    }

public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        OnPointerExit(eventData);
        //set Rotaiton to 0
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 positionDelta = eventData.position - eventData.pressPosition;
            transform.position = eventData.position;
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        //TODO Raycast to objects to determine if the card is played or moved back to the hand
        //TODO if the card is played, play the card and destroy the object
        transform.position = startPosition;
        transform.rotation = startRotation;

        isDragging = false;
    }
}
