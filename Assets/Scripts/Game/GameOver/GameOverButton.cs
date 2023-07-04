using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Sprite selectedSprite;
    public Sprite deselectedSprite;
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        GetComponent<Image>().sprite = selectedSprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Image>().sprite = deselectedSprite;
    }

}