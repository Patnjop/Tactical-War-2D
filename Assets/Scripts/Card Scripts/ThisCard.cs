using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThisCard : MonoBehaviour
{
    public CardDatabase _CardDatabase;
    public List<Card> thisCard = new List<Card>();

    public int thisId, index, cost;

    public Image image;
    public Sprite sprite;
    public Text costText, nameText, descriptionText;
    public GameObject unit;
    

    public string cardName, description;
    // Start is called before the first frame update
    void Start()
    {
        _CardDatabase = GameObject.Find("GameManager").GetComponent<CardDatabase>();
        thisCard.Add(_CardDatabase.allCards[thisId]);
        
        index = thisCard[0].index;
        cost = thisCard[0].cost;
        cardName = thisCard[0].cardName;
        description = thisCard[0].description;
        sprite = thisCard[0].sprite;
        unit = thisCard[0].unit;

        nameText.text = "" + cardName;
        descriptionText.text = "" + description;
        costText.text = "" + cost;
        image.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
