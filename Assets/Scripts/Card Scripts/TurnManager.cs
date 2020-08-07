using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public float mana, manaPerTurn;
    public Text manaText;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddMana());
    }

    // Update is called once per frame
    void Update()
    {
        manaText.text = "Mana: " + mana.ToString("F1");
    }

    IEnumerator AddMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            mana += manaPerTurn;
        }
    }
}
