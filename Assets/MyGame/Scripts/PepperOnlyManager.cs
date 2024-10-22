using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PepperOnlyManager : MonoBehaviour
{
    [SerializeField] private Canvas welcome, auftrag, endscreen;
    [SerializeField] private TMP_Text playerNameWelcome, playerNameAuftrag;
    [SerializeField] private TMP_Text taskPoints;
    [SerializeField] private TMP_Text[] roundPoints;
    [SerializeField] private Image[] roundImgsAuftrag;
    [SerializeField] private Image[] endSceneTaskImages;
    [SerializeField] private TMP_Text[] rgbTarget0, rbgTarget1, rbgTarget2, rbgTarget3;
    [SerializeField] private TMP_Text[] rgbPlayer0, rbgPlayer1, rbgPlayer2, rbgPlayer3;
    [SerializeField] private TMP_Text[] playerPoints;
    [SerializeField] private MixObjectColor mixColorObj;

    private int round = 0;


    public Session currentSession;

    // Start is called before the first frame update
    private void Start()
    {
        if(auftrag != null)
        {
            if (auftrag.gameObject.activeSelf)
            {
                auftrag.gameObject.SetActive(false);
                Debug.Log("Set Auftrag Canvas to false");
            }
            
        }
        welcome.gameObject.SetActive(true);
    }

    public void StartSession()
    {
        DateTime currentDateTime = DateTime.Now;
        string customFormat = currentDateTime.ToString("yyyy-MM-dd-HH:mm:ss");
        
        currentSession = new Session(playerNameWelcome.text, customFormat,PlayMode.PepperOnly);
        ActivateScreen(auftrag.gameObject);
        currentSession.SetFirstRound();

        SetAllRoundsUnplayed();

        //UpdateUI in Welcome-Canvas
        playerNameAuftrag.text = currentSession.playerName;
    }

    public bool IsPlayMode()
    {
        return !welcome.gameObject.activeSelf && !auftrag.gameObject.activeSelf;
    }

    public void EnableTaskCanvas()
    {
        playerNameAuftrag.text = currentSession.playerName;
        taskPoints.text = currentSession.points.ToString();
        ActivateScreen(auftrag.gameObject);
    }

    private void SetAllRoundsUnplayed()
    {
        for(int i = 0; i < roundImgsAuftrag.Length; i++)
        {
            roundImgsAuftrag[i].color = Color.gray;
            roundPoints[i].text = "--";
        }
    }

    private void ActivateScreen(GameObject objToActivate)
    {
        if (objToActivate.gameObject.GetComponent<Canvas>() == null) return;

        welcome.gameObject.SetActive(false);
        auftrag.gameObject.SetActive(false);
        endscreen.gameObject.SetActive(false);

        objToActivate.SetActive(true);
    }

    public void EnableEndSession()
    {
        ActivateScreen(endscreen.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableTaskCanvas();
            ActivateScreen(auftrag.gameObject);
            //mixColorObj.EvaluatePoints(currentSession.)
        }
    }
}
