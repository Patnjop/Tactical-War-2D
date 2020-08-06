using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool inHand;
    public bool isDragging, isDrafting = false;
    private bool placeable, cardActive = false;
    private Vector2 startposition, hoverStart;
    public Vector2 startScale;
    public RectTransform _rectTransform;
    private Canvas _canvas;
    private Camera camera;
    private int startOrder;
    public float draftScale;
    private Vector3 scale;
    private Hand hand;
    private DiscardPile DiscardPile;
    private DraftCards _draftCards;
    private TurnManager _turnManager;
    private ThisCard _card;

    private void Start()
    {
        camera = Camera.main;
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        //startScale = _rectTransform.localScale;
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        DiscardPile = GameObject.Find("Discard Pile").GetComponent<DiscardPile>();
        _turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        _draftCards = DiscardPile.GetComponent<DraftCards>();
        _card = GetComponent<ThisCard>();
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
            _canvas.sortingOrder = 10;
        }
    }

    //called when card collides with playable zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Field"))
        {
            placeable = true;
        }

        if (other.CompareTag("Card") && other.GetComponent<CardDragDrop>().isDragging == true)
        {
            cardActive = true;
        }
    }

    //called when card leaves playable zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Field"))
        {
            placeable = false;
        }
        if (other.CompareTag("Card"))
        {
            cardActive = false;
        }
    }

    public void MouseClick()
    {
        if (isDrafting == true)
        {
            hand.AddToDiscard(gameObject);
            isDrafting = false;
            DiscardPile.GetComponent<DraftCards>().EndDraft(gameObject);
        }
    }

    //called when card is dragged
    public void StartDrag()
    {
        if (inHand == true && _draftCards.drafting == false) 
        {
            ResetCard();
            hand.cardActive = true;
            isDragging = true;
            SetCard();
        }
    }

    //called when mouse drag is released
    public void EndDrag()
    {
        if (inHand == true && placeable == false && _draftCards.drafting == false)
        {
            ResetCard();
        }
        else if (inHand == true && placeable == true && _card.cost > _turnManager.mana)
        {
            ResetCard();
        }
        else if (inHand == true && placeable == true && _card.cost <= _turnManager.mana)
        {
            inHand = false;
            isDragging = false;
            _canvas.sortingOrder = startOrder;
            hand.CardPlayed(gameObject);
            _turnManager.mana -= _card.cost;
            Mathf.Round(_turnManager.mana * Mathf.Pow(10, 1));
        }

        hand.cardActive = false;
    }
    
    //saves the original information of the card before moving
    public void SetCard()
    {
        startposition = transform.position;
        startOrder = _canvas.sortingOrder;
        startScale = _rectTransform.localScale;
    }
    
    //resets the card to the original information
    public void ResetCard()
    {
        transform.position = startposition;
        _canvas.sortingOrder = startOrder;
        _rectTransform.localScale = startScale;
        isDragging = false;
    }
    
    //Alters each card when a new card is added to the hand
    public void UpdateCard(Vector3 v, int i)
    {
        _rectTransform.position = v;
        _canvas.sortingOrder = i;
        inHand = true;
        SetCard();
    }

    //Discards a card
    public void DiscardCard(Vector3 u)
    {
        EndDrag();
        ResetCard();
        transform.position = u;
        inHand = false;
    }

    //makes a card larger when hovered over
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true && hand.cardActive == false)
        {
            startOrder = _canvas.sortingOrder;
            var localScale = _rectTransform.localScale;
            startScale = localScale;
            localScale = new Vector2(localScale.x * 2, localScale.y *2);
            _rectTransform.localScale = localScale;
            transform.position = new Vector2(transform.position.x, transform.position.y + _rectTransform.rect.height / 2);
            _canvas.sortingOrder = 10;
        }
    }

    //returns the card to it's original state when no longer hovered over
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true && hand.cardActive == false)
        {
            ResetCard();
        }
    }

}
