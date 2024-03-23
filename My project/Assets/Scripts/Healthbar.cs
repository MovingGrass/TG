using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
   
    public Slider slider; // Reference to the slider component of the health bar

    public void SetMaxHealth (int currenthealthhealth)
    {
        slider.maxValue = currenthealthhealth;
        slider.value = currenthealthhealth;
    }
    public void SetHealth (int currenthealth)
    {
        slider.value = currenthealth;
    }
}

