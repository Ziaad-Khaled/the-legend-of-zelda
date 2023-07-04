using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite selectedSprite;
    public Sprite deselectedSprite;


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = selectedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = deselectedSprite;
    }
}
