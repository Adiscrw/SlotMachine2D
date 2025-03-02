using UnityEngine;
using UnityEngine.UI;

public class WinningRuleDiagonal1 : WinningRule
{
    public override int CheckMatchAndHighlight(int[] combination, SpriteRenderer[] renderers, int[] symbolMultipliers, Color highlightColor)
    {
        int reward = 0;
        if (combination[this.GetFieldIndex(0, 0)] == combination[this.GetFieldIndex(1, 1)] &&
            combination[this.GetFieldIndex(0, 0)] == combination[this.GetFieldIndex(2, 2)])
        {
            // nasly se 3 kombinace
            // nacteme odpovidajici hodnotu symbolu
            reward = symbolMultipliers[combination[this.GetFieldIndex(0, 0)]];

            // zvyraznime shodne symboly
            renderers[this.GetFieldIndex(0, 0)].color = highlightColor;
            renderers[this.GetFieldIndex(1, 1)].color = highlightColor;
            renderers[this.GetFieldIndex(2, 2)].color = highlightColor;
        }

        return reward;
    }
}
