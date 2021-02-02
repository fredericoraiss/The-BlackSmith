using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRecipeList : MonoBehaviour
{

    public RecipeDatabase database;
    public MaterialDatabase m_database;
    public GameObject UIRecipeInfo;
    public GameObject[] recipeList;

    // Use this for initialization
    void Start()
    {
        database = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
        m_database = GameObject.Find("LoadJSON").GetComponent<MaterialDatabase>();
        recipeList = new GameObject[database.recipeData.Count];

        for (int i = 0; i < database.recipeData.Count; i++)
        {
            Recipe recipe = database.FetchRecipeByID(i);

            recipeList[i] = Instantiate(UIRecipeInfo, Vector3.zero, Quaternion.identity) as GameObject;
            recipeList[i].transform.SetParent(gameObject.transform, false);
            recipeList[i].name = "Recipe " + recipe.Title;

            recipeList[i].transform.Find("txt_title").GetComponent<Text>().text = recipe.Title;
            recipeList[i].transform.Find("txt_xpgain").GetComponent<Text>().text = "exp: " + recipe.XpGain.ToString();
            recipeList[i].transform.Find("txt_goldgain").GetComponent<Text>().text = "gold: " + recipe.GoldGain.ToString();

            //recipeList[i].transform.FindChild("img_material");

            #region Material ICON
            if (recipe.Type.Equals("basic"))
            {
                recipeList[i].transform.Find("img_material_1").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_1);
                recipeList[i].transform.Find("img_material_1").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial1.ToString() + "x";
                recipeList[i].transform.Find("img_material_1").gameObject.SetActive(true);

                recipeList[i].transform.Find("img_material_2").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_2);
                recipeList[i].transform.Find("img_material_2").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial2.ToString() + "x";
                recipeList[i].transform.Find("img_material_2").gameObject.SetActive(true);
            }
            else if (recipe.Type.Equals("rare"))
            {
                recipeList[i].transform.Find("img_material_1").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_1);
                recipeList[i].transform.Find("img_material_1").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial1.ToString() + "x";
                recipeList[i].transform.Find("img_material_1").gameObject.SetActive(true);

                recipeList[i].transform.Find("img_material_2").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_2);
                recipeList[i].transform.Find("img_material_2").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial2.ToString() + "x";
                recipeList[i].transform.Find("img_material_2").gameObject.SetActive(true);
                /*
                recipeList[i].transform.FindChild("img_material_3").transform.FindChild("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_3);
                recipeList[i].transform.FindChild("img_material_3").transform.FindChild("amount_material").GetComponent<Text>().text = recipe.NumberMaterial3.ToString() + "x";
                recipeList[i].transform.FindChild("img_material_3").gameObject.SetActive(true);*/
            }
            else if (recipe.Type.Equals("ultrarare") || recipe.Type.Equals("divine"))
            {
                recipeList[i].transform.Find("img_material_1").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_1);
                recipeList[i].transform.Find("img_material_1").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial1.ToString() + "x";
                recipeList[i].transform.Find("img_material_1").gameObject.SetActive(true);

                recipeList[i].transform.Find("img_material_2").transform.Find("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_2);
                recipeList[i].transform.Find("img_material_2").transform.Find("amount_material").GetComponent<Text>().text = recipe.NumberMaterial2.ToString() + "x";
                recipeList[i].transform.Find("img_material_2").gameObject.SetActive(true);
                /*
                recipeList[i].transform.FindChild("img_material_3").transform.FindChild("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_3);
                recipeList[i].transform.FindChild("img_material_3").transform.FindChild("amount_material").GetComponent<Text>().text = recipe.NumberMaterial3.ToString() + "x";
                recipeList[i].transform.FindChild("img_material_3").gameObject.SetActive(true);

                recipeList[i].transform.FindChild("img_material_4").transform.FindChild("icon").GetComponent<Image>().sprite = m_database.SearchSpriteByName(recipe.MaterialSlug_4);
                recipeList[i].transform.FindChild("img_material_4").transform.FindChild("amount_material").GetComponent<Text>().text = recipe.NumberMaterial4.ToString() + "x";
                recipeList[i].transform.FindChild("img_material_4").gameObject.SetActive(true);*/
            }

            #endregion


            recipeList[i].transform.Find("button_make").GetComponent<RecipeChoose>().ID = i;
            recipeList[i].transform.Find("button_make").GetComponent<RecipeChoose>().gameObject.SetActive(true);
            recipeList[i].transform.Find("button_make").transform.Find("icon").GetComponent<Image>().sprite = recipe.Sprite;
            /* recipeList[i].transform.GetChild(0).GetComponent<Text>().text = recipe.Title;
             recipeList[i].transform.GetChild(1).GetComponent<Text>().text = "XP Gain: " + recipe.XpGain + "\nGold Gain:" + recipe.GoldGain;
             recipeList[i].transform.GetChild(2).GetComponent<Text>().text = recipe.MaterialTitle_1 + " x" + recipe.NumberMaterial1;
             recipeList[i].transform.GetChild(3).GetComponent<Text>().text = recipe.MaterialTitle_2 + " x" + recipe.NumberMaterial2;
             recipeList[i].transform.GetChild(4).GetComponent<RecipeChoose>().ID = i;
             recipeList[i].transform.GetChild(4).GetComponent<RecipeChoose>().gameObject.SetActive(true);

             */
        }
    }


}
