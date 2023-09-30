using UnityEngine;
using UnityEngine.UI;

public class NoteLengthHandler : MonoBehaviour
{
    public Button Whole;
    public Button Half;
    public Button Quarter;
    public Button Eighth;

    void OnEnable()
    {
        Whole.onClick.AddListener(delegate { ChangeLength(1f); });
        Half.onClick.AddListener(delegate { ChangeLength(2f); });
        Quarter.onClick.AddListener(delegate { ChangeLength(8f); });
        Eighth.onClick.AddListener(delegate { ChangeLength(16f); });
    }

    void OnDisable()
    {
        Whole.onClick.RemoveAllListeners();
        Half.onClick.RemoveAllListeners();
        Quarter.onClick.RemoveAllListeners();
        Eighth.onClick.RemoveAllListeners();
    }

    private void ChangeLength(float value) => NotesManager.ChangeLength(value);
}
