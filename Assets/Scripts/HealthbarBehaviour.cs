using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;

    public Color Low;
    public Color High;
    public Vector3 offset;

    // Start is called before the first frame update
    public void SetHealth(float currenthealth, float maxHealth)
    {
        slider.gameObject.SetActive(currenthealth < maxHealth);
        slider.value = currenthealth;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}

