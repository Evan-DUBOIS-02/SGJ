using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class FrenchToEnglishSwitcher : MonoBehaviour
    {
        [SerializeField][TextArea(7, 10)]
        private String frenchText;
        [SerializeField][TextArea(7, 10)]
        private String englishText;

        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }

        public void SetToFrench()
        {
            text.SetText(frenchText);
        }

        public void SetToEnglish()
        {
            text.SetText(englishText);
        }
    }
}