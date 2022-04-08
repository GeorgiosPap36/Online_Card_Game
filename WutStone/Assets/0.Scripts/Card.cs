using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{

    public enum TypeOfCard
    {
        Minion,
        Spell,
    };
    public TypeOfCard cardType = TypeOfCard.Minion;

    public enum ArchTypes
    {
        Nothing,
        OnePiece,
        BokuNoHeroAcademia,
        Deathnote
    };
    public ArchTypes archType = ArchTypes.OnePiece;

    public enum Rarity
    {
        Legendary,
        Epic,
        Rare
    };
    public Rarity rarity = Rarity.Rare;

    public string cardName;

    [TextArea]
    [Tooltip("<b>word</b> for bold")]
    public string description;

    [Header("Artwork")]
    public Sprite artwork;
    public Vector2 offeset;
    public Vector2 scale = new Vector2(1, 1);
    public Color32 color;

    [Header("Stats")]
    public int mana;
    public int attack;
    public int health;

    public enum Skill
    {
        DealDamage,
        Scorch,
        Discover,
        Draw
    }

    [Header("Battelcry Settings")]
    public Skill[] battlecry;
    public int[] valuesForBattlecries;

    [Header("Passive Settings")]
    public Skill[] passive;
    public int[] valuesForPassives;

    [Header("Deathrattle Settings")]
    public Skill[] deathrattle;
    public int[] valuesForDeathrattlesl;

    [Header("Different Forms Of Card")]
    public Card[] forms;


    public bool canBePutInDeck = true;
}
