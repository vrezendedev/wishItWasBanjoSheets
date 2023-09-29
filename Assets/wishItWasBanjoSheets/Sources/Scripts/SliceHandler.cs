using UnityEngine;
using UnityEngine.UI;

public class SliceHandler : MonoBehaviour
{
    public Button SliceUp;
    public Button SliceDown;

    void OnEnable()
    {
        SliceUp.onClick.AddListener(delegate { ChangeSlice(1); });
        SliceDown.onClick.AddListener(delegate { ChangeSlice(-1); });
    }

    void OnDisable()
    {
        SliceUp.onClick.RemoveAllListeners();
        SliceDown.onClick.RemoveAllListeners();
    }

    private void ChangeSlice(int value) => NotesManager.ChangeSlice(value);

}
