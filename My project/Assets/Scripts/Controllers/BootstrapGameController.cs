using Finder.Configs;
using Finder.Gameplay;
using Finder.Helper;
using System.Collections;
using UnityEngine;

namespace Finder.Controllers
{
    public class BootstrapGameController : MonoBehaviour
    {
        [SerializeField] private GridController _gridController;
        [SerializeField] private ConfigController _configController;
        [SerializeField] private UIController _uIController;
        private AnswerFlowController _answerFlowController = new AnswerFlowController();
        private GridFiller _gridFiller = new GridFiller();

        private void Start()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            _configController.NewGameStart();
            _answerFlowController.ResetAnswer();
            _uIController.SetEndGamePanelDisplay(false);
            SetNewLevel(true);
        }

        public void StartGameAgain()
        {
            StartCoroutine(FakeLoadingScreen());
        }

        private IEnumerator FakeLoadingScreen()
        {
            _uIController.SetVisibleLoadingPanel(true);
            //can replace when loading of cell will be heavy.Now it makes it almost momental
            yield return new WaitForSeconds(4f);
            _uIController.SetVisibleLoadingPanel(false);
            StartNewGame();
        }

        public void NextLevel()
        {
            StartCoroutine(DelayForParticlesBeforeNextLevel());
        }

        private IEnumerator DelayForParticlesBeforeNextLevel()
        {
            _gridController.SetGridInteractable(false);
            yield return new WaitForSeconds(0.5f);

            if (!_configController.ImproveDifficult())
            {
                EndGame();
            }
            else
            {
                SetNewLevel(false);
                _gridController.SetGridInteractable(true);
            }
        }

        private void SetNewLevel(bool isNewGame)
        {
            Cell[,] grid = _gridController.GenerateGrid(_configController.GetCurrentDifficulty(), isNewGame);
            CardBundleData randomBundle = _configController.GetRandomBundle();
            string answer = _answerFlowController.GenerateAnswer(_gridFiller.FillGrid(grid, randomBundle));
            _uIController.SetQuestion(answer, isNewGame);
        }

        private void EndGame()
        {
            _uIController.SetEndGamePanelDisplay(true);
        }

        #region subscribe and unsubscribe

        private void OnEnable()
        {
            _uIController.RestartButton.onClick.AddListener(StartGameAgain);
            _answerFlowController.RightAnswered += NextLevel;
        }

        private void OnDisable()
        {
            _uIController.RestartButton.onClick.RemoveListener(StartGameAgain);
            _answerFlowController.RightAnswered -= NextLevel;
        }

        #endregion
    }
}