using UnityEngine;
using System.Collections.Generic;

public class MatchInfo
{
    public List<GridItem> match;
    public int matchStartingX;
    public int matchEndingX;
    public int matchStartingY;
    public int matchEngindY;

    public bool validMatch
    {
        get { return match != null; }

    }
}
