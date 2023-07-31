using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] private GameObject hidingPanel;
    

    private void Awake() 
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void Start() 
    {
        playerHealth.onDeath += InvokeUI;
        FinishTrigger.instance.gameFinished += InvokeUI;
    }

    private void InvokeUI()
    {
        hidingPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
