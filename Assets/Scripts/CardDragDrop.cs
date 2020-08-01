using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool inHand;
    private bool isDragging, placeable, cardActive = false;
    private Vector2 startposition, hoverStart, startScale;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private int startOrder;
    public Hand hand;
    
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        startScale = _rectTransform.localScale;
        hand = GameObject.Find("Hand").GetComponent<Hand>();
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
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

        if (other.CompareTag("Card"))
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

    //called when card is dragged
    public void StartDrag()
    {
        if (inHand == true)
        {
            ResetCard();
            isDragging = true;
            SetCard();
        }
    }

    //called when mouse drag is released
    public void EndDrag()
    {
        if (inHand == true && placeable == false)
        {
            isDragging = false;
            ResetCard();
        }
        else if (inHand == true && placeable == true)
        {
            inHand = false;
            isDragging = false;
            _canvas.sortingOrder = startOrder;
            hand.CardPlayed(gameObject);
        }
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
        if (isDragging == false && inHand == true && cardActive == false)
        {
            startOrder = _canvas.sortingOrder;
            startScale = _rectTransform.localScale;
            transform.position = new Vector2(transform.position.x, transform.position.y + 40);
            _rectTransform.localScale = new Vector2(2, 2);
            _canvas.sortingOrder = 10;
        }
    }

    //returns the card to it's original state when no longer hovered over
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true && cardActive == false)
        {
            ResetCard();
        }
    }

}
