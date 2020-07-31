using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool inHand;
    public Hand hand;
    private bool isDragging = false;
    private Vector2 startposition, hoverStart, startScale;
    private RectTransform _rectTransform, childrenTransform;
    private Canvas _canvas;
    private int startOrder;
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        childrenTransform = GetComponentInChildren<RectTransform>();
        startScale = _rectTransform.localScale;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void startDrag()
    {
        if (inHand == true)
        {
            startposition = transform.position;
            isDragging = true;
            _rectTransform.localScale = startScale;
        }
    }

    public void EndDrag()
    {
        if (inHand == true)
        {
            isDragging = false;
            transform.position = startposition;
        }
    }

    public void SetCard()
    {
        hoverStart = transform.position;
        startOrder = _canvas.sortingOrder;
        startScale = _rectTransform.localScale;
    }

    public void ResetCard()
    {
        _canvas.sortingOrder = startOrder;
        transform.position = hoverStart;
        _rectTransform.localScale = startScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging == false && inHand == true)
        {
            SetCard();
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
