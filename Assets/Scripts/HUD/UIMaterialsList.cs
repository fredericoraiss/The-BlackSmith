using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaterialsList : MonoBehaviour
{

    public MaterialDatabase database;
    public GameObject UIMaterialInfo;
    public GameObject[] materialsUI;

    void Awake()
    {

    }

    void Start()
    {
        database = GameObject.Find("LoadJSON").GetComponent<MaterialDatabase>();
        materialsUI = new GameObject[database.materialData.Count];

        for (int i = 0; i < database.materialData.Count; i++)
        {
            Material materialItem = database.FetchMaterialByID(i);
        
            materialsUI[i] = Instantiate(UIMaterialInfo, Vector3.zero, Quaternion.identity) as GameObject;
            materialsUI[i].transform.SetParent(gameObject.transform, false);
            materialsUI[i].name = "Material " + materialItem.Title;

            materialsUI[i].transform.GetChild(0).GetComponent<Text>().text = materialItem.Title;
            materialsUI[i].transform.GetChild(1).GetComponent<Text>().text = PlayerManager.material_amount[i].ToString();
            materialsUI[i].transform.GetChild(2).GetComponent<Text>().text = materialItem.Description;
            materialsUI[i].transform.Find("BuyButton").GetComponent<BuyMaterial>().ID = i;
            materialsUI[i].transform.Find("BuyButton").gameObject.SetActive(true);
            materialsUI[i].transform.Find("Icon").GetComponent<Image>().sprite = materialItem.Sprite;
        }

        UpdateTeste();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTeste();
    }

    void UpdateTeste()
    {
        for (int i = 0; i < database.materialData.Count; i++)
        {
            Material materialItem = database.FetchMaterialByID(i);


            materialsUI[i].transform.GetChild(0).GetComponent<Text>().text = materialItem.Title;
            materialsUI[i].transform.GetChild(1).GetComponent<Text>().text = PlayerManager.material_amount[i].ToString();
            materialsUI[i].transform.GetChild(2).GetComponent<Text>().text = materialItem.Value_Buy.ToString();
        }
    }

    
}
