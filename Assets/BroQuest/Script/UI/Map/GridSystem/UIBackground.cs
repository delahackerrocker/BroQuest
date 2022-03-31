using UnityEngine;
using UnityEngine.UI;

public class UIBackground : MonoBehaviour
{
    public float animationDuration = 1.0f;
    public Color offColor;
    public Color onColor;

    Color targetColor;
    Color currentColor;

    Image image;

    float timer = 1.0f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (timer > 0.998f)
            return;

        timer += Time.deltaTime / animationDuration;
        image.color = Color.Lerp(currentColor, targetColor, timer); 
    }

    public void TurnOn()
    {
        timer = 0.0f;
        currentColor = image.color;
        targetColor = onColor;
    }

    public void TurnOff()
    {
        timer = 0.0f;
        currentColor = image.color;
        targetColor = offColor;
    }
}
