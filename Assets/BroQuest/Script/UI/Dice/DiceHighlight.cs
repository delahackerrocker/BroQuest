using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Highlights the number that is face up. Disable this script to make your dice no longer glow. 
public class DiceHighlight : MonoBehaviour
{
    public GameObject[] sides;
    DiceStats diceStats;

    public bool isDiceSet = false;

    public int framesUntilDiceIsSet = 150;
    private int frameCounter = 0;
    public int finalDiceRead = -1;
    private int lastDiceRead = -1;

    void Start()
    {
        diceStats=gameObject.GetComponent<DiceStats>();
    }

    void Update()
    {
        HighlightSides();
    }
    void HighlightSides()
    {
        for(int i = 0; i<sides.Length; i++)
        {
            sides[i].SetActive(false);

        }
        sides[diceStats.side - 1].SetActive(true);
        lastDiceRead = diceStats.side;
        CheckForFinalRead();
    }

    void CheckForFinalRead()
    {
        if (finalDiceRead == lastDiceRead)
        {
            frameCounter++;
        } else
        {
            frameCounter = 0;
            finalDiceRead = lastDiceRead;
        }
        if (frameCounter > framesUntilDiceIsSet)
        {
            isDiceSet = true;
        } else
        {
            isDiceSet = false;
        }
    }
}
