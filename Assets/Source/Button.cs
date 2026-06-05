using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class Button : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    private RectTransform m_rectangle;
    private Image m_image;

    void Start()
    {
        m_rectangle = GetComponent<RectTransform>();
        m_image = GetComponent<Image>();
    }

    void Update()
    {
        // If mouse is within our rectangle, clicking should do something
        if(RectTransformUtility.RectangleContainsScreenPoint(m_rectangle, Input.mousePosition))
        {
            m_image.color = Color.black;
            if(Input.GetButtonDown("Fire1"))
            {
                eventToTrigger.Invoke();

            }
        } else
        {
            m_image.color = Color.grey;
        }
    }
}
