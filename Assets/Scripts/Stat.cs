using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Text text;
    [SerializeField]
    private float lerpSpeed;

    private float currentFill;
    public float MaxValue { get; set; }
    private float currentValue;
    public float CurrentValue 
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value >= MaxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0f;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / MaxValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (image.fillAmount != currentFill)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
        text.text = CurrentValue + " / " + MaxValue;
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
