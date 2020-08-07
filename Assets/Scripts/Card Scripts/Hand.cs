using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int handSize;
    public float cardWidth, cardHeight, cardGap;
    public bool cardActive;
    public List<GameObject> cards = new List<GameObject>();
    public DiscardPile DiscardPile;
    public DeckManager DeckManager;
    private Vector3 _transformPosition;
    private float initialCardGap;
    public GameObject Canvas;
    RectTransform CanvasRect;
    private DraftCards _draftCards;

    // Start is called before the first frame update
    void Start()
    {
        initialCardGap = cardGap;
        CanvasRect = Canvas.GetComponent<RectTransform>();
        _draftCards = DiscardPile.GetComponent<DraftCards>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _draftCards.drafting == false)
        {
            DiscardHand();
            handSize = 0;
            cardGap = initialCardGap;
        }
    }

    public void UpdateHand()
    {
        if (handSize > 5 && handSize <= 10)
        {
            cardGap -= 8f;
        }

        var firstCardX = - ((handSize - 1) * 0.5f * cardWidth + (handSize - 1) * 0.5f * cardGap) + (CanvasRect.position.x);
        for (int i = 0; i < handSize; i++)
        {
            cards[i].GetComponent<CardDragDrop>().ResetCard();
            _transformPosition.x = firstCardX + (i * (cardGap + cardWidth));
            _transformPosition.y = cardHeight / 2;
            cards[i].GetComponent<CardDragDrop>().UpdateCard(_transformPosition, i);
        }
    }

    public void DiscardHand()
    {
        int discardCount = handSize;
        for (int d = discardCount - 1; d >= 0; d--)
        {
            AddToDiscard(cards[d]);
            cards.Remove(cards[d]);
            DeckManager.currentHandCount--;
        }
    }

    public void AddToDiscard(GameObject g)
    {
        g.GetComponent<CardDragDrop>().DiscardCard(DiscardPile.transform.position);
        DiscardPile.discardPile.Add(g);
        DiscardPile.discardCount++;
    }

    public void CardPlayed(GameObject g)
    {
        cards.Remove(g);
        handSize--;
        DeckManager.currentHandCount--;
        UpdateHand();
    }
}
