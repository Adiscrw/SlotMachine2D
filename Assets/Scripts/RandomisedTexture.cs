using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class RandomisedTexture : MonoBehaviour
{
    //[SerializeField] Button myButton;
    [System.Serializable]
    public class TextureProbability
    {
        public Texture texture;   // Textura
        [Range(0, 100)]
        public int probability; // Pravdìpodobnost 0-100
    }

    public int TilesCollumns; // pocet sloupcu dlazdic
    public int TilesRows; // pocet radku dlazdic
    public SpriteRenderer[] tiles; // Objekt, který mìní texturu
    public Sprite[] sprites; // Pole obrazku
    public int[] symbolMultipliers; // hodnoty jednotlivych symbolu
    public int[] spriteProbabilities; // Nastavene pravdepodobnosti jednotlivych symbolu (0-100)
    public Slider betSlider; // slider pro nastaveni sazky
    public TMP_Text betValue;
    public TMP_Text winValue;
    public TMP_Text totalBalance;
    public Button playButton;



    private const int SumOfProbabilities = 100;

    // pole pro generovani nahodneho vyberu - zde je kazdy symbol tolikrat, jakou ma pravdepodobnost
    private int[] spriteIndexes = new int[SumOfProbabilities];

    // jedna nahodne vygenerovana kombinace indexu symbolu
    private int[] generatedCombination;

    // vyherni pravidla
    private WinningRule[] winningRules;

    private Color highlightColor = Color.yellow;
    private Color backgroundColor = Color.white;

    void Start()
    {
        Debug.Assert(sprites.Length == spriteProbabilities.Length);
        Debug.Assert(TilesCollumns * TilesRows == tiles.Length);

        int totalProbability = 0;
        foreach (var spriteProbability in spriteProbabilities) 
        {
            totalProbability += spriteProbability;
        }

        Debug.Assert(totalProbability == SumOfProbabilities);

        generatedCombination = new int[TilesCollumns*TilesRows];
        int overallIndex = 0;
        for (int i = 0; i < sprites.Length; i++)
        {
            for (int j = 0; j < spriteProbabilities[i]; j++) 
            {
                spriteIndexes[overallIndex++] = i;
            }
        }

        // inicializace vyhernich pravidel
        winningRules = new WinningRule[4];
        var ruleRow = ScriptableObject.CreateInstance<WinningRuleRow>();
        ruleRow.Initialize(TilesCollumns, TilesRows, 0);
        winningRules[0] = ruleRow;

        ruleRow = ScriptableObject.CreateInstance<WinningRuleRow>();
        ruleRow.Initialize(TilesCollumns, TilesRows, 1);
        winningRules[1] = ruleRow;

        ruleRow = ScriptableObject.CreateInstance<WinningRuleRow>();
        ruleRow.Initialize(TilesCollumns, TilesRows, 2);
        winningRules[2] = ruleRow;

        var ruleDiagonal1 = ScriptableObject.CreateInstance<WinningRuleDiagonal1>();
        ruleDiagonal1.Initialize(TilesCollumns, TilesRows);
        winningRules[3] = ruleDiagonal1;

        // uvodni inicializace dlazdic

        // vygenerovani nahodne kombinace symbolu
        GenerateRandomCombination(generatedCombination, spriteIndexes);

        // zobrazeni vygeerovane kombinace symbolu
        DisplaySymbols(generatedCombination, tiles, sprites);
        
        // vsem nastavime zakladni barvu
        ResetSpriteColors(tiles);

        this.DisplayGameValues();
    }


    public void AssignRandomTexture()
    {
        StartCoroutine(PlayOneRound());
    }
    private IEnumerator PlayOneRound()
    {
        playButton.interactable = false;

        GlogalData.TotalScore -= GlogalData.Bet;
        GlogalData.Win = 0;

        this.DisplayGameValues();

        yield return StartCoroutine(WaitAndContinue(2f));

        // nejdriv vygenerujeme dalsi, ted uz soutezni kombinaci

        // vygenerovani nahodne kombinace symbolu
        GenerateRandomCombination(generatedCombination, spriteIndexes);

        // zobrazeni vygeerovane kombinace symbolu
        DisplaySymbols(generatedCombination, tiles, sprites);

        // pak projdeme vygenerovanou kombinaci a budeme hledat splneni definovanych pravidel
        int reward = 0;
        foreach(var rule in winningRules)
        {
            reward += rule.CheckMatchAndHighlight(generatedCombination, tiles, symbolMultipliers, highlightColor);
        }

        int totalWin = reward * GlogalData.Bet;
        winValue.text += totalWin.ToString();

        yield return StartCoroutine(WaitAndContinue(2f));

        GlogalData.TotalScore += totalWin;
        GlogalData.Bet = 1;
        GlogalData.Win = 0;
        betSlider.value = 1;

        this.DisplayGameValues();

        ResetSpriteColors(tiles);

        playButton.interactable = true;
    }

    private void GenerateRandomCombination(int[] combination, int[] symbolIndexes)
    {
        int randomIndex;
        for (int i=0; i<combination.Length; i++)
        {
            randomIndex = ((int)(Random.value * 100)); // Náhodné èíslo mezi 0 a 1
            combination[i] = symbolIndexes[randomIndex];
        }
    }

    private void DisplaySymbols(int[] combinations, SpriteRenderer[] renderers, Sprite[] symbols)
    {
        for (int i=0; i < combinations.Length; i++) 
        {
            renderers[i].sprite = symbols[combinations[i]];
        }
    }

    private void ResetSpriteColors(SpriteRenderer[] renderers)
    {
        foreach(var renderer in renderers)
        {
            renderer.color = backgroundColor;
        }
    }

    private void DisplayGameValues()
    {
        winValue.text = GlogalData.Win.ToString();
        totalBalance.text = GlogalData.TotalScore.ToString();
        betValue.text = GlogalData.Bet.ToString();
    }

    private IEnumerator WaitAndContinue(float value)
    {
        yield return new WaitForSeconds(value); // Poèká 1 sekundu
    }

}