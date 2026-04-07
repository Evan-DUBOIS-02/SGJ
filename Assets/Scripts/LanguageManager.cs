using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LanguageManager: MonoBehaviour
    {
        [NonSerialized]
        public bool isFrench = true;
        [NonSerialized]
        public bool isEnglish = false;
        
        private LanguageSwitcher[]  languageSwitchers;

        private void Start()
        {
            languageSwitchers = FindObjectsByType<LanguageSwitcher>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        }

        public void SwitchAllTextToFrench()
        {
            isFrench = true;
            isEnglish = false;
            foreach (var switcher in languageSwitchers)
            {
                switcher.SwitchToFrenchMode();
            }
        }

        public void SwitchAllTextToEnglish()
        {
            isFrench = false;
            isEnglish = true;
            foreach (var switcher in languageSwitchers)
            {
                switcher.SwitchToEnglishMode();
            }
        }
    }
}