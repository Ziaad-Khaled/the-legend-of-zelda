using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUDController : HUDController
{
    //Boss Health 
    public Slider bossHealthSlider;

    public void setBossHealth(int healthPoints)
    {
        bossHealthSlider.value = healthPoints < 0 ? 0 : (healthPoints > 200 ? 200 : healthPoints);
    }
}
