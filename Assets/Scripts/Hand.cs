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

    // Start is called before the first frame update
    void Start()
    {
        initialCardGap = cardGap;
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

    public void InstantiateHand()
    {
        if (handSize > 5 && handSize <= 8)
        {
            cardGap -= 0.025f * handSize;
        }
        else if (handSize > 8 && handSize <= 10)
        {
            cardGap -= 0.016f * handSize;
        }

        float firstCardX = (handSize - 1) * 0.5f * cardWidth + (handSize - 1) * 0.5f * cardGap;
        for (int i = 0; i < handSize; i++)
        {
            _transformPosition.x = firstCardX - (i * (cardGap + cardWidth));
            _transformPosition.y = -3.25f;
            cards[i].transform.position = _transformPosition;
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }

    public void Discard()
    {
        int discardCount = handSize;
        for (int d = discardCount - 1; d >= 0; d--)
        {
            cards[d].transform.position = DiscardPile.transform.position;
            GameObject cardToDiscard = cards[d];
            cards.Remove(cards[d]);
            DiscardPile.discardPile.Add(cardToDiscard);
            DiscardPile.discardCount++;
            DeckManager.currentHandCount--;
        }
    }
}
