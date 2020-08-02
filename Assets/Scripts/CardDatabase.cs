using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public static List<Card> CardList = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Card c in allCards)
        {
            CardList.Add(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
