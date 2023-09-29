using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Staff : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Customizables:")]
    public Color highlightColor = Color.grey;

    private int _staffIndex;
    private Image _image;
    private Color _color;

    public void Init(int staffIndex, float width, float height, Color color)
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
        _image = this.GetComponent<Image>();
        _image.color = color;
        _color = color;
        _staffIndex = staffIndex;
    }

    public void OnPointerClick(PointerEventData eventData) => NotesManager.AddNote(new Notation(_staffIndex, 1, eventData.pointerCurrentRaycast.screenPosition));

    public void OnPointerEnter(PointerEventData eventData) => _image.color = highlightColor;

    public void OnPointerExit(PointerEventData eventData) => _image.color = _color;
}