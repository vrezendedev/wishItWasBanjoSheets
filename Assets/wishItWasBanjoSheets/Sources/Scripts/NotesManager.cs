using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using static WishItWasBanjoSheetsEnums;
using System;

public class NotesManager : MonoBehaviour
{

    [Header("Required")]
    [SerializeField] private GameObject goNote;
    [SerializeField] private GameObject goSheet;
    [SerializeField] private TextMeshProUGUI currentSlice;
    [SerializeField] private Button bUndo;
    [SerializeField] private Image changeClefBtn;
    [Tooltip("Order matters here, please do not change it!"), SerializeField] private Sprite[] sNotes;
    [SerializeField] private Image clefImage;
    [SerializeField] private Sprite clefG;
    [SerializeField] private Sprite clefF;
    [SerializeField] private TextMeshProUGUI currentNote;

    private int _currentSlice = 0;
    private Dictionary<int, List<Notation>> _orderedNotes;
    private Dictionary<float, Sprite> _noteLengthSprite;

    public static UnityAction<Notation> AddNote;
    public static UnityAction<float> ChangeLength;
    public static UnityAction<int> ChangeSlice;
    public static UnityAction ChangeClef;
    public static UnityAction<string> ChangeCurrentNote;

    public static float NoteLength = 1f;
    public static Clef _currentClef = Clef.G;

    private Vector2 _sheetSize;
    private float _noteSize;

    void Awake()
    {
        _orderedNotes = new Dictionary<int, List<Notation>>();
        _noteLengthSprite = new Dictionary<float, Sprite>();
        RectTransform rt = this.GetComponent<RectTransform>();
        RectTransform sheetRT = goSheet.GetComponent<RectTransform>();
        rt.sizeDelta = sheetRT.sizeDelta;
        rt.localPosition = sheetRT.localPosition;
        _sheetSize = rt.sizeDelta;
        _noteSize = _sheetSize.y / 12;

        int spriteIndex = 0;
        foreach (float length in new[] { 1f, 2f, 8f, 16f })
        {
            _noteLengthSprite.Add(length, sNotes[spriteIndex]);
            spriteIndex++;
        }
    }

    void OnEnable()
    {
        AddNote += HandleAddNote;
        ChangeSlice += HandleChangeSlice;
        bUndo.onClick.AddListener(delegate { HandleUndo(); });
        ChangeLength += HandleChangeLength;
        ChangeClef += HandleChageClef;
        ChangeCurrentNote += HandleChangeCurrentNote;
    }

    void OnDisable()
    {
        AddNote -= HandleAddNote;
        ChangeSlice -= HandleChangeSlice;
        bUndo.onClick.RemoveAllListeners();
        ChangeLength -= HandleChangeLength;
        ChangeClef -= HandleChageClef;
        ChangeCurrentNote -= HandleChangeCurrentNote;
    }

    private void DrawNotes()
    {
        if (!_orderedNotes.ContainsKey(_currentSlice)) return;

        for (int i = 0; i < _orderedNotes[_currentSlice].Count; i++)
        {
            GameObject note = Instantiate(goNote, this.transform);
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
        currentSlice.text = (_currentSlice + 1).ToString();
        EraseNotes();
        DrawNotes();
    }

    private void HandleChangeLength(float length) => NotesManager.NoteLength = length;

    private void HandleChageClef()
    {
        _currentClef = _currentClef == Clef.G ? Clef.F : Clef.G;
        clefImage.sprite = _currentClef == Clef.G ? clefG : clefF;
        changeClefBtn.sprite = _currentClef == Clef.G ? clefF : clefG;
    }

    private void HandleChangeCurrentNote(string note)
    {
        currentNote.text = note;
    }

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



