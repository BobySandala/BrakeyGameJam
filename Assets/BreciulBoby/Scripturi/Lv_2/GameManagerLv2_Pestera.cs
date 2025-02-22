using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLv2_Pestera : MonoBehaviour
{

    public lv2_UI_pestra UI_Canvas;
    public MummySpawnerController mummySpawnerController;
    private MovementController player;
    [SerializeField]
    private int i_CurrentPhase = 0;
    [SerializeField]
    private int i_DedMummies = 0;
    private int i_playerFullHP;

    public MummyController mumie1;
    public MummyController mumie2;

    [SerializeField]
    private Vector3 mumie1InitialPosition;
    [SerializeField]
    private Vector3 mumie2InitialPosition;
    [SerializeField]
    private Vector3 playerInitialPosition;

    public AudioSource music;
    public AudioClip lvMuzic;
    public AudioClip slapNathTheme;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        if (player != null) { i_playerFullHP = player.uInt_HP; playerInitialPosition = player.transform.position; }

        if (mumie1 != null) {mumie1InitialPosition = mumie1.transform.position; }
        if (mumie2 != null) {mumie2InitialPosition = mumie2.transform.position; }

        music.clip = lvMuzic;
        music.loop = true;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.i_EquippedWeapon == 0)
            {
                UI_Canvas.v_SetWeapon1();
            } else
            {
                UI_Canvas.v_SetWeapon2();
            }
            int i_currentHP = player.uInt_HP;
            if (i_currentHP < i_playerFullHP)
            {
                i_playerFullHP = i_currentHP;
                UI_Canvas.v_TakeDmg();
            }
            if (i_currentHP > i_playerFullHP)
            {
                i_playerFullHP = i_currentHP;
            }
        }
        switch (i_CurrentPhase)
        {
            case 0:
                v_Phase1();
                break;
            case 1:
                v_Phase2();
                break;
            case 2:
                v_Phase3();
                break;
            case 3:
                v_GameOverPhase();
                break;
            case 4:
                v_GamePassedPhase();
                break;
            default:
                break;
        }
    }

    public void v_MummyDed()
    {
        i_DedMummies++;
    }
    private void v_Phase1()
    {
        //phase cand trebuie sa mori
        if (player.uInt_HP <= 0)
        {
            UI_Canvas.v_AparitieSlepNath1();
            i_CurrentPhase++;

            v_freezeMummies(true);
            mumie1.v_GoBackToInitialPosition();
            mumie2.v_GoBackToInitialPosition();

            music.clip = slapNathTheme;
            music.Play();
        }
    }
    private void v_Phase2()
    {
        if (UI_Canvas.b_TaceSlepNath())
        {
            v_freezeMummies(false);
            if (mummySpawnerController != null)
            {
                mummySpawnerController.v_StartSpawning();
            }
            i_CurrentPhase++;
            UI_Canvas.v_DisparitieSlepNath1();

            player.uInt_HP = 3;
            //player.v_SetDed(false);
            player.b_BouEquipped = true;
            UI_Canvas.v_RefillUIHP();
            UI_Canvas.v_EquipBow();
                
            player.transform.position = playerInitialPosition;
            music.clip = lvMuzic;
            music.Play();
        }
    }
    private void v_freezeMummies(bool freeze)
    {
        GameObject[] mummys = GameObject.FindGameObjectsWithTag("Mumie");
        foreach (GameObject mummy in mummys)
        {
            mummy.GetComponent<MummyController>().b_Freeze = freeze;
        }
    }
    private void v_Phase3()
    {
        if (player.uInt_HP <= 0)
        {
            //gameover
            UI_Canvas.v_gameOver();
            i_CurrentPhase++;
            v_freezeMummies(true);
        } else if (i_DedMummies >= mummySpawnerController.i_SpawnMaxCount + 2)
        {
            UI_Canvas.v_gamePassed();
            i_CurrentPhase += 2;
            v_freezeMummies(true);
        }
    }
    private void v_GameOverPhase()
    {
        if (UI_Canvas.b_TaceSlepNath())
        {
            UI_Canvas.v_SetInstructionText("press space to retry");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            print("final game over wompwomp");
        }
    }
    private void v_GamePassedPhase()
    {
        if (UI_Canvas.b_TaceSlepNath())
        {
            UI_Canvas.v_SetInstructionText("Press Space to continue");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            print("final game over wompwomp");
            print("final gamepassed congrulation");
        }
    }
}
