using Finder.Configs;
using Finder.Gameplay;
using Finder.Gameplay.Items;
using UnityEngine;

namespace Finder.Helper
{
    public class GridFiller
    {
        public Cell[,] FillGrid(Cell[,] cells, CardBundleData cardBundleData)
        {
            CardData[] cardDataArray = cardBundleData.CardData;
            System.Random rng = new System.Random();
            int n = cardDataArray.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                CardData temp = cardDataArray[n];
                cardDataArray[n] = cardDataArray[k];
                cardDataArray[k] = temp;
            }
            int width = cells.GetLength(0);
            int height = cells.GetLength(1);
            int index = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (index >= cardDataArray.Length)
                    {
                        Debug.LogWarning("Not enough card data to fill the grid without repetitions!");
                        return cells;
                    }

                    Cell cell = cells[i, j];
                    CardData cardData = cardDataArray[index++];
                    cell.SetupCell(cardData);
                }
            }
            return cells;
        }
    }
}