using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForActivate : MonoBehaviour
{
    [SerializeField] int id;

    [SerializeField] GameObject[] thingToActivate;

    bool on;

    public void TurnOn()
    {
        thingToActivate[id].SetActive(true);
        on = true;
    }

    public void TurnOff()
    {
        thingToActivate[id].SetActive(false);
        on = false;
    }

    public void Switch()
    {
        if(!on)
        {
            thingToActivate[id].SetActive(true);
            on = true;
        }
        if(on)
        {
            thingToActivate[id].SetActive(false);
            on = false;
        }
    }
}
