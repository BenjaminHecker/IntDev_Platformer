using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    [System.Serializable]
    struct Spinner
    {
        public Transform sprite;
        public float rotateSpeed;
    }
    [SerializeField] private Spinner[] spinners;

    private void Update()
    {
        foreach (Spinner spinner in spinners)
            spinner.sprite.Rotate(0, 0, spinner.rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(levelToLoad);
    }
}
