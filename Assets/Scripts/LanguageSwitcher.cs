using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LanguageSwitcher: MonoBehaviour
    {
        [SerializeField] [TextArea(7, 10)] private string french;
        [SerializeField] [TextArea(7, 10)] private string english;
        
        private TMP_Text textField;
        
        LanguageManager languageManager;

        private void Start()
        {
            textField = GetComponent<TMP_Text>();
            languageManager = FindFirstObjectByType<LanguageManager>();
            if(languageManager.isFrench)
                SwitchToFrenchMode();
            else if(languageManager.isEnglish)
                SwitchToEnglishMode();
        }

        private void OnEnable()
        {
            if (languageManager == null)
                return;
            
            if(languageManager.isFrench)
                SwitchToFrenchMode();
            else if(languageManager.isEnglish)
                SwitchToEnglishMode();
        }

        public void SwitchToFrenchMode()
        {
            textField.SetText(french);
        }
        
        public void SwitchToEnglishMode()
        {
            textField.SetText(english);
        }
    }
}