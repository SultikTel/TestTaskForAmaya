using DG.Tweening;
using Finder.Gameplay.Items;
using System;
using UnityEngine;

namespace Finder.Gameplay
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _contentSprite;
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private GameObject _highLite;
        [SerializeField] private ParticleSystem _particleSystem;
        private bool _isInteractable;
        private string _value;
        private Vector2 _scaleImage, _scaleCell;

        public event Action<Cell> OnCellClicked;
        public string Value => _value;

        public void SetInteractable(bool canInteract) => _isInteractable = canInteract;

        private void Start()
        {
            _scaleImage = _contentSprite.transform.localScale;
            _scaleCell = transform.localScale;
        }

        public void SetupCell(CardData cardData)
        {
            _value = cardData.Identifier;
            _contentSprite.sprite = cardData.Sprite;
            _contentSprite.transform.rotation = Quaternion.identity;

            if (cardData.NeededRotation != Rotation.Static)
                _contentSprite.transform.Rotate(0, 0, GetRotationAngle(cardData.NeededRotation));

            ChangeBackgrounColorRandom();
        }

        private float GetRotationAngle(Rotation rotation) =>
            rotation switch
            {
                Rotation.Left => 90f,
                Rotation.Right => -90f,
                Rotation.UpsideDown => 180f,
                _ => 0f
            };

        #region Cell appearance

        public void MakeBounceEffect() {
            transform.DOScale(Vector3.zero, 0.2f)
                .OnComplete(() => transform.DOScale(1.2f * _scaleCell, 0.2f)
                .OnComplete(() => transform.DOScale(0.95f * _scaleCell, 0.1f)
                .OnComplete(() => transform.DOScale(_scaleCell, 0.1f))));
        }

        private void ChangeBackgrounColorRandom() =>
            _background.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

        #endregion

        #region Mouse Events

        private void OnMouseEnter()
        {
            if (!_isInteractable) return;
            _highLite.SetActive(true);
            _contentSprite.transform.DOScale(_scaleImage * 1.1f, 0.3f).SetEase(Ease.OutBack);
        }

        private void OnMouseExit()
        {
            _highLite.SetActive(false);
            _contentSprite.transform.DOScale(_scaleImage, 0.3f).SetEase(Ease.InBack);
        }

        private void OnMouseDown()
        {
            if (!_isInteractable) return;
            OnCellClicked?.Invoke(this);
        }

        #endregion

        #region After answer visual callback

        public void Shake() =>
            _contentSprite.transform.DOShakePosition(1f, new Vector3(0.2f, 0, 0)).SetEase(Ease.InBounce)
            .OnComplete(() => _contentSprite.transform.DOLocalMove(Vector3.zero, 1f));

        public void RightCellPressed()
        {
            _contentSprite.transform.DOScale(0.5f * _scaleImage, 0.2f)
                .OnComplete(() => _contentSprite.transform.DOScale(1.30f * _scaleImage, 0.1f)
                .OnComplete(() => _contentSprite.transform.DOScale(0.95f * _scaleImage, 0.1f)
                .OnComplete(() => _contentSprite.transform.DOScale(_scaleImage, 0.1f))));
            Destroy(Instantiate(_particleSystem, _contentSprite.transform.position, Quaternion.identity).gameObject, 2f);
        }

        #endregion
    }
}