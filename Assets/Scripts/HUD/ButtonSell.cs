using UnityEngine;
using System.Collections;

public class ButtonSell : MonoBehaviour
{

    public int ID;
    RecipeDatabase r;
    // Use this for initialization
    void Start()
    {
        r = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Sell()
    {
        Recipe _r = r.FetchRecipeByID(ID);

        if (PlayerManager.item_amount[ID] > 0)
        {
            PlayerManager.item_amount[ID]--;
            PlayerManager.Gold += _r.GoldGain;
        }

    }
}
