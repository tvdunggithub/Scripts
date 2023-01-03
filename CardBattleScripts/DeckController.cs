using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;
    public List<CardSO> deckToUse = new List<CardSO>();
    public List<CardSO> activeCard = new List<CardSO>();
    public Card cardToSpawn;
    public int manaCostToDraw = 2;
    public float timeBetweenDrawingCards = 0.25f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        SetupDeck();
    }


    public void SetupDeck()
    {
        activeCard.Clear();
        List<CardSO> temDeck = new List<CardSO>();
        temDeck.AddRange(deckToUse);

        while(temDeck.Count > 0)
        {
            int selected = Random.Range(0, temDeck.Count);
            activeCard.Add(temDeck[selected]);
            temDeck.RemoveAt(selected);
        }
    }

    public void DrawCard()
    {
        if(activeCard.Count == 0)
        {
            SetupDeck();
        }
        //Card dc kế thừa từ mono ko thể dùng new để khởi tạo nên phải
        //xài instantiate trên 1 đối tượng(prefab) có sẵn sau đó set lại các thuộc tính
        Card newCard = Instantiate(cardToSpawn, transform.position, transform.rotation);
        newCard.cardSO = activeCard[0];
        newCard.SetupCard();
        activeCard.RemoveAt(0);
        AudioController.instance.sfx[3].Play();
        HandController.instance.AddCardToHand(newCard);
    }

    public void DrawCardForMana()
    {
        if(BattleController.instance.playerMana >= manaCostToDraw)
        {
            DrawCard();
            BattleController.instance.SpendMana(manaCostToDraw);
        }else
        {
            UIController.instance.ShowWarningMessage();
        }
    }

    public void DrawMultipleCards(int amountToDraw)
    {
            StartCoroutine(DrawCards(amountToDraw));
    }

    IEnumerator DrawCards(int amountToDraw)
    {
        for(int i=0; i< amountToDraw; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(timeBetweenDrawingCards);
        }
    }
}
