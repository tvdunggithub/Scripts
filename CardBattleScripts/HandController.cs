using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static HandController instance;
    public List<Card> heldCards;
    public List<Vector3> cardPositions = new List<Vector3>();

    public Transform minPos, maxPos;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        SetCardPosInHand();
    }

    public void DestroyCard()
    {
        for(int i = 0; i < heldCards.Count; i++)
        {
            Destroy(heldCards[i]);
        }
    }

    public void SetCardPosInHand()
    {
        cardPositions.Clear();
        Vector3 distanceBetweenCardPoints = Vector3.zero;

        if(heldCards.Count > 1)
        {
            distanceBetweenCardPoints = (maxPos.position - minPos.position)
                                                 / (heldCards.Count - 1);
        }

        for(int i=0; i < heldCards.Count; i++)
        {
            cardPositions.Add(minPos.position + (distanceBetweenCardPoints * i));
            heldCards[i].MoveToPoint(cardPositions[i], minPos.rotation);
            heldCards[i].inHand = true;
            heldCards[i].handPos = i;
        }
    }

    public void RemoveCardFromHand(Card cardToRemove)
    {
        heldCards.RemoveAt(cardToRemove.handPos);
        SetCardPosInHand();
    }

    public void AddCardToHand(Card cardToAdd)
    {
        heldCards.Add(cardToAdd);
        SetCardPosInHand();
    }

}
