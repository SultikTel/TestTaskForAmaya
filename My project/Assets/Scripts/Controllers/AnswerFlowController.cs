using Finder.Gameplay;
using System;
using System.Collections.Generic;

namespace Finder.Controllers
{
    public class AnswerFlowController
    {
        private List<string> _answers = new List<string>();
        private string _currentAnswer;
        public event Action RightAnswered;

        public void ResetAnswer() => _answers.Clear();

        public void CheckAnswer(Cell obj)
        {
            if (obj.Value == _currentAnswer)
            {
                obj.RightCellPressed();
                _answers.Add(_currentAnswer);
                RightAnswered?.Invoke();
            }
            else
            {
                obj.Shake();
            }
        }

        public string GenerateAnswer(Cell[,] cells)
        {
            List<string> availableAnswers = new List<string>();
            foreach (Cell cell in cells)
            {
                if (!_answers.Contains(cell.Value))
                {
                    availableAnswers.Add(cell.Value);
                }

                cell.OnCellClicked -= CheckAnswer;
                cell.OnCellClicked += CheckAnswer;
            }
            if (availableAnswers.Count > 0)
            {
                _currentAnswer = availableAnswers[UnityEngine.Random.Range(0, availableAnswers.Count)];
            }
            else
            {
                _currentAnswer = null;
            }
            return _currentAnswer;
        }
    }
}