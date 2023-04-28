using System;
using SkinSystem;
using UnityEngine;
using Utilities;

namespace Pipeline
{
    public class Hamster : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private HamsterConfig _config;
    
        private bool _isActive;
        private int _currentWayPointIndex;
        
        private HamsterPath _hamsterPath;
        
        private Action _onWinAction;
        private Action _onLoseAction;

        private const string RUN_TRIGGER = "Run";
        private const string LOVE_TRIGGER = "Love";
        private const string CRY_TRIGGER = "Cry";

        private readonly ICustomLogger _customLogger = new DebugLogger();

        public void StartMoving(HamsterPath path)
        {
            _animator.SetTrigger(RUN_TRIGGER);
            _hamsterPath = path;
            _isActive = true;
            
            _customLogger.PrintInfo("Hamster", "Start moving");
        }

        public void SetUp(RectTransform parent)
        {
            _rect.SetParent(parent);
            gameObject.SetActive(true);
            _rect.position = parent.position;
        }

        public void BindActions(Action onWin, Action onLose)
        {
            _onWinAction = onWin;
            _onLoseAction = onLose;
        }

        public void SkipRun()
        {
            _currentWayPointIndex = _hamsterPath.wayPoints.Count - 2;
            _rect.position = _hamsterPath.wayPoints[_currentWayPointIndex].position;
        }

        public void CleanUp()
        {
            _isActive = false;
            _currentWayPointIndex = 0;
            _hamsterPath = null;
        }

        public void ApplySkin(ISkin petSkin)
        {
            var petAsset = petSkin.GetCurrentAssetVariant<PetSkinAsset>();
            
            _animator.runtimeAnimatorController = petAsset.animator;
        }

        private void Update()
        {
            if(!_isActive && _hamsterPath == null) return;

            _rect.position = Vector3.MoveTowards(transform.position, _hamsterPath.wayPoints[_currentWayPointIndex].position, Time.deltaTime * _config.runningSpeed);

            var direction = (_hamsterPath.wayPoints[_currentWayPointIndex].position - _rect.position).normalized;
            var rotation = Quaternion.LookRotation(Vector3.forward, direction);
            var zRotate = Quaternion.RotateTowards(_rect.rotation, rotation, _config.rotateSpeed * Time.deltaTime).eulerAngles.z;
            
            _rect.rotation = Quaternion.Euler(0, 0, zRotate);
            
            float distance = Vector2.Distance(_rect.position, _hamsterPath.wayPoints[_currentWayPointIndex].position);

            if (distance <= .2f)
            {
                if (LastWayPoint())
                {
                    _isActive = false;
                    _currentWayPointIndex = 0;
                    
                    if (_hamsterPath.isVictoriousPath)
                        Win();
                    else
                        Lose();

                    return;
                }
                _currentWayPointIndex++;
            }
            
            bool LastWayPoint()
            { 
                return _currentWayPointIndex == _hamsterPath.wayPoints.Count - 1;
            }
        }

        private void Win()
        {
            _customLogger.PrintInfo("Hamster", "Win");

            _animator.SetTrigger(LOVE_TRIGGER);
            _rect.rotation = Quaternion.Euler(Vector3.zero);
            _onWinAction?.Invoke();
        }

        private void Lose()
        {
            _customLogger.PrintInfo("Hamster", "Lose");

            _animator.SetTrigger(CRY_TRIGGER);
            _onLoseAction?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if(_hamsterPath!=null)
                VisualizePath(_hamsterPath);
        }

        private void VisualizePath(HamsterPath path)
        {
            if (path.isVictoriousPath) Gizmos.color = Color.green;

            for (int i = 0; i < path.wayPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(path.wayPoints[i].position, path.wayPoints[i + 1].position);
            }

            for (int i = 0; i < path.wayPoints.Count; i++)
            {
                Gizmos.DrawSphere(path.wayPoints[i].position, 2);
            }
        }
    }
}