using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }


    void Awake()
    {
        if(Instance == null)
        {
            Instance = new GameManager();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
       /* SaveInfos();
        for(int i = 0;i < PlayerManager.materials_name.Length; i++)
        {
            Debug.Log(PlayerPrefs.GetString("materials_name" + i));
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void SaveInfos()
    {
        PlayerPrefs.SetInt("currentXp", PlayerManager.CurrentXP);
        PlayerPrefs.SetInt("maxXP", PlayerManager.MaxXP);
        PlayerPrefs.SetInt("currentLevel", PlayerManager.CurrentLevel);

        for (int i = 0; i < PlayerManager.materials_name.Length; i++)
        {
            PlayerPrefs.SetString("materials_name" + i, PlayerManager.materials_name[i]);
        }


    }
}
