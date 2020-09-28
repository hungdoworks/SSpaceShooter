using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    private Button button;
    private PlayerController playerController;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ActivateShield);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null && (button.interactable != playerController.CanActivateShield()))
        {
            button.interactable = playerController.CanActivateShield();
        }
    }

    private void ActivateShield()
    {
        if (playerController != null && gameManager.IsGamePaused() == false)
        {
            playerController.ActivateShield();
        }
    }
}
