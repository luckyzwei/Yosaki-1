using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniBossController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _gameObjects;

    private void OnEnable()
    {
        if (GameManager.Instance.bossId == 106||GameManager.Instance.bossId == 107)
        {
            foreach (var prefab in _gameObjects)
            {
                prefab.SetActive(false);
            }
            _gameObjects[Random.Range(0, _gameObjects.Count)].SetActive(true);
        }
        
    }


    public void GameObjectStop()
    {
        gameObject.SetActive(false);
    }

    public void Shuffle()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            int randomIndex = Random.Range(i, _gameObjects.Count);

            (_gameObjects[i], _gameObjects[randomIndex]) = (_gameObjects[randomIndex], _gameObjects[i]);
            _gameObjects[i].transform.localPosition = new Vector3(0, -3f + i * 3.4f, 0);
        }
    }
}
