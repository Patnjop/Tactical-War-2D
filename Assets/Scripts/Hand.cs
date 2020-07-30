using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int handSize;
    public float cardWidth, cardGap;
    public List<GameObject> cards = new List<GameObject>();
    public DiscardPile DiscardPile;
    public DeckManager DeckManager;
    private Vector3 _transformPosition;
    private float initialCardGap;
    public GameObject Canvas;
    RectTransform CanvasRect;

    // Start is called before the first frame update
    void Start()
    {
        initialCardGap = cardGap;
        CanvasRect = Canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Discard();
            handSize = 0;
            cardGap = initialCardGap;
        }
    }

    public void UpdateHand()
    {
        if (handSize > 5 && handSize <= 10)
        {
            cardGap -= 5f;
        }

        var firstCardX = ((handSize - 1) * 0.5f * cardWidth + (handSize - 1) * 0.5f * cardGap) + (CanvasRect.position.x);
        for (int i = 0; i < handSize; i++)
        {
            _transformPosition.x = firstCardX - (i * (cardGap + cardWidth));
            _transformPosition.y = (CanvasRect.position.y / 4);
            cards[i].GetComponent<RectTransform>().position = _transformPosition;
            cards[i].GetComponent<Canvas>().sortingOrder = i;
            cards[i].GetComponent<CardDragDrop>().inHand = true;
        }
    }

    public void Discard()
    {
        int discardCount = handSize;
        for (int d = discardCount - 1; d >= 0; d--)
        {
            cards[d].transform.position = DiscardPile.transform.position;
            cards[d].GetComponent<CardDragDrop>().inHand = false;
            GameObject cardToDiscard = cards[d];
            cards.Remove(cards[d]);
            DiscardPile.discardPile.Add(cardToDiscard);
            DiscardPile.discardCount++;
            DeckManager.currentHandCount--;
        }
    }
}
