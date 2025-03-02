using UnityEngine;

public abstract class WinningRule : ScriptableObject
{
    protected int fieldSizeColumns;
    protected int fieldSizeRows;

    public void Initialize(int fieldSizeColumns, int fieldSizeRows)
    {
        this.fieldSizeColumns = fieldSizeColumns;
        this.fieldSizeRows = fieldSizeRows;
    }

    // metoda zkontroluje, jestli se ve vygenerovane kombinaci nachazi nejake vyherni kombinace, pokud ano, tak prislusne symboly zvyrazni a na konci vrati koeficient, kterym se nasobi sazka
    public abstract int CheckMatchAndHighlight(int[] combination, SpriteRenderer[] renderers, int[] symbolMultipliers, Color highlightColor);

    // podle souracnice vyhleda v kombinaci odpovidajici hodnotu
    protected int GetFieldIndex(int col, int row)
    {
        return row * this.fieldSizeColumns + col;
    }
}
