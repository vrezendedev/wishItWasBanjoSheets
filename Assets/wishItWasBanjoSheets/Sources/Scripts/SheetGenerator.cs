using UnityEngine;

public class SheetGenerator : MonoBehaviour
{
    [Header("Required")]
    public GameObject Staff;

    private float _staffWidth;
    private float _staffHeight;

    void Awake()
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        _staffWidth = rt.sizeDelta.x;
        _staffHeight = rt.sizeDelta.y / 12;
    }

    void Start() => GenerateStaffs();

    private void GenerateStaffs()
    {
        GenerateStaff(11, Color.white);

        for (int i = 10; i > -1; i--)
        {
            GenerateStaff(i, i % 2 == 0 ? Color.white : Color.black);
        }

        GenerateStaff(-1, Color.white);
    }

    private void GenerateStaff(int index, Color color)
    {
        GameObject staff = Instantiate(Staff, this.transform);
        Staff staffComponent = staff.GetComponent<Staff>();
        staff.name = $"Staff_Index_{index}";
        staffComponent.Init(index, _staffWidth, _staffHeight, color);
    }

}
