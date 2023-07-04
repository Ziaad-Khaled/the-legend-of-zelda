using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class MainMenuLevel : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject arrow;
    public TMP_Text text;
    public GameObject levelImage;
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        text.color = new Color32(250, 247, 220, 255);
        arrow.SetActive(true);
        levelImage.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = new Color32(176, 173, 152, 255);
        arrow.SetActive(false);
        levelImage.SetActive(false);
    }

}

