using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    private Image image;
    public float blinkSpeed = 2f;

    void Start()
    {
        image= GetComponent<Image>();
    }

    void Update()
    {
        if (image != null)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
