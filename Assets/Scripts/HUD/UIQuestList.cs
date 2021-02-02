using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIQuestList : MonoBehaviour
{

    public QuestDatabase database;
    public RecipeDatabase r_database;
    public GameObject questItemModel;
    public GameObject[] questItemList;

    // Use this for initialization
    void Start()
    {

        database = GameObject.Find("LoadJSON").GetComponent<QuestDatabase>();
        r_database = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
        questItemList = new GameObject[database.questData.Count];

        for(int i = 0; i < database.questData.Count; i++)
        {
            Quest quest = database.FetchQuestByID(i);

            questItemList[i] = Instantiate(questItemModel, Vector3.zero, Quaternion.identity) as GameObject;
            questItemList[i].transform.SetParent(transform, false);
            questItemList[i].name = "Quest " + quest.Title;

            questItemList[i].transform.Find("Title").GetComponent<Text>().text = quest.Title;
            questItemList[i].transform.Find("Description").GetComponent<Text>().text = quest.Description;

            questItemList[i].transform.Find("GoldGain").transform.Find("Text").GetComponent<Text>().text = "$" + quest.GoldGain.ToString();
            questItemList[i].transform.Find("XpGain").transform.Find("Text").GetComponent<Text>().text = quest.XpGain.ToString();

            questItemList[i].transform.Find("Icon").transform.Find("icon").GetComponent<Image>().sprite = r_database.SearchSpriteByName(quest.ItemSlug_1);
            questItemList[i].transform.Find("Icon").transform.Find("Amount").GetComponent<Text>().text = quest.ItemAmount_1 +"x";

            questItemList[i].transform.GetComponent<QuestVerify>().ID = i;
            questItemList[i].transform.GetComponent<QuestVerify>().Name = r_database.FetchRecipeByNAME(quest.ItemNeed_1).Title;


        }

    }

    // Update is called once per frame
    void Update()
    {
       /* for(int i = 0; i < questItemList.Length; i++)
        {
            Quest q = database.FetchQuestByID(i);
            int id = r_database.FetchRecipeByNAME(q.ItemNeed_1).ID;

            if (PlayerManager.item_amount[id] >= q.ItemAmount_1)
            {
                //questItemList[i].transform.FindChild("Top").GetComponent<Image>().color = new Color(0,0,0,0);
                questItemList[i].transform.FindChild("Top").gameObject.SetActive(false);
            }
            else
            {
                //questItemList[i].transform.FindChild("Top").GetComponent<Image>().color = new Color(0, 0, 0, 180);
                questItemList[i].transform.FindChild("Top").gameObject.SetActive(true);
            }
        }*/
    }
}
