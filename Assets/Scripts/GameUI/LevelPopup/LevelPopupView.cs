using System.Collections;
using Services.Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class LevelPopupView : MonoBehaviour
    {
        public TMP_Text loseReasonText;
        public Button menuButton, restartButton, continueButton, nextLevelButton;
        public RectTransform settingsBlock, rewardBlock, starsBlock;
        public ValueDisplay rewardField;
        public RewardStars rewardStars;
        public RectTransform content;
        public RewardAdButton adsButton;
        
        public void StartDisplayStarsRoutine(int reward, int progress)
        {
            StartCoroutine(DisplayStarsRoutine(reward, progress));
        }
        
        private IEnumerator DisplayStarsRoutine(int rewardAmount, int starAmount)
        {
            yield return new WaitForSeconds(.5f);
        
            rewardField.UpdateField(rewardAmount);

            yield return new WaitForSeconds(.7f);

            if (starAmount != 0 && !(starAmount > 3))
            {
                rewardStars.SetWithAnimation(starAmount);
            }
        }
    }
}