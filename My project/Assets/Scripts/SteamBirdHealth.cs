using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteamBirdHealth : MonoBehaviour
{
    public Slider sliderB;

    public Color Low;
    public Color High;
    public Vector3 offset;

    // Start is called before the first frame update
    public void SetHealthBird(float currenthealth, float maxHealth)
    {
        sliderB.gameObject.SetActive(currenthealth < maxHealth);
        sliderB.value = currenthealth;
        sliderB.maxValue = maxHealth;

        sliderB.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, sliderB.normalizedValue);
    }

    void Update()
    {
        sliderB.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
