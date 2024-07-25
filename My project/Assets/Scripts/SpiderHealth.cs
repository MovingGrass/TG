using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderHealth : MonoBehaviour
{
    public Slider sliderS;

    public Color Low;
    public Color High;
    public Vector3 offset;

    // Start is called before the first frame update
    public void SetHealthSpider(float currenthealth, float maxHealth)
    {
        sliderS.gameObject.SetActive(currenthealth < maxHealth);
        sliderS.value = currenthealth;
        sliderS.maxValue = maxHealth;

        sliderS.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, sliderS.normalizedValue);
    }

    void Update()
    {
        sliderS.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
