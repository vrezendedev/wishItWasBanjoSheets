using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{

    [Header("Required")]
    public GameObject Note;

    private int _currentSlice = 0;
    private Dictionary<int, List<Notation>> _orderedNotes;
    private Dictionary<int, Stack<Notation>> _notesHistoric;

    public static UnityAction<Notation> AddNote;
    public static UnityAction<int> ChangeSlice;
    private Vector2 _sheetSize;
    private float _noteSize;

    void Awake()
    {
        _orderedNotes = new Dictionary<int, List<Notation>>();
        _notesHistoric = new Dictionary<int, Stack<Notation>>();
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

    private void HandleChangeSlice(int value) => _currentSlice += value;

    private void HandleAddNote(Notation notation)
    {
        if (_orderedNotes.ContainsKey(_currentSlice))
        {
            _orderedNotes[_currentSlice].Add(notation);
            _notesHistoric[_currentSlice].Push(notation);
        }
        else
        {
            _orderedNotes.Add(_currentSlice, new List<Notation>());
            _orderedNotes[_currentSlice].Add(notation);
            _notesHistoric.Add(_currentSlice, new Stack<Notation>());
            _notesHistoric[_currentSlice].Push(notation);
        }

        SortNotes();
        EraseNotes();
        DrawNotes();
    }

    //NEED REFACTOR!
    //Maybe not Draw every single note each time, but draw the new one... and draw using the Screen position...
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

    private void HandleUndo()
    {
        if (_notesHistoric.ContainsKey(_currentSlice))
            if (_notesHistoric[_currentSlice].Count > 0)
                _orderedNotes[_currentSlice].Remove(_notesHistoric[_currentSlice].Pop());
    }

    private void SortNotes() => _orderedNotes[_currentSlice].Sort((x, y) => x.Position.x.CompareTo(y.Position.x));

}



