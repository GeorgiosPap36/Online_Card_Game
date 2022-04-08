using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShowCardsForDeckBuilding : MonoBehaviour
{

    private TMP_Dropdown drop;
    private TMP_Dropdown sortDrop;

    private TMP_InputField searchInput;

    public List<Object> activeFilter;

    private List<Object>[] filterLists;

    [SerializeField]
    public Filter filter;

    private int cardsCounter;
    private int page;
    private int maxPages;
   

    void Start()
    {
        drop = transform.parent.Find("Dropdown").GetComponent<TMP_Dropdown>();
        sortDrop = transform.parent.Find("SortDropdown").GetComponent<TMP_Dropdown>();
        searchInput = transform.parent.Find("SearchInput").GetComponent<TMP_InputField>();
        activeFilter = new List<Object>();
        SetUpFilterLists();
        SetUp(filterLists[drop.value]);
    }

    void SetUpFilterLists()
    {
        filterLists = new List<Object>[filter.Filters.Length];
        for (int i = 0; i < filterLists.Length; i++)
        {
            filterLists[i] = new List<Object>(Resources.LoadAll("Cards/" + filter.Filters[i], typeof(Card)));
        }
    }

    void SetUpActiveFilter(List<Object> list)
    {
        activeFilter.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            activeFilter.Add(list[i]);
        }
    }

    void SetUp(List<Object> list)
    {
        cardsCounter = 0;
        page = 0;
        SetUpActiveFilter(ListWithStringInDescription(list));
        SetUpCards(activeFilter, sortDrop.value);
        CalculateMaxNumberOfPages(activeFilter);
        WhichCardsAreShown(activeFilter);
    }

    void WhichCardsAreShown(List<Object> list)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).GetComponent<DisplayCard>().card = list[cardsCounter] as Card;
            transform.transform.GetChild(i).GetComponent<DisplayCard>().Initialization();
            if (i + transform.childCount * page < list.Count - 1)
            {
                cardsCounter++;
            }
            else if (i + transform.childCount * page > list.Count - 1)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void CalculateMaxNumberOfPages(List<Object> list)
    {
        maxPages = 0;
        int temp = list.Count % transform.childCount;
        if (temp != 0)
        {
            maxPages = 1;
        }
        maxPages += list.Count / transform.childCount;
    }

    void FilterCards(List<Object> list)
    {
        SetUp(list);
    }

    void SetUpCards(List<Object> list, int k)
    {
        list.Sort(SortByCardName);
        if (k == 1)
        {
            list.Sort(SortByCardMana);
        }
        else if (k == 2)
        {
            list.Sort(SortByCardAttack);
        }
        else if (k == 3)
        {
            list.Sort(SortByCardHealth);
        }
    }

    List<Object> ListWithStringInDescription(List<Object> list)
    {
        string s = searchInput.text;
        if (s.Length > 0)
        {
            List<Object> tempList = new List<Object>();
            activeFilter.Clear();
            Card temp;
            for (int i = 0; i < list.Count; i++)
            {
                temp = list[i] as Card;
                if (temp.description.ToLower().Contains(s.ToLower()) || temp.cardName.ToLower().Contains(s.ToLower()))
                {
                    tempList.Add(temp);
                }
            }
            if (tempList.Count > 0)
            {
                return tempList;
            }
            
        }
        return list;
    }

    //Shorts
    static int SortByCardName(Object b1, Object b2)
    {
        Card c1 = b1 as Card;
        Card c2 = b2 as Card;
        return c1.name.CompareTo(c2.name);
    }

    static int SortByCardMana(Object b1, Object b2)
    {
        Card c1 = b1 as Card;
        Card c2 = b2 as Card;
        return c1.mana.CompareTo(c2.mana);
    }

    static int SortByCardAttack(Object b1, Object b2)
    {
        Card c1 = b1 as Card;
        Card c2 = b2 as Card;
        return (-c1.attack).CompareTo(-c2.attack);
    }

    static int SortByCardHealth(Object b1, Object b2)
    {
        Card c1 = b1 as Card;
        Card c2 = b2 as Card;
        return (-c1.health).CompareTo(-c2.health);
    }


    //UI
    public void PreviousButton()
    {
        if (page >= 1)
        {
            page--;
            cardsCounter = page * transform.childCount;
            WhichCardsAreShown(activeFilter);
        }
    }

    public void NextButton()
    {
        if (page < maxPages - 1)
        {
            page++;
            WhichCardsAreShown(activeFilter);
        }
    }

    public void DropDown()
    {
        FilterCards(filterLists[drop.value]);
    }

    public void SortDropDown()
    {
        FilterCards(filterLists[drop.value]);
    }

    public void SearchForCardWithDescription()
    {
        SetUp(filterLists[drop.value]);
    }
}
