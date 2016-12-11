using UnityEngine;
using System.Collections;
using System.IO;

public class saveTest : MonoBehaviour {
    LobsterContainer LC_LobItems;
	// Use this for initialization
	void Start ()
    {
        LobsterCollection lC_LobCol = new LobsterCollection();
        LobsterLevels ll_LobLevels = new LobsterLevels();
        LC_LobItems = new LobsterContainer();

        ll_LobLevels.b_levelNum = false;
        lC_LobCol.b_CollectNum = false;

        for (int i = 0; i < 5; i++)
            LC_LobItems.Levels.Add(ll_LobLevels);

        for (int i = 0; i < 20; i++)
            LC_LobItems.Collect.Add(lC_LobCol);

        LC_LobItems.Save(Path.Combine(Application.dataPath, "LobsterCont2.xml"));
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
