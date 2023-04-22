//Import System
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A struct to hold the cost of a card.
/// </summary>
[System.Serializable]
public struct Cost
{
    public int ore;
    public int clay;
    public int hay;
    public int sheep;
    public int gold;
    public int wood;

    public override string ToString()
         {
              return "Ore: " + ore + "\nClay: " + clay + "\nHay: " + hay + "\nSheep: " + sheep + "\nGold: " + gold + "\nWood: " + wood;
         }
}
