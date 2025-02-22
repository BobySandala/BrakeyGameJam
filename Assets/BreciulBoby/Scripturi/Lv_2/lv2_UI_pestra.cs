using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv2_UI_pestra : MonoBehaviour
{
    // Start is called before the first frame update
    public lv2_UI aparitieSlepNath1;

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

    public void v_DisparitieSlepNath1()
    {
        if (aparitieSlepNath1 == null) { return; }

        aparitieSlepNath1.gameObject.SetActive(false);
    }

    public bool b_TaceSlepNath()
    {
        if (aparitieSlepNath1.isActiveAndEnabled) { return aparitieSlepNath1.taceSlapnut; }

        return false;
    }
}
