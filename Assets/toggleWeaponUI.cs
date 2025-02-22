using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleWeaponUI : MonoBehaviour
{
    public Image bowImage;
    public Image axeImage;
    public Image arrowIndicatorImage;
    public Sprite s_weapon1;
    public Sprite s_weapon2;
    public Sprite[] s_ArrowIndicatorFrames;
    private bool b_PlayingAnim;
    private int i_AnimDirection;
    private int i_AnimFrameIndex;
    // Start is called before the first frame update
    public void v_SetWeapon1()
    {
        
    }
    public void v_SetWeapon2()
    {
        
    }

    private void Update()
    {
        if (b_PlayingAnim)
        {

        }
    }

    private void v_PlayArrowIndicatorAnimation(int direction)
    {
        b_PlayingAnim = true;
        if (direction == 1)
        {
            i_AnimFrameIndex = 0;
            i_AnimDirection = 1;
        }
        else if (direction == -1)
        {
            i_AnimFrameIndex = s_ArrowIndicatorFrames.Length - 1;
            i_AnimDirection = -1;
        }
    }

}
