using UnityEngine;
using UnityEngine.UI;

public class WinningRuleRow : WinningRule
{
    private int rowNumber;

    public void Initialize (int fieldSizeColumns, int fieldSizeRows, int rowNumber)
    {
        this.rowNumber = rowNumber;
        base.Initialize(fieldSizeColumns, fieldSizeRows);
    }

    public override int CheckMatchAndHighlight(int[] combination, SpriteRenderer[] renderers, int[] symbolMultipliers, Color highlightColor)
    {
        int reward = 0;
        if (combination[this.GetFieldIndex(0, this.rowNumber)] == combination[this.GetFieldIndex(1, this.rowNumber)] &&
            combination[this.GetFieldIndex(0, this.rowNumber)] == combination[this.GetFieldIndex(2, this.rowNumber)])
        {
            // nasly se 3 kombinace
            // nacteme odpovidajici hodnotu symbolu
            reward = symbolMultipliers[combination[this.GetFieldIndex(0, this.rowNumber)]];

            // zvyraznime shodne symboly
            renderers[this.GetFieldIndex(0, this.rowNumber)].color = highlightColor;
            renderers[this.GetFieldIndex(1, this.rowNumber)].color = highlightColor;
            renderers[this.GetFieldIndex(2, this.rowNumber)].color = highlightColor;


            if (combination[this.GetFieldIndex(0, this.rowNumber)] == combination[this.GetFieldIndex(3, this.rowNumber)])
            {
                // i ctvrty symbol je steny
                // zdvojnasobime vyhru
                reward *= 2;

                // zvyraznime i posledni pole
                renderers[this.GetFieldIndex(3, this.rowNumber)].color = highlightColor;
            }
        }

        return reward;
    }
}
