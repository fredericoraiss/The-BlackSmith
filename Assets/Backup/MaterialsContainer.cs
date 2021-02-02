using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[XmlRoot("MaterialsCollection")]
public class MaterialsContainer {

    [XmlArray("Materials")]
    [XmlArrayItem("Material")]

    public List<MaterialXML> materialList = new List<MaterialXML>();

	public static MaterialsContainer Load(string path)
    {

        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(MaterialsContainer));
        StringReader reader = new StringReader(_xml.text);
        MaterialsContainer material = serializer.Deserialize(reader) as MaterialsContainer;

        

        reader.Close();
        return material;

    }
	
	
}
