using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv2_UI_pestra : MonoBehaviour
{
    // Start is called before the first frame update
    public lv2_UI aparitieSlepNath1;
    public Phase_1_Controller_LV_1 weapon_UI;

    public lv2_UI gameOver_UI;
    public lv2_UI gamePassed_UI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void v_AparitieSlepNath1() 
    {
        if (aparitieSlepNath1 == null) {return; }

        aparitieSlepNath1.gameObject.SetActive(true);
        aparitieSlepNath1.AFostSetatActiv();
        aparitieSlepNath1.b_LineByLine = true;
    }

    public void v_SetInstructionText(string text)
    {
        if (gameOver_UI.enabled) { gameOver_UI.SetInstructionText(text); }
        if (gamePassed_UI.enabled) { gamePassed_UI.SetInstructionText(text); }
    }

    public void v_DisparitieSlepNath1()
    {
        if (aparitieSlepNath1 == null) { return; }

        aparitieSlepNath1.gameObject.SetActive(false);
    }

    public bool b_TaceSlepNath()
    {
        if (aparitieSlepNath1.isActiveAndEnabled) { return aparitieSlepNath1.taceSlapnut; }
        if (gameOver_UI.isActiveAndEnabled) { return gameOver_UI.taceSlapnut; }
        if (gamePassed_UI.isActiveAndEnabled) { return gamePassed_UI.taceSlapnut; }

        return false;
    }
    public void v_EquipBow()
    {
        if (weapon_UI != null) { weapon_UI.v_EquipBow(); }
    }
    public void v_SetWeapon1()
    {
        if (weapon_UI != null) { weapon_UI.v_SetWeapon1(); }
    }
    public void v_SetWeapon2()
    {
        if (weapon_UI != null) { weapon_UI.v_SetWeapon2(); }
    }
    public void v_RefillUIHP()
    {
        if (weapon_UI != null) { weapon_UI.v_ResetHP(); }
    }
    public void v_TakeDmg()
    {
        if (weapon_UI != null) { weapon_UI.DedInima(); }
    }
    public void v_gameOver()
    {
        if (gameOver_UI == null) { return; }
        if (weapon_UI == null) { return; }
        if (gamePassed_UI == null) { return; }
        if (aparitieSlepNath1 == null) { return; }

        gameOver_UI.gameObject.SetActive(true);
        gameOver_UI.AFostSetatActiv();
        weapon_UI.gameObject.SetActive(false);
        gamePassed_UI.gameObject.SetActive(false);
        aparitieSlepNath1.gameObject.SetActive(false);
    }
    public void v_gamePassed()
    {
        if (gameOver_UI == null) { return; }
        if (weapon_UI == null) { return; }
        if (gamePassed_UI == null) { return; }
        if (aparitieSlepNath1 == null) { return; }

        gamePassed_UI.gameObject.SetActive(true);
        gamePassed_UI.AFostSetatActiv();
        weapon_UI.gameObject.SetActive(false);
        gameOver_UI.gameObject.SetActive(false);
        aparitieSlepNath1.gameObject.SetActive(false);
    }
}
