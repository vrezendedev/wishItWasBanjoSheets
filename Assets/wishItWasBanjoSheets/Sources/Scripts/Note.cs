using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public void Init(Sprite image, float size, Vector2 position)
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        float scaledSize = size * 3;
        rt.sizeDelta = new Vector2(scaledSize, scaledSize);
        rt.localPosition = position;
        Image img = this.GetComponent<Image>();
        img.sprite = image;
    }
}
