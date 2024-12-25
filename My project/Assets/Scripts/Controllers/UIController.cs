using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Finder.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private GameObject _loadingImage;
        [SerializeField] private Image _curtain;
        [SerializeField] private Button _restartButton;

        public Button RestartButton => _restartButton;

        public void SetQuestion(string answer, bool isNeedToAppear) 
        { 
            _questionText.text = $"Find {answer}";
            if(isNeedToAppear) _questionText.DOFade(1f, 2f);
        }

        public void SetEndGamePanelDisplay(bool isShow)
        {
            _curtain.DOFade(isShow ? 0.5f : 0f, 2f);
            if(!isShow) _questionText.DOFade(0f, 0f);
            _restartButton.gameObject.SetActive(isShow);
        }

        public void SetVisibleLoadingPanel(bool isVisable)
        {
            _loadingImage.SetActive(isVisable);
        }
    }
}