using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPointController : MonoBehaviour
{
    public static CardPointController instance;
    public Placement[] playerCardPoint, enemyCardPoint;
    public float waitTime = 0.5f;
    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCo());
    }

    IEnumerator PlayerAttackCo()
    {

        for(int i = 0; i < playerCardPoint.Length; i++)
        {
            if(playerCardPoint[i].activeCard != null)
            {
                if(enemyCardPoint[i].activeCard != null)
                {
                    enemyCardPoint[i].activeCard.DamageCard(playerCardPoint[i].activeCard.attackPower);
                }else
                {
                    BattleController.instance.EnemyTakeDamage(playerCardPoint[i].activeCard.attackPower);
                }
                playerCardPoint[i].activeCard.animator.SetTrigger("Attack");
                AudioController.instance.sfx[5].Play();
                yield return new WaitForSeconds(waitTime);
            }
        }
        BattleController.instance.AdvanceTurn();
    }

    public void EnemyAttack()
    {
        StartCoroutine(EnemyAttackCo());
    }

    IEnumerator EnemyAttackCo()
    {

        for(int i = 0; i < enemyCardPoint.Length; i++)
        {
            if(enemyCardPoint[i].activeCard != null)
            {
                if(playerCardPoint[i].activeCard != null)
                {
                    playerCardPoint[i].activeCard.DamageCard(enemyCardPoint[i].activeCard.attackPower);
                }else
                {
                    BattleController.instance.PlayerTakeDamage(enemyCardPoint[i].activeCard.attackPower);
                }
                enemyCardPoint[i].activeCard.animator.SetTrigger("Attack");
                AudioController.instance.sfx[5].Play();
                yield return new WaitForSeconds(waitTime);
            }
        }
        BattleController.instance.AdvanceTurn();
    }

    public void CheckAssignedCard()
    {
        foreach(Placement point in enemyCardPoint)
        {
            if(point.activeCard != null)
            {
                if(point.activeCard.currentHealth <= 0)
                {
                    point.activeCard = null;
                }
            }
        }

        foreach(Placement point in playerCardPoint)
        {
            if(point.activeCard != null)
            {
                if(point.activeCard.currentHealth <= 0)
                {
                    point.activeCard = null;
                }
            }
        }
    }
}
