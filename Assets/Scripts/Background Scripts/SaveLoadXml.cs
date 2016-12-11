using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class LobsterMain
{
    public LobsterLevels levels;
    public LobsterCollection coll;
}

public class LobsterLevels
{
    [XmlAttribute("Level")]
    public bool b_levelNum;
}

public class LobsterCollection
{
    [XmlAttribute("Collectables")]
    public bool b_CollectNum;
}

[XmlRoot("LobsterMain")]
public class LobsterContainer
{
    [XmlArray("Levels")]
    [XmlArrayItem("LevelsUnlocked")]
    public List<LobsterLevels> Levels = new List<LobsterLevels>();
    [XmlArrayItem("CollsUnlocked")]
    public List<LobsterCollection> Collect = new List<LobsterCollection>();

    public void Save(string s_filepath)
    {
        XmlSerializer serial = new XmlSerializer(typeof(LobsterContainer));
        using (FileStream stream = new FileStream(s_filepath, FileMode.Create))
        {
            serial.Serialize(stream, this);
        }
    }

    static public LobsterContainer load(string s_filepath)
    {
        XmlSerializer serial = new XmlSerializer(typeof(LobsterContainer));
        using (FileStream stream = new FileStream(s_filepath, FileMode.Open))
        {
            return serial.Deserialize(stream) as LobsterContainer;
        }
    }
}

