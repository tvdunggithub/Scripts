using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public List<CardSO> enemyCard = new List<CardSO>();
    public Card cardToSpawn;
    public Placement[] spawnPlace;
    private bool isFullSlot;

    private void Awake() {
        instance = this;
    }
    
    void Start()
    {
        
    }

    public void CheckSlot()
    {
        for(int i = 0; i < 5; i++)
        {
            if(spawnPlace[i].activeCard == null)
            {
                isFullSlot = false;
                break;
            }            
        }
    }

    public void CreateCard()
    {
        int randomPoint = Random.Range(0, spawnPlace.Length);
        if(spawnPlace[randomPoint].activeCard == null)
        {
            if(enemyCard.Count != 0)
            {
                Card newCard = Instantiate(cardToSpawn, spawnPlace[randomPoint].transform.position, transform.rotation);
                spawnPlace[randomPoint].activeCard = newCard;
                newCard.cardSO = enemyCard[0];
                newCard.SetupCard();
                enemyCard.RemoveAt(0);
            }
            
        }
        else
        {
            isFullSlot = true;
            CheckSlot();
            if(isFullSlot == false)
            {
                CreateCard();
            }
        }

    }

    public void MoveCard()
    {
        for(int i = 0; i < 5; i++)
        {
            if(CardPointController.instance.enemyCardPoint[i].activeCard == null 
                && spawnPlace[i].activeCard != null)
            {
                spawnPlace[i].activeCard.MoveToPoint(CardPointController.instance.enemyCardPoint[i].transform.position, transform.rotation);
                CardPointController.instance.enemyCardPoint[i].activeCard = spawnPlace[i].activeCard;
                spawnPlace[i].activeCard = null;
            }
        }
    }
}
