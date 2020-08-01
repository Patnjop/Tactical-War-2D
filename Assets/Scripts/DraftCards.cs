using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftCards : MonoBehaviour
{
    public GameObject card;
    public Canvas canvas;

    public int draftAmount;
    public float canvasWidth, canvasHeight;
    // Start is called before the first frame update
    void Start()
    {
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Draft();
        }
    }

    public void Draft()
    {
        for (int i = 0; i < draftAmount; i++)
        {
            GameObject draftCard = Instantiate(card, canvas.transform, true);
            draftCard.transform.position = new Vector3(i * (canvasWidth / draftAmount), canvasHeight / 2, 0);
        }
    }
}
