using UnityEngine;
using UnityEngine.UI;

public class ClefHandler : MonoBehaviour
{
    public Button Clef;

    void OnEnable()
    {
        Clef.onClick.AddListener(delegate { ChangeClef(); });
    }

    void OnDisable()
    {
        Clef.onClick.RemoveAllListeners();
    }

    private void ChangeClef() => NotesManager.ChangeClef();
}
