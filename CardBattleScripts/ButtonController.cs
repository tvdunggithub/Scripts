using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    public GameObject buttonText;

    private void Awake() 
    {
        buttonText.SetActive(false);
    }
    void Update()
    {
         
    }
 
     public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.SetActive(true);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.SetActive(false);
    }
 }
