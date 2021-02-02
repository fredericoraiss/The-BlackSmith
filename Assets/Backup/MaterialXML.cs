using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;


public class MaterialXML {
            
    [XmlAttribute("name")]
    public string TheName { get; set; }

    [XmlElement(ElementName = "Value")]
    public float Value { get; set; }

    [XmlElement(ElementName = "Description")]
    public string Description { get; set; }

 
}
