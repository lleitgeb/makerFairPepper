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
    [SerializeField] private TMP_Text[] rgbTarget0, rgbTarget1, rgbTarget2, rgbTarget3;
    [SerializeField] private TMP_Text[] rgbPlayer0, rgbPlayer1, rgbPlayer2, rgbPlayer3;
    [SerializeField] private TMP_Text[] playerPoints;
    [SerializeField] private MixObjectColor mixColorObj;
    [SerializeField] private Sprite[] spritesAuftrag;
    [SerializeField] private TMP_Text sumPoints;
    

    private int round = 0;


    private Session currentSession;

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


    //WelcomeSene BTN Start. 
    public void StartSession()
    {
        DateTime currentDateTime = DateTime.Now;
        string customFormat = currentDateTime.ToString("yyyy-MM-dd-HH:mm:ss");

        currentSession = new GameObject().AddComponent<Session>();
        currentSession.SetInfos(playerNameWelcome.text, customFormat, PlayMode.PepperOnly);
        currentSession.SetFirstRound();

        SetUIRoundsUnplayed();
        EnableTaskCanvas();

        //UpdateUI in Welcome-Canvas
        playerNameAuftrag.text = currentSession.PlayerName;
    }

    //Wir von Card in AuftragCanvas aufgerufen. 
    public void GenerateTask(int task)
    {
        currentSession.SetTask((TaskToDo)task);
        auftrag.gameObject.SetActive(false);
    }

    public bool IsPlayMode()
    {
        return !welcome.gameObject.activeSelf && !auftrag.gameObject.activeSelf;
    }

    public void EnableTaskCanvas()
    {
        playerNameAuftrag.text = currentSession.PlayerName;
        int currentRound = currentSession.GetCurrentRound();
        roundImgsAuftrag[currentRound].color = Color.white;
        Debug.Log("enable canvas: !!!! cr " + currentRound);
        if(currentRound >= currentSession.MaxRounds-1)
        {
            SetEndScreenInfos();
            ActivateScreen(endscreen.gameObject);
        }
        else
        {
            for (int i = 0; i < currentSession.MaxRounds; i++)
            {
                Debug.Log("poiont: " + i + " " + currentSession.GetRoundTask(i).Points);
                if (currentSession.GetRoundTask(i).Points == -1) break;
                roundPoints[i].text = currentSession.GetRoundTask(i).Points.ToString();
            }

            ActivateScreen(auftrag.gameObject);
        }
    }

    public void SetEndScreenInfos()
    {
        for (int i = 0; i < currentSession.MaxRounds; i++)
        {
            PepperOnlyTask tmpTask = currentSession.GetRoundTask(i);
            
            Sprite taskSprite = tmpTask.targetSprite;
            endSceneTaskImages[i].sprite = taskSprite;

            playerPoints[i].text = tmpTask.Points.ToString();

            TMP_Text[] tmpRGBTarget = new TMP_Text[3];
            TMP_Text[] tmpRGBPlayer = new TMP_Text[3];

            switch (i)
            {
                case 0:
                    tmpRGBTarget = rgbTarget0;
                    tmpRGBPlayer = rgbPlayer0;
                    break;
                case 1:
                    tmpRGBTarget = rgbTarget1;
                    tmpRGBPlayer = rgbPlayer1;
                    break;
                case 2:
                    tmpRGBTarget = rgbTarget2;
                    tmpRGBPlayer = rgbPlayer2;
                    break;
                case 3:
                    tmpRGBTarget = rgbTarget3;
                    tmpRGBPlayer = rgbPlayer3;
                    break;
            }

            tmpRGBTarget[0].text = tmpTask.targetColor.r.ToString();
            tmpRGBTarget[1].text = tmpTask.targetColor.g.ToString();
            tmpRGBTarget[2].text = tmpTask.targetColor.b.ToString();

            tmpRGBPlayer[0].text = tmpTask.playerColor.r.ToString();
            tmpRGBPlayer[1].text = tmpTask.playerColor.g.ToString();
            tmpRGBPlayer[2].text = tmpTask.playerColor.b.ToString();
        }
        sumPoints.text = currentSession.SumTaskResults().ToString();
    }

    public void CollectEndScreenData()
    {
        for(int i = 0; i < currentSession.MaxRounds; i++)
        {
            Sprite taskSprite = currentSession.GetRoundTask(i).targetSprite;
            Debug.Log("taskSprite name: " +taskSprite.name);
            endSceneTaskImages[i].sprite = taskSprite;
        }
    }

    private void SetUIRoundsUnplayed()
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
        CollectEndScreenData();
        ActivateScreen(endscreen.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("cur round: " + currentSession.GetCurrentRound());

            PepperOnlyTask cptask = currentSession.GetRoundTask(currentSession.GetCurrentRound());
            cptask.playerColor = mixColorObj.GetPlayerMixedColor();
            cptask.CalcPoints();
            EnableTaskCanvas();
            currentSession.IncreaseRound();
            mixColorObj.ResetPipeStation();
        }
    }
}
