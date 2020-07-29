using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public List<GameObject> selectedUnits = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Unit"))
                {
                    hit.collider.gameObject.GetComponent<BaseUnit>().selected =
                        !hit.collider.gameObject.GetComponent<BaseUnit>().selected;
                    if (hit.collider.gameObject.GetComponent<BaseUnit>().selected == true)
                    {
                        selectedUnits.Add(hit.collider.gameObject);
                    }
                    else
                    {
                        selectedUnits.Remove(hit.collider.gameObject);
                    }
                }
                Debug.Log(hit.collider.gameObject.name);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetMousePos2D = new Vector2(targetMousePos.x, targetMousePos.y);

            foreach (GameObject g in selectedUnits)
            {
                g.GetComponent<BaseUnit>().moveToPoint(targetMousePos2D);
            }
        }
    }
}
