using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftCards : MonoBehaviour
{
    public GameObject card;
    public Canvas canvas;

    public bool drafting;
    public int draftAmount, draftIndex;
    public float cameraHalfWidth, cameraHalfHeight, sizeMultiplier;

    public List<GameObject> draftedCards;
    // Start is called before the first frame update
    void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.aspect * cameraHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && drafting == false)
        {
            Draft();
        }
    }

    public void Draft()
    {
        drafting = true;
        draftIndex++;
        for (int i = 0; i < draftAmount; i++)
        {
            GameObject draftCard = Instantiate(card, canvas.transform, true);
            draftCard.name = "Drafted Card " + draftIndex;
            draftCard.GetComponent<ThisCard>().thisId = Random.Range(0, 2);
            draftCard.transform.position = new Vector3((i+1) * ((cameraHalfWidth * 2) / (draftAmount + 1)) - cameraHalfWidth, cameraHalfHeight, 0);
            draftCard.GetComponent<Canvas>().sortingOrder = 5;
            draftCard.GetComponent<RectTransform>().localScale= new Vector2(2, 2);
            draftedCards.Add(draftCard);
            draftCard.GetComponent<CardDragDrop>().isDrafting = true;
        }
    }

    public void EndDraft(GameObject cardPicked)
    {
        draftedCards.Remove(cardPicked);
        foreach (GameObject g in draftedCards)
        {
            Destroy(g);
        }
        draftedCards.Clear();
        drafting = false;
    }
}
