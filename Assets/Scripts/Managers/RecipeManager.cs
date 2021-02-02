using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecipeManager : MonoBehaviour
{

    public SavePlayerData saveData;
    RecipeDatabase database;
    public GameObject recipePanel;
    public GameObject progressBar;
    

    public Text DEBUG;

    private int item_id;
    private string title_recipe;
    private string materialNeed_1;
    private string materialNeed_2;
    private int amountNeed_1;
    private int amountNeed_2;
    public float cost_recipe;
    private int xpgain_recipe;
    private int goldgain_recipe;

    public int[] aux;


    public float countHammer = 0;


    // Use this for initialization
    void Start()
    {
        saveData = GameObject.Find("LoadJSON").GetComponent<SavePlayerData>();
        
        database = GameObject.Find("LoadJSON").GetComponent<RecipeDatabase>();
        //UpdateRecipe(0);

        aux = new int[2];
    }

    void Update()
    {
        DEBUG.text ="Touch Cont: " + countHammer.ToString();
       
    }

    public void UpdateRecipe(int id)
    {
        Recipe recipe = database.FetchRecipeByID(id);
       /* transform.FindChild("Title_Recipe").GetComponent<Text>().text = recipe.Title;
        transform.FindChild("Material1_Recipe").GetComponent<Text>().text = recipe.MaterialTitle_1 + " x" + recipe.NumberMaterial1;
        transform.FindChild("Material2_Recipe").GetComponent<Text>().text = recipe.MaterialTitle_2 + " x" + recipe.NumberMaterial2;
        transform.FindChild("Cost_Recipe").GetComponent<Text>().text = "Touch Cost: "+ recipe.Cost.ToString();*/
        progressBar.transform.Find("_Icon").transform.Find("Icon").GetComponent<Image>().sprite = recipe.Sprite;

        this.item_id = recipe.ID;
        title_recipe = recipe.Title;
        materialNeed_1 = recipe.MaterialTitle_1;
        materialNeed_2 = recipe.MaterialTitle_2;
        amountNeed_1 = recipe.NumberMaterial1;
        amountNeed_2 = recipe.NumberMaterial2;
        cost_recipe = recipe.Cost;
        xpgain_recipe = recipe.XpGain;
        goldgain_recipe = recipe.GoldGain;


    }

    public void MakeRecipe()
    {
        
        if (countHammer == 0)
        {
            for (int i = 0; i < PlayerManager.materials_name.Length; i++)
            {

                if (PlayerManager.materials_name[i].Contains(materialNeed_1))
                {
                    aux[0] = i;
                    //PlayerManager.material_amount[aux[0]] = 2;

                    //Debug.Log(PlayerManager.materials_name[aux[0]] + ": mat1 :" + materialNeed_1);
                    //Debug.Log("quantidade 1: " + PlayerManager.material_amount[aux[0]]);
                }
                if (PlayerManager.materials_name[i].Contains(materialNeed_2))
                {
                    aux[1] = i;
                    //PlayerManager.material_amount[aux[1]] = 2;
                    
                    //Debug.Log(PlayerManager.materials_name[aux[1]] + ": mat2 :" + materialNeed_2);
                    //Debug.Log(" quantidade 2: " + PlayerManager.material_amount[aux[1]]);
                }

            }

            if (PlayerManager.material_amount[aux[0]] >= amountNeed_1 && PlayerManager.material_amount[aux[1]] >= amountNeed_2)
            {

                int aux1 = PlayerManager.material_amount[aux[0]] - amountNeed_1;
                int aux2 = PlayerManager.material_amount[aux[1]] - amountNeed_2;

                Debug.Log(aux1);
                Debug.Log(aux2);

                if (aux1 >= 0 && aux2 >= 0)
                {
                    progressBar.SetActive(true);
                    countHammer++;
                    PlayerManager.material_amount[aux[0]] = aux1;
                    PlayerManager.material_amount[aux[1]] = aux2;
                    
                }
                //Debug.Log("DEPOIS quantidade 1: " + PlayerManager.material_amount[aux[0]] + " quantidade 2: " + PlayerManager.material_amount[aux[1]]);
            }

            else
            {
                Debug.Log("Não tem material");
            }

        }

        else if (countHammer > 0 && countHammer < (cost_recipe -1))
        {
            countHammer++;
        }

        else if (countHammer >= (cost_recipe - 1))
        {
            progressBar.SetActive(false);
            Debug.Log(title_recipe + " criado com sucesso!");
            countHammer = 0;
            PlayerManager.item_amount[item_id]++;
            //PlayerManager.Gold += goldgain_recipe;
            PlayerManager.AddXP(xpgain_recipe);
            saveData.SavePlayerInfo();
            
        }

        ProgressBar();

    }

    void ProgressBar()
    {
        float aux = countHammer / cost_recipe;
        Debug.Log(aux);
        UpdateProgressBar(aux);
        
    }
    void UpdateProgressBar(float value)
    {
        Transform bar = progressBar.transform.Find("Bar");
        bar.localScale = new Vector3(Mathf.Clamp(value, 0f, 1f), bar.localScale.y, bar.localScale.z);
            
    }

}

