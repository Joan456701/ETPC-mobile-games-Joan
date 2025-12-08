using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public static AmmoController instance;

    public int maxAmmoCount = 4;
    public float offset = 0.05f;
    public Transform birdPrefab;

    public List<BirdController> _birds;

    private int _ammoCount;

    void Start()
    {
        instance = this;
        _ammoCount = maxAmmoCount;

        float size = birdPrefab.GetComponent<CircleCollider2D>().radius * 2f + offset;

        for (int i = 0; i < maxAmmoCount - 1; i++)
        {
            Vector3 pos = transform.position + Vector3.left * i * size;
            Transform bird = Instantiate(birdPrefab, pos, Quaternion.identity);
            bird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            BirdController bCtr = bird.GetComponent<BirdController>();
            _birds.Add(bCtr);
        }

        _birds.Reverse();
    }

    void Update()
    {

    }

    public BirdController Reload()
    {
        _ammoCount = _ammoCount - 1;

        if (CheckVictory())
        {
            GameStateManager.Instance.ChangeGameState(GameState.StateType.WIN);
            return null;
        }

        if (_ammoCount > 0)
        {
            return _birds[_ammoCount - 1];
        }
        else
        {
            GameStateManager.Instance.ChangeGameState(GameState.StateType.OVER);
            return null;
        }
    }

    private bool CheckVictory()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        return enemies.Length == 0;
    }
}
