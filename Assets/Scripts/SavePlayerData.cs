using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class SavePlayerData : MonoBehaviour
{

    public InfoPlayer infoPlayer = new InfoPlayer(PlayerManager.CurrentLevel, PlayerManager.CurrentXP, PlayerManager.MaxXP, PlayerManager.material_amount);
    JsonData infoJson;


    // Use this for initialization
    void Start()
    {

        //infoJson = JsonMapper.ToJson(infoPlayer);
        // File.WriteAllText(Application.dataPath + "/PlayerInfo.json", infoJson.ToString());
        //LoadPlayerInfo();
    }

    public void SavePlayerInfo()
    {
        infoPlayer = new InfoPlayer(PlayerManager.CurrentLevel, PlayerManager.CurrentXP, PlayerManager.MaxXP, PlayerManager.material_amount); 
        infoJson = JsonMapper.ToJson(infoPlayer);
        File.WriteAllText(Application.dataPath + "/PlayerInfo.json", infoJson.ToString());
    }

    public void LoadPlayerInfo()
    {

        string jsn = File.ReadAllText(Application.dataPath + "/PlayerInfo.json");//Resources.Load<TextAsset>("PlayerInfo").text;

        if (jsn != null)
            infoJson = JsonMapper.ToObject(jsn);

        PlayerManager.CurrentLevel = (int)infoJson["Level"];
        PlayerManager.CurrentXP = (int)infoJson["currentXp"];
        PlayerManager.MaxXP = (int)infoJson["maxXp"];

        for (int i = 0; i < PlayerManager.material_amount.Length; i++)
        {
            int aux = (int)infoJson["material_amount"][i];
            PlayerManager.material_amount[i] = aux;
        }
            


    }
}

public class InfoPlayer
{
    public int Level;
    public int currentXp;
    public int maxXp;
    public int[] material_amount;

    public InfoPlayer(int level, int currentxp, int maxxp, int[] materialamount)
    {
        this.Level = level;
        this.currentXp = currentxp;
        this.maxXp = maxxp;
        this.material_amount = materialamount;

    }
}
