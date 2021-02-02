using UnityEngine;
using System.Collections;

public class RecipeChoose : MonoBehaviour {

    public int ID;
    RecipeManager rm;
    GameObject painel;

	// Use this for initialization
	void Start () {
        painel = GameObject.Find("RecipeListToChoose");
        rm = GameObject.Find("Principal").GetComponent<RecipeManager>();
	}
	
	public void ChooseRecipe()
    {
        rm.UpdateRecipe(ID);

        if (painel.activeSelf)
        {
            painel.SetActive(false);
        }
        else
        {
            painel.SetActive(true);
        }
    }
}
