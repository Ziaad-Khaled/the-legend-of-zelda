using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBarController : MonoBehaviour
{
    public Slider healthSlider;
    public int testHealth=200;
    void Update()
    {
        setHealth(testHealth);
    }

    public void setHealth(int healthPoints)
    {
        healthSlider.value = healthPoints < 0 ? 0 : (healthPoints > 200 ? 200 : healthPoints); 
    }
}
