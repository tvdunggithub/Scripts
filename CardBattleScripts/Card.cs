using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardSO cardSO;
    public int currentHealth;
    public int attackPower, manaCost;
    public TMP_Text healthText, attackText, manaCostText, nameText, 
                    actionDescriptionText, loreText;
    public Image characterArt, backGroundArt;
    private Vector3 targetPos;
    private Quaternion targetRot;
    public float moveSpeed = 3f, rotSpeed = 500f;
    public bool inHand;
    public int handPos;
    private bool isSelected;
    Collider boxCollider;
    private HandController handController;
    public LayerMask placementLayer;

    public Placement assignedPlace;
    public Animator animator;

    private void Start() 
    {   
        handController = FindObjectOfType<HandController>();
        boxCollider = GetComponent<Collider>();
        SetupCard();   
        UIController.instance.UpdateManaText(BattleController.instance.playerMana);
    }

    private void Update() 
    {
        if(transform.position != targetPos)
        {
            StartMove();
        }
    }

    private void StartMove()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if(targetPos == Vector3.zero)
        {
            targetPos = transform.position;
            targetRot = transform.rotation;
        }
    }

    public void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;
        
        UpdateCardDisplay();

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        characterArt.sprite = cardSO.characterSprite;
        backGroundArt.sprite = cardSO.backGroundSprite;
    }

    public void MoveToPoint(Vector3 pointToMove, Quaternion rotToMatch)
    {
        targetPos = pointToMove;
        targetRot = rotToMatch;
    }

    private void OnMouseOver() 
    {
        if(inHand)
        {
            MoveToPoint(handController.cardPositions[handPos] 
                        + new Vector3(0f,0.7f,.5f), Quaternion.identity);
        }
    }

    private void OnMouseExit() 
    {
        if(inHand)
        {
            MoveToPoint(handController.cardPositions[handPos], handController.minPos.rotation);
        }
    }

    private void OnMouseDrag() 
    {
        isSelected = true;
        boxCollider.enabled = false;
        FollowCursor();
    }

    private void OnMouseUp() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayer))
        {   
            Placement placement = hit.collider.gameObject.GetComponent<Placement>();
            if(placement.activeCard == null)
            {
                if(BattleController.instance.playerMana >= manaCost)
                {
                    MoveToPoint(placement.transform.position, Quaternion.identity);
                    handController.RemoveCardFromHand(this);
                    BattleController.instance.SpendMana(manaCost);
                    UIController.instance.UpdateManaText(BattleController.instance.playerMana);
                    inHand = false;
                    placement.activeCard = this;
                    assignedPlace = placement;
                    AudioController.instance.sfx[4].Play();
                }else
                {
                    UIController.instance.ShowWarningMessage();
                    ReturnToHand();
                }
            }
            else
                ReturnToHand();   
        }
        else
        {
            ReturnToHand();
        }    
    }

    private void FollowCursor()
    {
        if(isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                MoveToPoint(hit.point + new Vector3(0,1f,0), Quaternion.identity);
            }
        }
    }

    private void ReturnToHand()
    {
        isSelected = false;
        boxCollider.enabled = true;
        MoveToPoint(handController.cardPositions[handPos], handController.minPos.rotation);
    }

    public void DamageCard(int damageAmount)
    {
        currentHealth -= damageAmount;
        AudioController.instance.sfx[5].Play();
        UpdateCardDisplay();
        if(currentHealth <= 0)
        {
            MoveToPoint(BattleController.instance.discardPoint.position,BattleController.instance.discardPoint.rotation);
            animator.SetTrigger("Jumb");
            Destroy(gameObject, 3f);
            CardPointController.instance.CheckAssignedCard();
        }
        animator.SetTrigger("Hurt");
    }

    public void UpdateCardDisplay()
    {
        healthText.text = currentHealth.ToString();
        attackText.text = attackPower.ToString();
        manaCostText.text = manaCost.ToString();
    }
}
