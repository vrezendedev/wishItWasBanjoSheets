public readonly struct Notation
{
    public Notation(int staffIndex, float length)
    {
        StaffIndex = staffIndex;
        Length = length;
    }

    public readonly int StaffIndex;
    public readonly float Length;

    public override string ToString() => $"{StaffIndex} with length of {Length}";
}
