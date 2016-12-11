using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_Lobfather : MonoBehaviour
{
    enum STATE { IDLE, CANETHROW, SKYCANE, SUMMON, TRAMPLE };

    public GameObject GO_player;
    private bool bl_onscreen;

    //Lobfather
    public int in_Health;
    public Vector2 V2_bossPosOn;
    public Vector2 V2_bossPosOff;
    public float fl_transSpeed;

    //Cane Throw phase
    public int in_numofCanestothrow;
    private int in_canethrowcount;
    public float fl_canethrowspeed;
    public float fl_caneTimer;
    private float fl_whentothrow;
    public Vector2 V3_caneStart;
    public GameObject GO_cane;

    //Sky Cane phase
    public int in_numofSkyCanes;
    private int in_skycanecount;
    public float fl_skycanespeed;
    public GameObject GO_skycane;
    private bool bl_skycaneSpawned;
    private GameObject skyCane;

    //Summoning Phase
    public int in_enemiestospawn;
    private int in_enemiesleft;
    private List<GameObject> GO_enemies;
    public GameObject GO_enemy;
    public float fl_turnTimer;
    private bool bl_stopSpawning;

    //Trample Phase
    public float fl_tramplespeed;
    public int in_numoftramples;
    private int in_tramplescount;
    private bool bl_hit;

    STATE currState;

    void Start()
    {
        bl_onscreen = true;
        in_canethrowcount = 0;
        in_skycanecount = 0;
        in_enemiesleft = in_enemiestospawn;
        in_tramplescount = 0;
        bl_hit = false;
        fl_whentothrow = 0;
        bl_skycaneSpawned = false;
        bl_stopSpawning = false;

        GO_enemies = new List<GameObject>();

        currState = STATE.IDLE;
    }

    void Update()
    {
        switch (currState)
        {
            case STATE.IDLE:
                IdlePhase();
                break;
            case STATE.CANETHROW:
                CaneThrowPhase();
                break;
            case STATE.SKYCANE:
                SkyCanePhase();
                break;
            case STATE.SUMMON:
                SummonPhase();
                break;
            case STATE.TRAMPLE:
                TramplePhase();
                break;
            default:
                break;
        }
    }

    void IdlePhase()
    {
        transform.position = Vector2.MoveTowards(transform.position, V2_bossPosOn, fl_transSpeed);

        if (transform.position.x <= 2.3)
        {
            currState = STATE.CANETHROW;
        }
    }

    void CaneThrowPhase()
    {
        fl_whentothrow += Time.deltaTime;

        if (fl_whentothrow >= fl_caneTimer && in_canethrowcount < in_numofCanestothrow)
        {
           GameObject cane = Instantiate(GO_cane);

            cane.GetComponent<scr_ThrowCane>().SetCane(V3_caneStart, GO_player.transform.position, fl_canethrowspeed);

            fl_whentothrow = 0;
            in_canethrowcount += 1;
        }

        if (in_canethrowcount >= in_numofCanestothrow && GameObject.Find("Throw Cane(Clone)") == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, V2_bossPosOff, fl_transSpeed);

            if (transform.position.x >= V2_bossPosOff.x)
            {
                in_canethrowcount = 0;

                currState = STATE.SKYCANE;
            }
        }
    }

    void SkyCanePhase()
    {
        if (!bl_skycaneSpawned)
        {
            skyCane = Instantiate(GO_skycane);
            skyCane.GetComponent<scr_SkyCane>().SetSkyCane(GO_player);
            bl_skycaneSpawned = true;
        }

        in_skycanecount = skyCane.GetComponent<scr_SkyCane>().NumOfAttacking();

        if (in_skycanecount >= in_numofSkyCanes)
        {
            Destroy(skyCane);
            in_skycanecount = 0;
            currState = STATE.SUMMON;
            bl_skycaneSpawned = false;
        }
    }

    void SummonPhase()
    {
        if (!bl_stopSpawning)
        {
            for (int enemies = 0; enemies < in_enemiestospawn; enemies++)
            {
                GO_enemies.Add(Instantiate(GO_enemy));
                GO_enemies[enemies].GetComponent<scr_BossAdds>().SetGoTimer(enemies * 3);
            }

            bl_stopSpawning = true;
        }

        for (int i = 0; i < GO_enemies.Count; i++)
        {
            if (GO_enemies[i] == null)
                GO_enemies.RemoveAt(i);
            else
                GO_enemies[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }

        in_enemiesleft = GO_enemies.Count;

        if (in_enemiesleft <= 0)
        {
            in_enemiesleft = in_enemiestospawn;
            transform.position = Vector2.MoveTowards(transform.position, V2_bossPosOn, fl_transSpeed);

            if (transform.position.x <= V2_bossPosOn.x)
            {
                currState = STATE.CANETHROW;
            }
        }
    }

    void TramplePhase()
    {
        if (in_tramplescount >= in_numoftramples)
        {
            in_tramplescount = 0;
            currState = STATE.CANETHROW;
        }
    }

    public void BossTakeDamage()
    {
        in_Health -= 1;
    }
}
