
[System.Serializable]
public class PlayerDat
{

    public string[,] deckLists;
    public string[] deckNames;

    public int[] deckSize;
    

    public PlayerDat (Decks decks)
    {
        deckLists = new string[decks.deckMaxCards, 10];
        deckNames = new string[10];

        deckSize = new int[10];

        for (int i = 0; i < 10; i++)
        {
            deckSize[i] = decks.deckLists[i].Count;
            for (int j = 0; j < deckSize[i]; j++)
            {
                deckLists[j, i] = decks.deckLists[i][j].name;
            }
            deckNames[i] = decks.deckNames[i];
        }
    }

}
