using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLv2_Pestera : MonoBehaviour
{

    public lv2_UI_pestra UI_Canvas;
    public MummySpawnerController mummySpawnerController;
    private MovementController player;
    private int i_CurrentPhase = 0;
    [SerializeField]
    private int i_DedMummies = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (i_CurrentPhase)
        {
            case 0:
                //phase cand trebuie sa mori
                if (player.uInt_HP <= 0) 
                { 
                    UI_Canvas.v_AparitieSlepNath1(); 
                    i_CurrentPhase++;

                    GameObject[] mummys = GameObject.FindGameObjectsWithTag("Mumie");
                    foreach(GameObject mummy in mummys)
                    {
                        mummy.GetComponent<MummyController>().b_Freeze = true;
                    }
                }
                break;
            case 1:
                if (UI_Canvas.b_TaceSlepNath())
                {
                    GameObject[] mummys = GameObject.FindGameObjectsWithTag("Mumie");
                    foreach (GameObject mummy in mummys)
                    {
                        mummy.GetComponent<MummyController>().b_Freeze = false;
                    }
                    if (mummySpawnerController != null)
                    {
                        mummySpawnerController.v_StartSpawning();
                        i_CurrentPhase++;
                        UI_Canvas.v_DisparitieSlepNath1();
                        player.transform.position = new Vector3(-7.5f, 8, -17);
                        player.uInt_HP = 3;
                        player.b_BouEquipped = true;
                    }
                }
                break;
            case 2:

                break;
            default:
                break;
        }
    }

    public void v_MummyDed()
    {
        i_DedMummies++;
    }
}
