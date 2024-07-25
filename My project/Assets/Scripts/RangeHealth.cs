using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeHealth : MonoBehaviour
{
    public Slider sliderR;

    public Color Low;
    public Color High;
    public Vector3 offset;

    // Start is called before the first frame update
    public void SetHealthRange(float currenthealth, float maxHealth)
    {
        sliderR.gameObject.SetActive(currenthealth < maxHealth);
        sliderR.value = currenthealth;
        sliderR.maxValue = maxHealth;

        sliderR.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, sliderR.normalizedValue);
    }

    void Update()
    {
        sliderR.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    
}
