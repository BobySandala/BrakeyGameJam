using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_1_Controller_LV_1 : MonoBehaviour
{
    public toggleDedPng Lupi;
    public toggleDedPng Gaini;
    public toggleDedPng Inimi;
    public toggleWeaponUI Weapon;


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
            print("toggle - controller");
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
    public void v_ResetHP()
    {
        if (Inimi != null)
        {
            Inimi.v_Refresh();
        }
    }
    public void v_SetWeapon1()
    {
        if (Weapon != null) { Weapon.v_SetWeapon1(); }
    }
    public void v_SetWeapon2()
    {
        if (Weapon != null) { Weapon.v_SetWeapon2(); }
    }
}
