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


    public bool doyouknowwhyareyouhere = false;
    public bool whatisappliance = false;
    public bool whatappliancedo = false;
    public bool wouldyougiveeverything = false;
    public bool doyouknowwhoiswatching = false; //im always watching. no matter where you go. i will follow you. the curse is upon you.

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
            //please someone kill me.
            serverComms.requestmaker(eventL);


            if (TAC.currentSection == "A" && TAC.currentDialogue == 2)
            {
                serverComms.isAsking = "name";

            }
            if (TAC.currentSection == "A" && TAC.currentDialogue == 4)
            {
                serverComms.isAsking = "doyouknowwhyareyouhere";
            }
            if ((TAC.currentSection == "A" && TAC.currentDialogue == 6))
            {
                serverComms.isAsking = "whatisappliance";
            }
            if (TAC.currentSection == "A" && TAC.currentDialogue == 7)
            {
                serverComms.isAsking = "whatappliancedo";
            }
            if (TAC.currentSection == "A" && TAC.currentDialogue == 8)
            {
                serverComms.isAsking = "wouldyougiveeverything";
            }
            if (TAC.currentSection == "A" && TAC.currentDialogue == 9)
            {
                serverComms.isAsking = "doyouknowwhoiswatching";
            }
        }

    }


    public void cleardial()
    {
        textMP.text = null;
        textMP.SetText(".");
        print(TAC.returnCurrentSection().Length);
        if (TAC.currentDialogue == TAC.returnCurrentSection().Length - 1 || TAC.currentDialogue >= TAC.returnCurrentSection().Length)
        {
            print("next section");
            //going to next section
            if (TAC.currentSection == "A")
            {
                if (doyouknowwhyareyouhere && whatisappliance && whatappliancedo && doyouknowwhoiswatching == false)
                {
                    //he can't know...
                    //...
                    //right...?
                }
                if (doyouknowwhyareyouhere && whatisappliance && whatappliancedo && doyouknowwhoiswatching)
                {
                    //...
                    //he have nothing to proof
                    //its only bluff
                }
                if (doyouknowwhyareyouhere == false && whatisappliance == false && whatappliancedo == false)
                {
                    //lets proceed to the regular form then.
                }
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
