using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    public Image thisImage;

    private void Start()
    {
        thisImage = GetComponent<Image>();
    }
    public void UpdateBar(float currentValue, float maxValue)
    {
        thisImage.fillAmount = currentValue / maxValue;
    }
}
