using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_1_Controller_LV_1 : MonoBehaviour
{
    public toggleDedPng Lupi;
    public toggleDedPng Gaini;
    public toggleDedPng Inimi;

    public void DedLup()
    {
        if (Lupi)
        {
            Lupi.Died();
        }
    }
    public void DedGaina()
    {
        if (Gaini)
        {
            Gaini.Died();
        }
    }
    public void DedInima()
    {
        if (Inimi)
        {
            Inimi.Died();
        }
    }
}
