using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool inHand;
    private bool isDragging, placeable = false;
    private Vector2 startposition, hoverStart, startScale;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private int startOrder;
    
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        startScale = _rectTransform.localScale;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        if (inHand == true)
        {
            ResetCard();
            isDragging = true;
            SetCard();
        }
    }

    public void EndDrag()
    {
        if (inHand == true)
        {
            isDragging = false;
            ResetCard();
        }
    }

    public void SetCard()
    {
        startposition = transform.position;
        startOrder = _canvas.sortingOrder;
        startScale = _rectTransform.localScale;
    }

    public void ResetCard()
    {
        transform.position = startposition;
        _canvas.sortingOrder = startOrder;
        _rectTransform.localScale = startScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true)
        {
            startOrder = _canvas.sortingOrder;
            startScale = _rectTransform.localScale;
            transform.position = new Vector2(transform.position.x, transform.position.y + 40);
            _rectTransform.localScale = new Vector2(2, 2);
            _canvas.sortingOrder = 10;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true)
        {
            ResetCard();
        }
    }

}
