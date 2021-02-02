using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class MaterialDatabase : MonoBehaviour
{
    public List<Material> database = new List<Material>();
    public JsonData materialData;
    public Sprite[] material_sheet;

    void Awake()
    {
        material_sheet = Resources.LoadAll<Sprite>("Sheet_Material");
    }

    void Start()
    {

        string jsn = Resources.Load<TextAsset>("Materials").text;

        materialData = JsonMapper.ToObject(jsn); //"jar:file://" + Application.dataPath + "!/Materials.json"));

        
        ConstructMaterialDatabase();

        PlayerManager.materials_name = new string[materialData.Count];
        PlayerManager.material_amount = new int[materialData.Count];
        for (int i = 0; i < materialData.Count; i++)
        {
            PlayerManager.materials_name[i] = materialData[i]["title"].ToString();
            //Debug.Log(PlayerManager.materials_name[i]);
        }

        //Debug.Log(FetchMaterialByID(1).Description);
    }

    public Material FetchMaterialByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if(database[i].ID == id)
                return database[i];

        return null;
    }

    public Sprite SearchSpriteByName(string name)
    {
        Sprite aux;

        for(int i  =0; i <material_sheet.Length; i++)
        {
            if (material_sheet[i].name.Equals(name))
            {
                aux = material_sheet[i];
                return aux;
            }
        }
        return null;
    }

    void ConstructMaterialDatabase()
    {
        for(int i = 0 ; i < materialData.Count; i++)
        {
            database.Add(new Material(
                (int)materialData[i]["id"], 
                materialData[i]["title"].ToString(), 
                (int)materialData[i]["value_sell"], 
                (int)materialData[i]["value_buy"], 
                materialData[i]["description"].ToString(),
                (bool)materialData[i]["stackable"],
                materialData[i]["slug"].ToString(),
                SearchSpriteByName(materialData[i]["slug"].ToString())
                ));
        }
    }

}

public class Material
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value_Sell { get; set; }
    public int Value_Buy { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }


    public Material(int id, string title, int value_sell, int value_buy, string description, bool stackable, string slug, Sprite sprite)
    {
        this.ID = id;
        this.Title = title;
        this.Value_Sell = value_sell;
        this.Value_Buy = value_buy;
        this.Description = description;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Sprite = sprite;

    }
    public Material()
    {
        ID = -1;
    }

}
