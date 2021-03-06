﻿using UnityEngine;
using System.Collections;

public class GridItem : MonoBehaviour
{

    public int x { get; private set; }
    public int y { get; private set; }
    public int id;

    public void OnItemPositionChanged(int newX, int newY)
    {
        x = newX;
        y = newY;
        gameObject.name = string.Format("Sprite [{0}][{1}]", x, y);
    }

    void OnMouseDown()
    {
        if(OnMouseOverItemEventhandler != null)
        {
            OnMouseOverItemEventhandler(this);
        }
    }

    public delegate void OnMouseOverItem(GridItem item);
    public static event OnMouseOverItem OnMouseOverItemEventhandler;
}
