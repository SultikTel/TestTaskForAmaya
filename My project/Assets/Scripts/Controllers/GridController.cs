using Finder.Configs;
using Finder.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace Finder.Controllers
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private Transform _camera;
        private Cell[,] cells;
        private List<Cell> cellPool = new List<Cell>();

        public Cell[,] GenerateGrid(Difficulties difficulties, bool IsNewGame)
        {
            int width = difficulties.Width;
            int height = difficulties.Height;
            Vector3 cellScale = _cellPrefab.transform.localScale;
            cells = new Cell[width, height];
            foreach (var cell in cellPool)
            {
                cell.gameObject.SetActive(false);
            }
            int cellIndex = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Cell cell;
                    if (cellIndex < cellPool.Count)
                    {
                        cell = cellPool[cellIndex];
                        cell.gameObject.SetActive(true);
                    }
                    else
                    {
                        cell = Instantiate(_cellPrefab, new Vector3(i * cellScale.x, j * cellScale.y), Quaternion.identity);
                        cellPool.Add(cell);
                        cell.transform.SetParent(transform);
                    }
                    if (IsNewGame)
                    {
                        cell.MakeBounceEffect();
                    }
                    cells[i, j] = cell;
                    cell.transform.position = new Vector3(i * cellScale.x, j * cellScale.y);
                    cell.SetInteractable(true);
                    cellIndex++;
                }
            }
            _camera.position = new Vector3((float)(width / 2) * cellScale.x, (float)(height / 2) * cellScale.y, -10f);
            return cells;
        }

        public void SetGridInteractable(bool canInteract)
        {
            foreach(Cell cell in cells)
            {
                cell.SetInteractable(canInteract);
            }
        }
    }
}