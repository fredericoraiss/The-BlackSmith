using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class QuestDatabase : MonoBehaviour
{

    public List<Quest> database = new List<Quest>();
    public JsonData questData;
    //public Sprite[] quest_sheet;

    // Use this for initialization
    void Start()
    {
        string jsn = Resources.Load<TextAsset>("Quest").text;
        questData = JsonMapper.ToObject(jsn);

        ConstructQuestDatabase();

    }

    public Quest FetchQuestByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];

        return null;
    }

    void ConstructQuestDatabase()
    {
        for (int i = 0; i < questData.Count; i++)
        {
            database.Add(new Quest(
                (int)questData[i]["id"],
                questData[i]["title"].ToString(),
                questData[i]["description"].ToString(),
                questData[i]["type"].ToString(),
                questData[i]["itens"]["item_need_1"].ToString(),
                (int)questData[i]["itens"]["item_amount_1"],
                questData[i]["itens"]["item_slug_1"].ToString(),
                questData[i]["slug"].ToString(),
                (int)questData[i]["xpgain"],
                (int)questData[i]["goldgain"]
                ));
        }


    }
}

public class Quest
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    public string ItemNeed_1 { get; set; }
    public int ItemAmount_1 { get; set; }
    public string ItemSlug_1 { get; set; }

    /*public string ItemNeed_2 { get; set; }
    public string ItemAmount_2 { get; set; }
    public string ItemSlug_2 { get; set; }
    public string ItemNeed_3 { get; set; }
    public string ItemAmount_3 { get; set; }
    public string ItemSlug_3 { get; set; }
    public string ItemNeed_4 { get; set; }
    public string ItemAmount_4 { get; set; }
    public string ItemSlug_4 { get; set; }*/

    public string Slug { get; set; }
    public int XpGain { get; set; }
    public int GoldGain { get; set; }

    public Quest(int id, string title, string description, string type, string itemneed1, int itemamount1, string itemslug1, string slug, int xpgain, int goldgain)
    {

        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Type = type;
        this.ItemNeed_1 = itemneed1;
        this.ItemAmount_1 = itemamount1;
        this.ItemSlug_1 = itemslug1;
        this.Slug = slug;
        this.XpGain = xpgain;
        this.GoldGain = goldgain;

    }
}
