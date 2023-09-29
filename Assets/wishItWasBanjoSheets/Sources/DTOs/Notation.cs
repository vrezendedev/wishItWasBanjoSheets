
using UnityEngine;

public readonly struct Notation
{
    public Notation(int staffIndex, float length, Vector2 position)
    {
        StaffIndex = staffIndex;
        Length = length;
        Position = position;
    }
    public readonly int StaffIndex;
    public readonly float Length;
    public readonly Vector2 Position;

    public override string ToString() => $"{StaffIndex} with length of {Length} and position of {Position.ToString()}";
}
