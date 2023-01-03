using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TMP_Text manaText, playerHealthText, enemyHealthText;
    public GameObject warningMessage;
    public GameObject endTurnButton;
    public GameObject battleEndCanvas;
    public GameObject drawCardButton;

    private void Awake() {
        instance = this;
        warningMessage.SetActive(false);
    }

    public void UpdateManaText(int playerMana)
    {
        manaText.text = "Mana: " + playerMana;
    }

    public void UpdateHealthText(int playerHealth, int enemyHealth)
    {
        playerHealthText.text = "Player Health: " + playerHealth;
        enemyHealthText.text = "Enemy Health: " + enemyHealth;
    }

    public void ShowWarningMessage()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        warningMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        warningMessage.SetActive(false);
    }

    public void DrawCard()
    {
        DeckController.instance.DrawCardForMana();
    }

    public void EndButtonClick()
    {
        BattleController.instance.AdvanceTurn();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSelectLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
