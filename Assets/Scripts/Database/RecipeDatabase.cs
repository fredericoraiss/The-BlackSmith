using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class RecipeDatabase : MonoBehaviour
{

    public List<Recipe> database = new List<Recipe>();
    public JsonData recipeData;

    public Sprite[] recipe_sheet;

    void Awake()
    {
        recipe_sheet = Resources.LoadAll<Sprite>("Sheet_Item");
    }

    // Use this for initialization
    void Start()
    {
        string jsn = Resources.Load<TextAsset>("Recipe").text;
        recipeData = JsonMapper.ToObject(jsn);//File.ReadAllText(Application.dataPath + "/StreamingAssets/Recipe.json"));
        ConstructRecipeDatabase();

        PlayerManager.item_name = new string[recipeData.Count];
        PlayerManager.item_amount = new int[recipeData.Count];

        for(int i = 0;  i < recipeData.Count; i++)
        {
            PlayerManager.item_name[i] = recipeData[i]["title"].ToString();
        }

        //Debug.Log(FetchRecipeByID(0).Title);

    }

    public Sprite SearchSpriteByName(string name)
    {
        Sprite aux;

        for (int i = 0; i < recipe_sheet.Length; i++)
        {
            if (recipe_sheet[i].name.Equals(name))
            {
                aux = recipe_sheet[i];
                return aux;
            }
        }
        return null;
    }

    public Recipe FetchRecipeByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];

        return null;
    }

    public Recipe FetchRecipeByNAME(string name)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].Title.Equals(name))
                return database[i];

        return null;
    }



    void ConstructRecipeDatabase()
    {
        for (int i = 0; i < recipeData.Count; i++)
        {
            database.Add(new Recipe(
                (int)recipeData[i]["id"],
                recipeData[i]["title"].ToString(),
                recipeData[i]["type"].ToString(),
                recipeData[i]["materials"]["material_title_1"].ToString(),
                recipeData[i]["materials"]["material_slug_1"].ToString(),
                recipeData[i]["materials"]["material_title_2"].ToString(),
                recipeData[i]["materials"]["material_slug_2"].ToString(),
                (int)recipeData[i]["materials"]["number_material1"],
                (int)recipeData[i]["materials"]["number_material2"],
                (int)recipeData[i]["cost"],
                recipeData[i]["slug"].ToString(),
                (int)recipeData[i]["xpgain"],
                (int)recipeData[i]["goldgain"],
                SearchSpriteByName(recipeData[i]["slug"].ToString())
                ));
        }
    }

}

public class Recipe
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string MaterialTitle_1 { get; set; }
    public string MaterialSlug_1 { get; set; }
    public string MaterialTitle_2 { get; set; }
    public string MaterialSlug_2 { get; set; }
    public int NumberMaterial1 { get; set; }
    public int NumberMaterial2 { get; set; }
    //public string Material_3 { get; set; }
    public int Cost { get; set; }
    public string Slug { get; set; }
    public int XpGain { get; set; }
    public int GoldGain { get; set; }
    public Sprite Sprite { get; set; }

    public Recipe(int id, string title, string type, string material_title_1,string material_slug_1, string material_title_2,string material_slug_2, int numberMaterial1, int numberMaterial2, int cost, string slug, int xpgain, int goldgain, Sprite sprite)
    {
        this.ID = id;
        this.Title = title;
        this.Type = type;

        this.MaterialTitle_1 = material_title_1;
        this.MaterialSlug_1 = material_slug_1;

        this.MaterialTitle_2 = material_title_2;
        this.MaterialSlug_2 = material_slug_2;

        this.NumberMaterial1 = numberMaterial1;
        this.NumberMaterial2 = numberMaterial2;

        this.Cost = cost;
        this.Slug = slug;
        this.XpGain = xpgain;
        this.GoldGain = goldgain;
        this.Sprite = sprite;
        
    }

    public Recipe()
    {
        this.ID = -1;
    }
}
