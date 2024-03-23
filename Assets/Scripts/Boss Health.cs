using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider slide;

    public Color Low;
    public Color High;
    public Vector3 offset;

    // Start is called before the first frame update
    public void SetHealthBoss(float currenthealth, float maxHealth)
    {
        slide.gameObject.SetActive(currenthealth < maxHealth);
        slide.value = currenthealth;
        slide.maxValue = maxHealth;

        slide.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slide.normalizedValue);
    }

    void Update()
    {
        slide.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
