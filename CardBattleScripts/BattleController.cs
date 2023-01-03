using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public int playerMana;
    public int manaPerTurn = 4, maxMana = 8;
    public int playerHealth = 15, enemyHealth = 15;
    public int startingCardsAmount = 5;
    public Transform discardPoint;
    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;
    public int turn1Amount, turn2Amount, turn3Amount;
    private int turn = 1;

    public static BattleController instance;

    private void Awake() {
        instance = this;
        playerMana = manaPerTurn;
    }

    private void Start() {
        DeckController.instance.DrawMultipleCards(startingCardsAmount);
        for(int i =0; i < turn1Amount; i++)
        {
            EnemyController.instance.CreateCard();
        }
    }

    public void SpendMana(int manaCost)
    {
        playerMana = playerMana - manaCost;
        if(playerMana < 0)
            playerMana = 0;
    }

    public void FillPlayerMana()
    {
        playerMana += manaPerTurn;
        if(playerMana > maxMana)
        {
            playerMana = maxMana;
        }
    }

    IEnumerator MoveCardCoroutine()
    {
        EnemyController.instance.MoveCard();
        yield return new WaitForSeconds(0.75f);
        AdvanceTurn();
    }

    public void AdvanceTurn()
    {
        if(playerHealth > 0 && enemyHealth > 0)
        {
            currentPhase++;
            if((int)currentPhase >= System.Enum.GetNames(typeof(TurnOrder)).Length)
            {
                currentPhase = 0;
            }
            switch(currentPhase)
            {
                case TurnOrder.playerActive:
                int turnAmount;
                if(turn == 2)
                    turnAmount = turn2Amount;
                else
                    turnAmount = turn3Amount;
                for(int i =0; i < turnAmount; i++)
                {
                    EnemyController.instance.CreateCard();
                }
                DeckController.instance.DrawCard();
                UIController.instance.endTurnButton.SetActive(true);
                FillPlayerMana();
                break;

                case TurnOrder.playerCardAttacks:
                CardPointController.instance.PlayerAttack();
                break;

                case TurnOrder.enemyActive:
                StartCoroutine(MoveCardCoroutine());
                turn++;
                break;

                case TurnOrder.enemyCardAttacks:
                CardPointController.instance.EnemyAttack();
                break;
            }
        }else
        {
            DisableButton();
            HandController.instance.DestroyCard();
        }
        
    }

    public void DisableButton()
    {
        UIController.instance.battleEndCanvas.SetActive(true);
        UIController.instance.endTurnButton.SetActive(false);
        UIController.instance.drawCardButton.SetActive(false);
    }

    public void PlayerTakeDamage(int damage)
    {
        if(playerHealth > 0)
        {
            playerHealth -= damage;
            UIController.instance.UpdateHealthText(playerHealth, enemyHealth);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        if(enemyHealth > 0)
        {
            enemyHealth -= damage;
            UIController.instance.UpdateHealthText(playerHealth, enemyHealth);
        }
    }
}
