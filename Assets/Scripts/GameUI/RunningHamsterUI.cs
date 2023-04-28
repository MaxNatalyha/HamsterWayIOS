using Services;
using SkinSystem;
using UnityEngine;
using Zenject;

namespace GameUI
{
    [RequireComponent(typeof(Animator))]
    public class RunningHamsterUI : MonoBehaviour
    {
        [SerializeField] private float _runningSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private RectTransform[] _wayPoints;
        
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Animator _animator;
        private ISkinsService _skinsService;

        private const string RUN_TRIGGER = "Run";

        private int _index;

        [Inject]
        public void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }

        private void OnEnable()
        {
            _animator.SetTrigger(RUN_TRIGGER);
            ApplySkin();
        }

        private void Update()
        {
            if (_wayPoints.Length == 0) return;
            
            _rect.position = Vector3.MoveTowards(transform.position, _wayPoints[_index].position,
                Time.deltaTime * _runningSpeed);

            var direction = (_wayPoints[_index].position - _rect.position).normalized;
            var rotation = Quaternion.LookRotation(Vector3.forward, direction);
            var zRotate = Quaternion.RotateTowards(_rect.rotation, rotation, _rotateSpeed * Time.deltaTime)
                .eulerAngles.z;

            _rect.rotation = Quaternion.Euler(0, 0, zRotate);

            float distance = Vector2.Distance(_rect.position, _wayPoints[_index].position);

            if (distance <= .2f)
            {
                if (_index == _wayPoints.Length - 1)
                {
                    _rect.position = _wayPoints[0].position;
                    _index = 1;
                    return;
                }

                _index++;
            }
        }

        private void ApplySkin()
        {
            var petSkin = _skinsService.GetSkin(SkinCategories.Pet);
            var petAsset = petSkin.GetCurrentAssetVariant<PetSkinAsset>();

            _animator.runtimeAnimatorController = petAsset.animator;
        }
        
        private void OnDrawGizmos()
        {
            if (_wayPoints == null || _wayPoints.Length == 0) return;
            
            Gizmos.color = Color.yellow;

            for (int i = 0; i < _wayPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[i + 1].position);
            }

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                Gizmos.DrawSphere(_wayPoints[i].position, 1);
            }
        }
    }
}
