using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject childrenWidget;
    private bool _isActive = false;
    private bool _hasBeenClicked = false;
    private float timeWidget;
    public Behaviour buttonType;
    [SerializeField] int actionThreshold;
    public string onclickText;
    public GameObject buttonText;
    
    private void Start()
    {
        GetComponent<Button>().interactable = false;
        childrenWidget = transform.GetChild(0).gameObject;
        GameManager.instance.OnPointsGained.AddListener(CheckActionThreshold);
        GameManager.instance.OnShowCanvaScreen.AddListener(HandleOpenedCanva);
        GameManager.instance.OnCloseCanvaScreen.AddListener(HandleClosedCanva);
        timeWidget = GameManager.instance.timeShowActionWidget;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hasBeenClicked)
            return;
        
        if (!GameManager.instance.isOpen && _isActive)
        {
            childrenWidget.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_hasBeenClicked)
            return;

        if (!GameManager.instance.isOpen &&  _isActive)
        {
            childrenWidget.SetActive(false);
        }
    }

    public void OnClickEnter()
    {
        _hasBeenClicked = true;
        buttonText.GetComponent<TMPro.TextMeshProUGUI>().text = onclickText;
        GetComponent<Button>().interactable = false;
        StartCoroutine(ShowAction());
        GameManager.instance.GainActionPoint(buttonType, 5.0f/3.0f);
    }

    IEnumerator ShowAction()
    {
        GameManager.instance.timeShowActionWidget = timeWidget;
        yield return new WaitForSeconds(GameManager.instance.timeShowActionWidget);
        childrenWidget.SetActive(false);
    }

    private void CheckActionThreshold()
    {
        switch (buttonType)
        {
            case Behaviour.Private:
                if (GameManager.instance.currentPrivatePoints == actionThreshold && !_hasBeenClicked)
                {
                    GetComponent<Button>().interactable = true;
                    _isActive = true;
                }
                break;
            
            case  Behaviour.Public:
                if (GameManager.instance.currentPublicPoints == actionThreshold && !_hasBeenClicked)
                {
                    GetComponent<Button>().interactable = true;
                    _isActive = true;
                }
                break;
        }
    }

    private void HandleOpenedCanva()
    {
        childrenWidget.SetActive(false);
        
        if(_isActive && !_hasBeenClicked)
            GetComponent<Button>().interactable = false;
    }

    private void HandleClosedCanva()
    {
        if(_isActive && !_hasBeenClicked)
            GetComponent<Button>().interactable = true;
    }
}
