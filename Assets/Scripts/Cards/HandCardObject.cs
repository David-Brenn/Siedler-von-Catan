using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//import textmesh Pro for unity
using TMPro;


[RequireComponent(typeof(Animator))]
public class HandCardObject : MonoBehaviour
{
    public Card card;
   
    public new TMP_Text name;
    public TMP_Text description; 
    public TMP_Text cost;

    public RawImage borderBox;

    public RawImage textBox;

    public Image Sprite;

    public Image TradePointSprite;
    public Image VictoryPointSprite;

    
    public Color red;
    public Color green;
    public Color yellow;

    public enum TextBoxColor {
        Red,
        Green,
        Yellow,

    }
    public void playCard()
    {
        
        
        card.playCard();
    }

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
                Destroy(TradePointSprite.gameObject);
            }
        }else{
            Destroy(TradePointSprite.gameObject);
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
                Destroy(VictoryPointSprite.gameObject);
            }
        }else{
            Destroy(VictoryPointSprite.gameObject);
        }
    }

    ///<summary>
    /// This method is called as soon a card is drawn from the deck
    /// Its triggers the DrawCard animation
    /// </summary>
    public void isDrawn(){
        GetComponent<Animator>().SetTrigger("DrawCard");

    }
}
