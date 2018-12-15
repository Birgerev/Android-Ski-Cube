using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public static StateManager instance;

    private bool started = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (!started)
        {
            Menu();
            started = true;
        }
    }

	public void Menu () {
        GameManager.instance.Reset();

        MenuManager.instance.SetState(MenuManager.menu);
    }

    public void Game()
    {
        GameManager.instance.Game();

        MenuManager.instance.SetState(MenuManager.game);
    }

    public void Lose()
    {
        MenuManager.instance.SetState(MenuManager.lose);
    }
}
