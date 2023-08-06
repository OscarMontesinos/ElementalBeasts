using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEditorManager : MonoBehaviour
{
    static public TeamEditorManager Instance;

    public TeamEditorPlayerManager player1;
    public TeamEditorPlayerManager player2;

    int playersReady;

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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerReady()
    {
        playersReady++;
        if (playersReady == 2)
        {
            playersReady = 0;
            player1.StartCombat();
            player2.StartCombat();
        }
    }
    public void PlayerReadyForCombat()
    {
        playersReady++;
        if (playersReady == 2)
        {
            LoadCombat();
        }
    }
    void LoadCombat()
    {

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("SampleScene");

        Destroy(player1.gameObject);
        Destroy(player2.gameObject);
        Destroy(gameObject);
    }
}
