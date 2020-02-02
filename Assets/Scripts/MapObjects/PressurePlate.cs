using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private Sprite plateImage;
    [SerializeField]
    private Sprite pressedPlateImage;
    [SerializeField,ReadOnly]
    private int itemsPressingCount = 0;

    private OnOffSwitch _switch;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _switch = GetComponent<OnOffSwitch>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    void OnTriggerEnter2D()
    {
        itemsPressingCount++;
        if(itemsPressingCount == 1)
        {
            _switch.On();
            _renderer.sprite = pressedPlateImage;
        }
    }

    void OnTriggerExit2D()
    {
        itemsPressingCount--;
        if(itemsPressingCount == 0)
        {
            _switch.Off();
            _renderer.sprite = plateImage;
        }
    }
}
