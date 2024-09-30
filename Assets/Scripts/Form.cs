using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using System.Net.Mail;

public class Form : MonoBehaviour
{
    public TextAndContext TAC;

    public TextMeshProUGUI textMP;
    public ServerComms serverComms;
    private string tempt;
    public bool specialyip = false;


    public bool doesheknowwhoiswhatching = false;
    public bool whatisappliance = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextMachineE("...", 0f, 0f, "text")); 
        TAC = gameObject.GetComponent<TextAndContext>();
        serverComms = gameObject.GetComponent<ServerComms>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextMachine(string text)
    {
        foreach (var item in text)
            {
            tempt = tempt + item;
            print(tempt);
            }
        print(tempt);
    }

    IEnumerator TextMachineE(string text, float timespace, float waitingTime, string eventL)
    {
        if (timespace == 0)
        {
            timespace = 0.1f;
        }
        if (waitingTime == 0)
        {
            waitingTime = 3f;
        }
        if (eventL != "text")
        {
            if (eventL == "crash")
            {

            }
            else
            {
                specialyip = true;
            }
        }
        if (eventL == "text")
        {
            specialyip = false;
        }
    
        foreach (var item in text)
        {
            tempt = tempt + item;
            textMP.text = tempt;
            yield return new WaitForSeconds(timespace);
        }

         yield return new WaitForSeconds(waitingTime);
        tempt = null;
        if (specialyip == false)
        {
            cleardial();
            if (eventL == "crash")
            {
                serverComms.requestmaker("Wincrash");
            }
        }
        else
        {
            //waiting for manual cleardial :3
            //only use for special event/question lol
            serverComms.requestmaker(eventL);


            if (TAC.currentSection == "A" && TAC.currentDialogue == 2)
            {
                serverComms.isAsking = "name";

            }
            if (TAC.currentSection == "A" && TAC.currentDialogue == 4)
            {
                serverComms.isAsking = "whoiswatchingYON";
            }
            if ((TAC.currentSection == "A" && TAC.currentDialogue == 6))
            {
                serverComms.isAsking = "whatisappliance";
            }
        }

    }


    public void cleardial()
    {
        textMP.text = null;
        textMP.SetText(".");
        if (TAC.currentDialogue == TAC.returnCurrentSection().Length || TAC.currentDialogue >= TAC.returnCurrentSection().Length)
        {
            //going to next section
            if (TAC.currentSection == "A")
            {
                TAC.currentSection = "B";
            }
        }
        else
        {
        TAC.currentDialogue = TAC.currentDialogue + 1;
            //start the couroutine and return the text to TextMachineE(returnCurrentDial());
            StartCoroutine(TextMachineE(TAC.returnCurrentDial(), TAC.returnSpacetime(), 0, TAC.returnCurrentDialEvent()));
        }
    }
}
