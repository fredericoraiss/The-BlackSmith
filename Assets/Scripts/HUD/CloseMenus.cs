using UnityEngine;
using System.Collections;

public class CloseMenus : MonoBehaviour
{

    public GameObject[] paineis;


    public void ManipularPaineis(int id)
    {
        for (int i = 0; i < paineis.Length; i++)
        {
            if (i == id)
            {
                if (paineis[i].activeSelf)
                {
                    paineis[i].SetActive(false);
                }
                else
                {
                    paineis[i].SetActive(true);
                }

            }
            else
            {
                paineis[i].SetActive(false);
            }
        }
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
}
