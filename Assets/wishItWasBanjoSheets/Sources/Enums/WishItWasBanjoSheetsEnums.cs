using System.Collections.Generic;

public static class WishItWasBanjoSheetsEnums
{
    public enum Clef
    {
        G = 0,
        F = 1
    }

    public static Dictionary<int, string> GClefNoteByStaffIndex = new Dictionary<int, string>
    {
       {-1 , "C3"},
       {0 , "D3"},
       {1 , "E3"},
       {2 , "F3"},
       {3 , "G3"},
       {4 , "A3"},
       {5 , "B3"},
       {6 , "C4"},
       {7 , "D4"},
       {8 , "E4"},
       {9 , "F4"},
       {10 , "G4"},
       {11 , "A4"}
    };


    public static Dictionary<int, string> FClefNoteByStaffIndex = new Dictionary<int, string>
    {
       {-1 , "E1"},
       {0 , "F1"},
       {1 , "G1"},
       {2 , "A1"},
       {3 , "B1"},
       {4 , "C2"},
       {5 , "D2"},
       {6 , "E2"},
       {7 , "F2"},
       {8 , "G2"},
       {9 , "A2"},
       {10 , "B2"},
       {11 , "C3"}
    };
}
