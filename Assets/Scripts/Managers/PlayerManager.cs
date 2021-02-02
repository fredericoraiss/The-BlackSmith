using UnityEngine;
using System.IO;
using LitJson;
using System.Collections;




public static class PlayerManager
{

    private static int gold = 1000;
    private static int currentXp;
    private static int maxXP = 10;
    private static int currentLevel = 1;
    public static string[] materials_name;
    public static int[] material_amount;
    public static string[] item_name;
    public static int[] item_amount;
 

    public static int Gold
    {
        get
        {
            return gold;
        }

        set
        {
            gold = value;
        }
    }

    public static int CurrentXP
    {
        get
        {
            return currentXp;
        }

        set
        {
            currentXp = value;
        }
    }

    public static int MaxXP
    {
        get
        {
            return maxXP;
        }

        set
        {
            maxXP = value;
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }

    public static void AddMaterial(int id, int amount)
    {
        material_amount[id] += amount;
    }

    public static void AddXP(int xp)
    {
        int aux = xp + currentXp;

        if(aux >= maxXP)
        {
            aux = aux - maxXP;
            currentLevel++;
            PlayerManager.MaxXP = (int)(PlayerManager.MaxXP * 1.3f);
            AddXP(aux);
        }
        else
        {
            currentXp = aux;
        }
        
    }

}
