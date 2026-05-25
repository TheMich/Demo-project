using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image filledPart;
    public Image background;

    public void ApplyHealthFraction(float healthFraction)
    {
        // Scale the filled part to the fraction provided
        filledPart.rectTransform.localScale = new(healthFraction, 1, 1);

        // Only show partially filled health bars
        filledPart.enabled = healthFraction < 1;
        background.enabled = healthFraction < 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
