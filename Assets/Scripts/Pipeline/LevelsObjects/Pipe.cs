using System;
using SkinSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Pipeline
{
    public class Pipe : ConnectableObject, IPointerClickHandler
    {
        [Header("Pipe")]
        [SerializeField] private Image _backPipeLayer;
        
        private float _newRotateAngle;
        private const float AngularSpeed = 25f;

        private void Start()
        {
            SetRandomRotation();
        }

        public void ApplySkin(ISkin pipesSkin)
        {
            var pipesAsset = pipesSkin.GetCurrentAssetVariant<PipesSkinAsset>();
            
            _viewImage.sprite = ObjectConfig.gameObjectType switch
            {
                GameObjectTypes.StraightPipe => pipesAsset.straightPipe,
                GameObjectTypes.CornerPipe => pipesAsset.cornerPipe,
                GameObjectTypes.StraightCrossPipe => pipesAsset.straightCrossPipe,
                GameObjectTypes.CornerCrossPipe => pipesAsset.cornerCrossPipe,
                _ => throw new ArgumentOutOfRangeException()
            };

            _backPipeLayer.color = pipesAsset.backColor;

            _viewImage.color = pipesSkin.HasColorVariants ? pipesSkin.GetCurrentColorVariant() : Color.white;
        }

        private void SetRandomRotation()
        {
            int randomAngle = Random.Range(0, 4) * 90;

            _newRotateAngle = randomAngle;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Rotate();
        }

        private void FixedUpdate()
        {
            _contentContainer.rotation = Quaternion.Lerp(_contentContainer.rotation, Quaternion.Euler(new Vector3(0, 0, _newRotateAngle)),
                Time.fixedDeltaTime * AngularSpeed);
        }

        private void Rotate()
        {
            _newRotateAngle += 90;
        }
    }
}
