using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour
{

    [Header("Required")]
    public GameObject Note;
    public GameObject Sheet;
    public TextMeshProUGUI CurrentSlice;
    public Button Undo;
    [Tooltip("Order matters here, please do not change it!")] public Sprite[] Notes;

    private int _currentSlice = 0;
    private Dictionary<int, List<Notation>> _orderedNotes;
    private Dictionary<float, Sprite> _noteLengthSprite;

    public static UnityAction<Notation> AddNote;
    public static UnityAction<float> ChangeLength;
    public static UnityAction<int> ChangeSlice;
    public static float NoteLength = 1f;

    private Vector2 _sheetSize;
    private float _noteSize;

    void Awake()
    {
        _orderedNotes = new Dictionary<int, List<Notation>>();
        _noteLengthSprite = new Dictionary<float, Sprite>();
        RectTransform rt = this.GetComponent<RectTransform>();
        RectTransform sheetRT = Sheet.GetComponent<RectTransform>();
        rt.sizeDelta = sheetRT.sizeDelta;
        rt.localPosition = sheetRT.localPosition;
        _sheetSize = rt.sizeDelta;
        _noteSize = _sheetSize.y / 12;

        int spriteIndex = 0;
        foreach (float length in new[] { 1f, 2f, 8f, 16f })
        {
            _noteLengthSprite.Add(length, Notes[spriteIndex]);
            spriteIndex++;
        }
    }

    void OnEnable()
    {
        AddNote += HandleAddNote;
        ChangeSlice += HandleChangeSlice;
        Undo.onClick.AddListener(delegate { HandleUndo(); });
        ChangeLength += HandleChangeLength;
    }

    void OnDisable()
    {
        AddNote -= HandleAddNote;
        ChangeSlice -= HandleChangeSlice;
        Undo.onClick.RemoveAllListeners();
        ChangeLength -= HandleChangeLength;
    }

    private void DrawNotes()
    {
        if (!_orderedNotes.ContainsKey(_currentSlice)) return;

        for (int i = 0; i < _orderedNotes[_currentSlice].Count; i++)
        {
            GameObject note = Instantiate(Note, this.transform);
            RectTransform noteRT = note.GetComponent<RectTransform>();
            noteRT.sizeDelta = new Vector2(_noteSize, _sheetSize.y);
            noteRT.localPosition = new Vector3(_noteSize * i, 1);
            Note noteComponent = note.GetComponentInChildren<Note>();
            noteComponent.Init(_noteLengthSprite[_orderedNotes[_currentSlice][i].Length], _noteSize, new Vector2(1, _noteSize * _orderedNotes[_currentSlice][i].StaffIndex));
        }
    }

    private void EraseNotes()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);
    }

    private void HandleChangeSlice(int value)
    {
        _currentSlice += value;
        _currentSlice = Mathf.Clamp(_currentSlice, 0, _orderedNotes.Count);
        CurrentSlice.text = (_currentSlice + 1).ToString();
        EraseNotes();
        DrawNotes();
    }

    private void HandleChangeLength(float length) => NotesManager.NoteLength = length;

    private void HandleAddNote(Notation notation)
    {
        if (!ValidateNotesLength(notation.Length)) return;

        if (_orderedNotes.ContainsKey(_currentSlice))
        {
            _orderedNotes[_currentSlice].Add(notation);
        }
        else
        {
            _orderedNotes.Add(_currentSlice, new List<Notation>());
            _orderedNotes[_currentSlice].Add(notation);
        }

        EraseNotes();
        DrawNotes();
    }

    private void HandleUndo()
    {
        if (_orderedNotes.ContainsKey(_currentSlice))
        {
            if (_orderedNotes[_currentSlice].Count > 0)
            {
                _orderedNotes[_currentSlice].RemoveAt(_orderedNotes[_currentSlice].Count - 1);
                EraseNotes();
                DrawNotes();
            }

            if (_orderedNotes[_currentSlice].Count == 0)
            {
                if (_currentSlice > 0)
                {
                    _orderedNotes.Remove(_currentSlice);
                    HandleChangeSlice(-1);
                }
            }
        }
    }

    private bool ValidateNotesLength(float newNoteLength)
    {
        if (!_orderedNotes.ContainsKey(_currentSlice)) return true;

        float sum = 4 / newNoteLength;

        for (int i = 0; i < _orderedNotes[_currentSlice].Count; i++)
        {
            sum += 4 / _orderedNotes[_currentSlice][i].Length;
        }

        return sum <= 4 ? true : false;
    }

}



