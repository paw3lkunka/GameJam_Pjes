using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private int itemsPressingCount = 0;

    void OnTriggerEnter2D()
    {
        itemsPressingCount++;
        if(itemsPressingCount == 1)
        {
            GetComponent<OnOffSwitch>().On();
        }
    }

    void OnTriggerExit2D()
    {
        itemsPressingCount--;
        if(itemsPressingCount == 0)
        {
            GetComponent<OnOffSwitch>().Off();
        }
    }
}
