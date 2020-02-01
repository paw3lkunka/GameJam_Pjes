using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Sprite plateImage;
    [SerializeField]
    private Sprite pressedPlateImage;
    private int itemsPressingCount = 0;

    void OnTriggerEnter2D()
    {
        itemsPressingCount++;
        if(itemsPressingCount == 1)
        {
            GetComponent<OnOffSwitch>().On();
            GetComponent<SpriteRenderer>().sprite = pressedPlateImage;
        }
    }

    void OnTriggerExit2D()
    {
        itemsPressingCount--;
        if(itemsPressingCount == 0)
        {
            GetComponent<OnOffSwitch>().Off();
            GetComponent<SpriteRenderer>().sprite = plateImage;
        }
    }
}
