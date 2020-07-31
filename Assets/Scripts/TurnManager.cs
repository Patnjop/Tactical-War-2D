using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float mana, manaPerTurn;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddMana());
    }

    // Update is called once per frame
    void Update()
    {
        
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
