using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [HideInInspector] public int score;
    [HideInInspector] public int highScore;

    [HideInInspector] public bool firstGame = true;

    [SerializeField] int numOfGames = 4;

    bool playing = false;
    bool waiting = false;

    public bool Playing {
        get {return playing;}
        set {
            if(value) StartGame();
            else StartCoroutine(GameOver());
            playing = value;
            }
    }
    int gameID = 0;
    int GameID {get {return gameID;}
    set{
        // gameID = ((value - 1) % numOfGames) + 1;
       gameID = ((value - 1) % numOfGames) + 1;
       if(value>gameID) NextWave();
       ChangeGame();}}

    int trackID = 0;
    int TrackID {get {return trackID;}
    set{trackID = value % tracks.Length;
        tracks[trackID].Play(); }}

    GlitchTile[] glitchTiles;

    float timeScale = 1;

    void Awake(){        
        if (gm != null && gm != this)
            Destroy(gameObject);
        else
        {
            gm = this;
            DontDestroyOnLoad(transform);
        }
        glitchTiles = FindObjectsOfType<GlitchTile>();
    }

    void Update(){
        if(Playing){
            print("Timer: "+Timer+"; Timer10: "+Timer10);
            if(Timer10 >= 10)
            {
                savedTime10 += 10 + glitchDelay;
                savedTime -= glitchDelay;
                // Timer10 -= (10 + glitchDelay);
                // Timer -= glitchDelay;
                StartCoroutine(FullGlitch());   
            }
        }
        else if (!waiting) {
            if((Input.GetButtonDown("Up") || Input.GetButtonDown("Down") ||
            Input.GetButtonDown("Right") || Input.GetButtonDown("Left")))
                Playing = true;            
        }
    }

    [SerializeField] float glitchDelay = 3;
    [SerializeField] float gameOverDelay = 3;

    IEnumerator FullGlitch()
    {        
        Time.timeScale = 0;
        glitch.Play();
        Glitch();
        yield return new WaitForSecondsRealtime(glitchDelay);
        Glitch(false);
        Time.timeScale = timeScale;
        click.Play();
        NextGame();
    }

    void Glitch(bool value = true){
        foreach (GlitchTile tile in glitchTiles)
        {
            tile.Glitch(value);
        }
    }

    // float savedTime;
    // float Timer {get{return Time.unscaledTime - savedTime;}
    // set{savedTime = Time.unscaledTime - value;
    //     savedTime10 = savedTime;}}

    // float savedTime10;
    // float Timer10 {get{return Time.unscaledTime - savedTime10;}
    // set{savedTime10 = Time.unscaledTime - value;}}

    float savedTime;
    float Timer {get{return Time.unscaledTime - savedTime;}
    set{savedTime = Time.unscaledTime - value;
        savedTime10 = savedTime;}}

    float savedTime10;
    float Timer10 {get{return Time.unscaledTime - savedTime10;}
    set{savedTime10 = Time.unscaledTime - value;}}

    void StartGame(){
        menuTrack.Stop();
        click.Play();

        GameID = 1;
        TrackID = 0;
        Timer = 0;
        firstGame = false;

        // StartCoroutine(WaitChangeGame());
    }

    // IEnumerator WaitChangeGame(){
        
    //     yield return new WaitForSeconds(10);
    //     while(Playing)
    //     {
    //         NextGame();
    //         yield return new WaitForSeconds(10);            
    //     }
    // }

    // void GameOver(){

    //     Time.timeScale = 1;
    //     score = (int)Mathf.Floor(Timer) * 10;
    //     if(score >= highScore) highScore = score;
        
    //     ChangeScene(0);
    // }

    IEnumerator GameOver(){
        waiting = true;
        tracks[trackID].Stop();
        score = (int)Mathf.Floor(Timer) * 10;
        if(score >= highScore) highScore = score;
        
        yield return new WaitForSeconds(gameOverDelay);
        timeScale = 1;
        Time.timeScale = 1;
        
        ChangeScene(0);
        menuTrack.Play();
        waiting = false;
    }

    void NextWave(){
        timeScale += .2f;
        Time.timeScale = timeScale;
    }

    void NextGame(){GameID++; TrackID++;}

    void ChangeGame(){
        ChangeScene(gameID);
    }

    void ChangeScene(int id){
        UnityEngine.SceneManagement.SceneManager.LoadScene(id);
    }

    public AudioSource click;
    public AudioSource explode;
    public AudioSource tap;
    public AudioSource hit;
    public AudioSource jump;
    public AudioSource glitch;

    [SerializeField] AudioSource[] tracks;
    [SerializeField] AudioSource menuTrack;
}