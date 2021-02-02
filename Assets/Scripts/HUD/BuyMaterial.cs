using UnityEngine;
using System.Collections;

public class BuyMaterial : MonoBehaviour {

    public MaterialDatabase database;
    public int ID;

    
    void Start () {
        database = GameObject.Find("LoadJSON").GetComponent<MaterialDatabase>();
	}
	
	
	void Update () {
	
	}

    public void ButtonBUY()
    {
        Material materialItem = database.FetchMaterialByID(ID);

        if (PlayerManager.Gold >= materialItem.Value_Buy)
        {
            PlayerManager.material_amount[ID]++;
            PlayerManager.Gold -= materialItem.Value_Buy;
        }
        else
        {
            Debug.Log("Compra nao feita");
        }
    }
}
