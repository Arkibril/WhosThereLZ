using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{

    public GameObject Player;
    private Player _player;
    public Image SprintBarLevelImage;


    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar();
    }

    void HealthBar()
    {
        SprintBarLevelImage.fillAmount = _player.sprint / _player.maxSprint;

        _player.sprint = Mathf.Clamp(_player.sprint, 0f, _player.maxSprint);
    }
}

