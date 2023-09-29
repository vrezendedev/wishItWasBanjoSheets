using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{

    [Header("Required")]
    public GameObject Note;

    private int _currentSlice = 0;
    private Dictionary<int, List<Notation>> _orderedNotes;

    public static UnityAction<Notation> AddNote;
    public static UnityAction<int> ChangeSlice;
    private Vector2 _sheetSize;
    private float _noteSize;

    void Awake()
    {
        _orderedNotes = new Dictionary<int, List<Notation>>();
        _sheetSize = this.GetComponent<RectTransform>().sizeDelta;
        _noteSize = _sheetSize.y / 12;
    }

    void OnEnable()
    {
        AddNote += HandleAddNote;
        ChangeSlice += HandleChangeSlice;
    }

    void OnDisable()
    {
        AddNote -= HandleAddNote;
        ChangeSlice -= HandleChangeSlice;
    }

    private void DrawNotes()
    {
        for (int i = 0; i < _orderedNotes[_currentSlice].Count; i++)
        {
            GameObject note = Instantiate(Note, this.transform);
            RectTransform noteRT = note.GetComponent<RectTransform>();
            noteRT.sizeDelta = new Vector2(_noteSize, _sheetSize.y);
            noteRT.localPosition = new Vector3(_noteSize * i, 1);

            Debug.Log(noteRT.transform.localPosition);
            Note noteComponent = note.GetComponentInChildren<Note>();
            noteComponent.Init(null, _noteSize, new Vector2(1, _noteSize * _orderedNotes[_currentSlice][i].StaffIndex));
        }
    }

    private void EraseNotes()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);
    }

    private void HandleChangeSlice(int value) => _currentSlice += value;

    private void HandleAddNote(Notation notation)
    {
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
        }
    }

}



