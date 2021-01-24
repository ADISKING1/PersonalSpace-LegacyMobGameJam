using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    #region Singleton class: Test

    public static Gamemanager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    #region Fields

    public Transform StartPosition;
    public Transform EndPosition;

    public GameObject Player;
    private Player PlayerScript;

    public enum Gamestate { playing, notPlaying }
    public Gamestate currentState;
    public int countLevel;
    public static int currentLevel;
    private GameObject Level;
    public GameObject Ready;
    public AudioClip lvlSound;
    public AudioClip lbBreak;
    public ParticleSystem ps;
    public Text levelText;
    private int failedCount;
    #endregion

    private void Start()
    {
        ps.Pause();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<Player>();
        StartCoroutine("LoadLevel", currentLevel);
    }
    public void Finished()
    {
        if (currentLevel == countLevel)
        {
            failedCount = 0;
            currentLevel = -1;
        }
        StartCoroutine("LoadLevel", ++currentLevel);
    }

    public void PausePS()
    {
        ps.Stop();
    }

    private IEnumerator LoadLevel(int i)
    {
        PlayClip(lvlSound);
        Player.SetActive(false);
        currentState = Gamestate.notPlaying;
        float waitTime;
        Destroy(Level);
        if (i == countLevel)
        {
            Ready.SetActive(false);
            waitTime = 0;
            ps.Play();
            Invoke("PausePS", 2);
            levelText.text = "✌";
        }
        else
        {
            Ready.SetActive(true);
            ps.Stop();
            waitTime = 1.4f;
            levelText.text = i.ToString();// + " / " + countLevel.ToString();
        }
        yield return new WaitForSeconds(waitTime);
        Ready.SetActive(false);
        Level = Instantiate(Resources.Load<GameObject>("L/L" + i), transform.position, Quaternion.identity);

        Player.transform.position = StartPosition.position;
        PlayerScript.ResetHealth();

        currentState = Gamestate.playing;
        Player.SetActive(true);
    }
    public IEnumerator AfterMath(float waitTime, Vector2 pos, Trap trap)
    {
        Debug.Log(trap);
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject col in trap.GetCollisions())
        {
            col.GetComponent<Charater>().SetGoalPos(pos);
        }
    }
    private void OnEnable()
    {
        EventManager.GameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.GameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        Debug.Log("GameOver!");
        failedCount++;
        PlayClip(lbBreak);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine("LoadLevel", currentLevel);
    }
    public void PlayClip(AudioClip ac)
    {
        GetComponent<AudioSource>().PlayOneShot(ac);
    }
    public string getFailCount()
    {
        return failedCount.ToString();
    }
}
