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

    private float f_AnimDelay = 0.1f;
    private float f_AnimStartTime;

    private int i_EquippedWeapon = 0;
    private void Start()
    {
        bowImage.enabled = false;
        arrowIndicatorImage.enabled = false;
    }
    // Start is called before the first frame update
    public void v_SetWeapon1()
    {
        if (i_EquippedWeapon == 0) { return; }
        i_EquippedWeapon = 0;
        v_PlayArrowIndicatorAnimation(1);
    }
    public void v_SetWeapon2()
    {
        if(i_EquippedWeapon == 1) { return; }
        i_EquippedWeapon = 1;
        v_PlayArrowIndicatorAnimation(-1);
    }

    public void v_EquipBow()
    {
        bowImage.enabled = true;
        arrowIndicatorImage.enabled = true;
        arrowIndicatorImage.sprite = s_ArrowIndicatorFrames[s_ArrowIndicatorFrames.Length - 1];
    }

    private void Update()
    {
        if (b_PlayingAnim)
        {
            if (Time.time >= f_AnimStartTime + f_AnimDelay)
            {
                f_AnimStartTime = Time.time;
                i_AnimFrameIndex += i_AnimDirection;
                if (i_AnimFrameIndex < 0)
                {
                    i_AnimFrameIndex = 0;
                    b_PlayingAnim = false;
                }
                else if (i_AnimFrameIndex >= s_ArrowIndicatorFrames.Length)
                {
                    i_AnimFrameIndex = s_ArrowIndicatorFrames.Length - 1;
                } else
                {
                    arrowIndicatorImage.sprite = s_ArrowIndicatorFrames[i_AnimFrameIndex];
                }
            }
        }
    }

    private void v_PlayArrowIndicatorAnimation(int direction)
    {
        f_AnimStartTime = Time.time;
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
