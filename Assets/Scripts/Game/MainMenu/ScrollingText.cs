using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float scrollSpeed = 10.0f;

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;
    private string sourceText;
    private string tempText;

    // Use this for initialization
    void Awake()
    {
        textRectTransform = text.GetComponent<RectTransform>();

    }

    private void Start()
    {
        StartCoroutine(ScrollingCoroutine());
    }
    private void OnEnable()
    {
        StartCoroutine(ScrollingCoroutine());
    }
    private IEnumerator ScrollingCoroutine()
    {

        float height = text.preferredHeight;
        Vector3 startPosition = textRectTransform.localPosition;

        float scrollPosition = 0;

        while (true)
        {
            textRectTransform.localPosition = new Vector3(startPosition.x, scrollPosition % height, startPosition.z);
            scrollPosition += scrollSpeed * 20 * Time.deltaTime;
            yield return null;
        }
    }
}