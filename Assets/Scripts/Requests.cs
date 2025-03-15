using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Requests : MonoBehaviour
{
    [Header("Donnees requete")]
    public string description;
    public string answerA;
    public string answerB;
    public enum SelectedAnswer {None, A, B};

    public List<GameObject> AListHide = new List<GameObject>();
    public List<GameObject> AListShow = new List<GameObject>();
    public List<GameObject> BListHide = new List<GameObject>();
    public List<GameObject> BListShow = new List<GameObject>();

    private GameObject goDescription;
    private GameObject goBtnA;
    private GameObject goBtnB;

    private void Start()
    {
        /*
        GameObject[] childrens = GetComponentsInChildren<GameObject>();
        goDescription = childrens[0];
        goBtnA = childrens[1];
        goBtnB = childrens[2];

        goDescription.GetComponent<Text>().text = description;
        goBtnA.GetComponent<Text>().text = answerA;
        goBtnB.GetComponent<Text>().text = answerB;
        */
    }

    public List<GameObject>[] SetSA(int answerId) //1 = answer A ; 2 = answerB
    {
        // Initialisation du tableau de listes
        List<GameObject>[] returnTAB = new List<GameObject>[2];

        // Initialisation des listes dans chaque case du tableau
        returnTAB[0] = new List<GameObject>();
        returnTAB[1] = new List<GameObject>();

        if (answerId == 1)
        {
            returnTAB[0].AddRange(AListHide);
            returnTAB[1].AddRange(AListShow);
        }
        else if (answerId == 2)
        {
            returnTAB[0].AddRange(BListHide);
            returnTAB[1].AddRange(BListShow); 
        }

        return returnTAB;  
    }
}
