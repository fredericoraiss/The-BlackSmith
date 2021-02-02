using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListMaterials : MonoBehaviour {

    MaterialXML[] _m;
    public GameObject material;
    public int aux = 0;
    // Use this for initialization
    void Start () {

        MaterialsContainer m = MaterialsContainer.Load("Materials");
        _m = new MaterialXML[m.materialList.Count];
        m.materialList.CopyTo(_m);
        
        aux = m.materialList.Count;

        for(int i = 0; i < aux; i++)
        {
            GameObject mAux;
            mAux = Instantiate(material, new Vector3(), Quaternion.identity) as GameObject;
            mAux.transform.SetParent(gameObject.transform, false);

            mAux.transform.GetChild(0).GetComponent<Text>().text = _m[i].TheName;
            mAux.transform.GetChild(1).GetComponent<Text>().text = _m[i].Value.ToString();
            mAux.transform.GetChild(2).GetComponent<Text>().text = _m[i].Description;
        }
	}
	
	
}
