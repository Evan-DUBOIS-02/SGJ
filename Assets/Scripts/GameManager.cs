using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private List<Requests> listRequest = new List<Requests>();
    private int currentRequest;
    

    public void PressButtonA()
    {
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(1);

        for (int i = 0; i < tabGO[0].Count; i++)
        {
            //tabGO[0][i].SetTriggerAnim;
            tabGO[0][i].SetActive(false);
        }

        //WaitForSeconds();

        for (int i = 0; i < tabGO[1].Count; i++)
        {
            //tabGO[1][i].SetTriggerAnim;
            tabGO[1][i].SetActive(true);
        }
    }

    public void PressButtonB() 
    {
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(2);
    }
}
