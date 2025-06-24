using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] AudioClip btnclip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBtn()
    {
        audioSource.clip = btnclip;
        audioSource.Play();

        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
