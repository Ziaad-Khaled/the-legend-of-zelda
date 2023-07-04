using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthHearts : MonoBehaviour
{
    public int health = 24;
    public int maxHearts = 12;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        updateHealthUI(health); //TODO: Remove this line when integrating
    }
    public void updateHealthUI(int HealthPoints)
    {
        health = HealthPoints;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < ((health - 1) / 2))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                if (i == ((health - 1) / 2))
                {
                    if (health % 2 == 1)
                    {
                        hearts[i].sprite = halfHeart;
                    }
                    else
                    {
                        hearts[i].sprite = fullHeart;
                    }
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }
}


