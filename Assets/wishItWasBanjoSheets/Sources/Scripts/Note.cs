using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public void Init(Sprite image, float size, Vector2 position)
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(size, size);
        rt.localPosition = position;

        if (image == null) return;
        Image img = this.GetComponent<Image>();
        img.sprite = image;
    }
}
