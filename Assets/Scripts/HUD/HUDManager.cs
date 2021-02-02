using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

    public GameObject mercadoPainel;
    public GameObject receitasPainel;

    
    public Text XP;
    public Text Level;
    public Text Gold;
    public Image iconProgress;

    // Use this for initialization
    void Start () {
        receitasPainel = GameObject.Find("UI Completo").transform.Find("RecipeListToChoose").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        XP.text = PlayerManager.CurrentXP.ToString() + "/" + PlayerManager.MaxXP.ToString();
        Gold.text = "$" + PlayerManager.Gold.ToString();
        Level.text = PlayerManager.CurrentLevel.ToString();

        UpdateLevel();
    }
    public void PainelControle()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void AbrirMercado()
    {
        if (mercadoPainel.activeSelf)
        {
            mercadoPainel.SetActive(false);
        }
        else
        {
            mercadoPainel.SetActive(true);
        }
    }

    public void AbrirReceitas()
    {
        if (receitasPainel.activeSelf)
        {
            receitasPainel.SetActive(false);
        }
        else
        {
            receitasPainel.SetActive(true);
        }
    }

    void UpdateLevel()
    {
        if(PlayerManager.CurrentXP >= PlayerManager.MaxXP)
        {
            


            PlayerManager.CurrentLevel++;
            PlayerManager.CurrentXP = 0;
            
        }
    }
}
