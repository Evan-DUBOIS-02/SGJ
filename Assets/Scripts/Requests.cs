using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum SelectedAnswer { None, A, B };

public class Requests : MonoBehaviour
{
    [Header("Description")]
    [TextArea(7, 10)]
    public string description;

    [Header("Answer A")]
    public string answerA;
    public List<GameObject> AListHide = new List<GameObject>();
    public List<GameObject> AListShow = new List<GameObject>();
    public int AarchitecturalPoints;
    public int AlandscapedPoints;
    public int AecologicalPoints;

    [Header("Answer B")]
    public string answerB;
    public List<GameObject> BListHide = new List<GameObject>();
    public List<GameObject> BListShow = new List<GameObject>();
    public int BarchitecturalPoints;
    public int BlandscapedPoints;
    public int BecologicalPoints;

    [NonSerialized]
    public SelectedAnswer sa = SelectedAnswer.None;

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
            sa = SelectedAnswer.A;
        }
        else if (answerId == 2)
        {
            returnTAB[0].AddRange(BListHide);
            returnTAB[1].AddRange(BListShow); 
            sa = SelectedAnswer.B;
        }

        return returnTAB;  
    }
}
