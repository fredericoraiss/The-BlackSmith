using UnityEngine;
using System.Collections;

public class QuestVerify : MonoBehaviour {

    public int ID;
    public string Name;
    private QuestDatabase q;
    private RecipeDatabase r;

	// Use this for initialization
	void Start () {
        q = GameObject.Find("LoadJSON").GetComponent<QuestDatabase>();
        r = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CheckQuest()
    {
        Quest _q = q.FetchQuestByID(ID);
        Recipe _r = r.FetchRecipeByNAME(Name);

        if(PlayerManager.item_amount[_r.ID] >= _q.ItemAmount_1)
        {
            PlayerManager.Gold += _q.GoldGain;
            PlayerManager.AddXP(_q.XpGain);
            PlayerManager.item_amount[_r.ID] -= _q.ItemAmount_1;
        }
        else
        {
            Debug.Log("Não tem quantidade certa dos itens da Quest");
        }

    }
}
