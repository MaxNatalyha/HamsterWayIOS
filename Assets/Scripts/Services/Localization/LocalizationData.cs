using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Services/LocalizationData")]
    public class LocalizationData : ScriptableObject
    {
        public LanguageEnum DefaultLanguage;
        public List<LocalizationUnit> LocalizationUnits;
    }

    [Serializable]
    public class LocalizationUnit
    {
        public string UnitKey;
        [Space(10)]
        public string Russian;
        public string Srpski;
        public string English;
        public string Turkish;
        public string Italian;
        public string French;
        public string German;
        public string Dutch;
        public string Spanish;
        public string Portuguese;
        public string Japanese;
        public string Chinese;
        public string Vietnamese;
        public string Korean;
    }
}