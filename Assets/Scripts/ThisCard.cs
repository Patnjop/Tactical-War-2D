using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisCard : MonoBehaviour
{
    public CardDatabase _CardDatabase;
    public List<Card> thisCard = new List<Card>();

    public int thisId, index, cost;

    public string cardName, description;
    // Start is called before the first frame update
    void Start()
    {
        _CardDatabase = GameObject.Find("GameManager").GetComponent<CardDatabase>();
        thisCard.Add(_CardDatabase.allCards[thisId]);
    }

    // Update is called once per frame
    void Update()
    {
        index = thisCard[0].index;
        cost = thisCard[0].cost;
        cardName = thisCard[0].cardName;
        description = thisCard[0].description;
    }
}
