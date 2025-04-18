using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private float targetHealth;
    private Image fillImage;

    public Color highColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;

    private void Awake()
    {
        fillImage = slider.fillRect.GetComponent<Image>();
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, targetHealth, Time.deltaTime * 10f);
        UpdateColor();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        targetHealth = health;
        UpdateColor();
    }

    public void SetValue(int health)
    {
        targetHealth = health;
    }

    private void UpdateColor()
    {
        float percentage = slider.value / slider.maxValue;

        if (percentage > 0.6f)
            fillImage.color = highColor;
        else if (percentage > 0.3f)
            fillImage.color = midColor;
        else
            fillImage.color = lowColor;
    }
}
