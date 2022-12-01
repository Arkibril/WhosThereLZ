using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class HesHere : MonoBehaviour
{

    [Header("Intrusion Type")]
    public bool Spam;
    public bool Stay;
   
    private bool ItsGood;
    private bool TimeLeftFinish;
    private bool Triggered;

    [Header("Click / Time Number")]
    public int NumberSpam = 20;
    public int StayTime;

    private int NumberTouchEnter;
    private float LeftTime = 15;
    private float CurrentTime;
    public float Y;
    public float Ysave;

    [Header("GameObject")]
    public AudioSource music;

    [Header("GameObject")]
    public GameObject Monster;
    public Transform MonsterSpawn;
    public GameObject Indicator;
    public GameObject tEST;

    private PostProcessVolume postProcessVolume;
    private ChromaticAberration ChromaAberation;
    private float ChromaValue = 0.22f;

    private CameraShakeScript _cameraShakeScript;


    void Start()
    {
        music = GameObject.FindWithTag("HorrorMusic").GetComponent<AudioSource>();
        StartCoroutine(CheckY());
        Y = tEST.transform.position.y;
        //anim.Play(animName);
        Indicator.SetActive(true);
        GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
        MonsterScript.GetComponent<MonsterScript>().enabled = false;
        CurrentTime = LeftTime;
        postProcessVolume.profile.TryGetSettings(out ChromaAberation);

    }

    // Update is called once per frame
    void Update()
    {
        Y = tEST.transform.position.y;
        _cameraShakeScript = GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<CameraShakeScript>();
        Y = tEST.transform.position.y;
        postProcessVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out ChromaAberation);
        CurrentTime -= 1 * Time.deltaTime;
        
        if(Y == Ysave + 0.7f) 
        {
            music.Play();
            Monster.transform.localScale = new Vector3(10, 10, 10);
            Instantiate(Monster, MonsterSpawn);
            LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;
            Triggered = false;
            Indicator.SetActive(false);
        }

        if (Triggered) 
        {
            ChromaAberation.intensity.value += 1 * Time.deltaTime / 2;
        }
        else 
        {
            ChromaAberation.intensity.value = 0.233f;
        }


        if (CurrentTime <= 0 && Y >= Ysave + 0.7f)  
        {
            music.Play();
            Monster.transform.localScale = new Vector3(10, 10, 10);
            Instantiate(Monster, MonsterSpawn);
            LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;
            Triggered = false;
            Indicator.SetActive(false);
        }
        else if(CurrentTime <= 0 && Y <= Ysave + 0.6f)
        {
            ChromaAberation.intensity.value = 0.233f;
            Triggered = false;
            LeftTime = 20f;
            CurrentTime = 15f;
            this.GetComponent<HesHere>().enabled = false;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = true;
            Indicator.SetActive(false);
        }


        if(CurrentTime <= 15)
        {
            Triggered = true;
        }

       // if(NumberTouchEnter == NumberSpam) 
        //{
       //     LeftTime = 20f;
       //     this.GetComponent<HesHere>().enabled = false;
      //      GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
      //      MonsterScript.GetComponent<MonsterScript>().enabled = true;
     //       Indicator1.SetActive(true);
     //   }

        if( GameObject.FindGameObjectWithTag("MonsterCheck").GetComponent<MonsterCheck>().HesHere == true) 
        {
            this.gameObject.GetComponent<HesHere>().enabled = false;
        }

        tEST.transform.Translate(Vector3.up * Time.deltaTime / 19);
      
    }

    void OnTriggerStay(Collider other) 
    {
        if (tEST.transform.position.y >= Ysave)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tEST.transform.Translate(Vector3.down * Time.deltaTime / 1.5f);
                _cameraShakeScript.CameraShake();
            }
        }
    }

    IEnumerator SpamWaiting() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            NumberTouchEnter++;
        }
        yield return new WaitUntil(() => ItsGood == true);
    }

    IEnumerator TimeLeft() 
    {
        yield return new WaitForSeconds(1f);
        TimeLeftFinish = true;
    }

    IEnumerator CheckY() 
    {
        yield return new WaitForSeconds(0.1f);
        Ysave = Y;
    }
}
