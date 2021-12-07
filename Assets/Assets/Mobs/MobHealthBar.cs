using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject mob;
    [SerializeField]
    private Vector3 offset;

    private SpriteRenderer mobSprite;
    // Start is called before the first frame update
    void Start()
    {
        mobSprite = mob.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vect = new Vector3(0f, mobSprite.bounds.size.y / 2, 0f);
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position - vect + offset);
    }
    public void UpdateHealthUI(int health, int maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.minValue = 0f;
    }
    public void DestroyHealthBar()
    {
        Destroy(transform.gameObject);
    }
}
