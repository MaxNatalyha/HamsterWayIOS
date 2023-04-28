using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectionButtonView : MonoBehaviour
    {
        public Button selectButton;
        public TMP_Text levelIdText;
        public Image[] starsImages = new Image[3];
        public Sprite activeStar, inActiveStar;
        public RectTransform lockPanel;
        public Action OnEnableAction;

        private void OnEnable()
        {
            OnEnableAction?.Invoke();
        }
    }
}