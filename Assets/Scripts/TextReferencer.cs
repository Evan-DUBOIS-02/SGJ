using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class TextReferencer : MonoBehaviour
    {
        [SerializeField]
        private List<FrenchToEnglishSwitcher> textSwitchers = new List<FrenchToEnglishSwitcher>();

        public void SwitchAllTextToFrench()
        {
            foreach (var switcher in textSwitchers)
            {
                switcher.SetToFrench();
            }
        }

        public void SwitchAllTextToEnglish()
        {
            foreach (var switcher in textSwitchers)
            {
                switcher.SetToEnglish();
            }
        }
    }
}