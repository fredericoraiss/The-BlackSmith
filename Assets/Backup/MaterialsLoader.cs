using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeField]
public class MaterialsLoader : MonoBehaviour
{
    
    public string path = "Materials";
    [SerializeField] public MaterialXML[] m;

    void Start()
    {
        MaterialsContainer mc = MaterialsContainer.Load(path);

        m = new MaterialXML[mc.materialList.Count];
        mc.materialList.CopyTo(m);
/*
        for(int i = 0; i < m.Length; i++)
        {
            Debug.Log(m[i].description.ToString());
            Debug.Log(m[i].value.ToString());
            Debug.Log(m[i].aName.ToString());
        }
        */
        foreach(MaterialXML material in mc.materialList)
        {
            print(mc.materialList.ToString());
            print(material.TheName);
            print(material.Value.ToString());
            print(material.Description);
        }
    }
}
