using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventary : MonoBehaviour {

    public GameObject itemExample;
    public GameObject[] itensList;
    public RecipeDatabase r_data;
    

	// Use this for initialization
	void Start () {
        r_data = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
        itensList = new GameObject[r_data.recipeData.Count];

        for (int i = 0; i < r_data.recipeData.Count; i++)
        {
            Recipe recipe = r_data.FetchRecipeByID(i);

            itensList[i] = Instantiate(itemExample, Vector2.zero, Quaternion.identity) as GameObject;
            itensList[i].transform.SetParent(transform, false);
            itensList[i].name = "Item " + recipe.Title;

            itensList[i].transform.Find("Title").GetComponent<Text>().text = recipe.Title;
            itensList[i].transform.Find("Amount").GetComponent<Text>().text = PlayerManager.item_amount[i].ToString();
            itensList[i].transform.Find("Value").GetComponent<Text>().text = "$" + recipe.GoldGain.ToString();
            itensList[i].transform.Find("Icon").GetComponent<Image>().sprite = recipe.Sprite;
            itensList[i].transform.Find("Button").GetComponent<ButtonSell>().ID = recipe.ID;

        }
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < itensList.Length; i++)
        {
            itensList[i].transform.Find("Amount").GetComponent<Text>().text = PlayerManager.item_amount[i].ToString();
        }
	}
}
