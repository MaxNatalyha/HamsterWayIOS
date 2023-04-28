using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class LevelSelectionLockAnim : MonoBehaviour
    {
        private Button _button;
        private Animator _lockAnimator;

        private const string LOCK_TRIGGER = "Lock";

        private void Awake()
        {
            _button = GetComponent<Button>();
            _lockAnimator = GetComponent<Animator>();
            
            _button.onClick.AddListener(() => _lockAnimator.SetTrigger(LOCK_TRIGGER));
            _lockAnimator.keepAnimatorStateOnDisable = true;
        }

    }
}