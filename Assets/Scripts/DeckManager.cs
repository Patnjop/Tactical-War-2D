using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject card, canvas;
    public float deckSize, handSizeMax, currentHandCount, startingHandSize;
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> handofCards = new List<GameObject>();
    
    public Hand hand;
    public DiscardPile DiscardPile;
    private GameObject tempCard;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateDeck();
        ShuffleDeck();
        //DrawCards(startingHandSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DrawCards(1f);
        }

        if (deckSize <= 0)
        {
            deckSize = 0;
            ReShuffle();
        }
    }

    public void CreateDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            GameObject newCard = Instantiate(card, canvas.transform, true);
            newCard.transform.position = transform.position;
            deck.Add(newCard);
            deck[i].name = "Card number " + i;
        }
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rnd = Random.Range(0, deck.Count);
            tempCard = deck[rnd];
            deck[rnd] = deck[i];
            deck[i] = tempCard;
        }
    }

    public void DrawCards(float numberofCards)
    {
        float cardsToDraw;
        if (currentHandCount + numberofCards <= handSizeMax)
        {
            cardsToDraw = numberofCards;
        }
        else if (currentHandCount + numberofCards > handSizeMax)
        {
            cardsToDraw = handSizeMax - currentHandCount;
        }
        else
        {
            cardsToDraw = 0;
        }
        for (int h = 0; h < cardsToDraw; h++)
        {
            GameObject cardDrawn = deck[h];
            deck.Remove(deck[h]);
            hand.cards.Add(cardDrawn);
            hand.handSize++;
            currentHandCount++;
            deckSize--;
            hand.UpdateHand();
        }
    }

    public void ReShuffle()
    {
        int reShuffleCount = DiscardPile.discardCount;
        for (int r = reShuffleCount - 1; r >= 0; r--)
        {
            DiscardPile.discardPile[r].transform.position = transform.position;
            GameObject cardToReshuffle = DiscardPile.discardPile[r];
            DiscardPile.discardPile.Remove(DiscardPile.discardPile[r]);
            deck.Add(cardToReshuffle);
            DiscardPile.discardCount--;
            deckSize++;
        }
    }
}
