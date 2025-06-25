using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public List<CharacterData> characterDatas;
    public Transform spawner;
    public AudioClip checkClip;

    public GameObject currentcharacter;
    private int selectIndex;

    private AudioSource audioSource;


    // PlayerPrefs : ����Ƽ���� ������ ����� ����� �����ϴ� Ŭ����
    // GetInt("key", value)  : Key�� ����� ���� �ҷ��´�. ���� ����� ���� ������ 0
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        string savedID = PlayerPrefs.GetString("LobbyCharacter", characterDatas[0].CharacterID);

        for(int i = 0; i < characterDatas.Count; i++)
        {
            if(characterDatas[i].CharacterID == savedID)
            {
                selectIndex = i;
                    break;
            }
        }

        ShowCharacter(selectIndex);
    }

    public void OnClickRightArrow()
    {
        audioSource.clip = checkClip;
        audioSource.Play();

        selectIndex++;

        if(selectIndex > characterDatas.Count-1)
        {
            selectIndex = 0;
        }
        SelectCharacter();
    }

    public void OnClickLeftArrow()
    {
        audioSource.clip = checkClip;
        audioSource.Play();

        selectIndex--;
        if(selectIndex < 0)
        {
            selectIndex = characterDatas.Count - 1; //������ ��ȯ
        }

        SelectCharacter();
    }

    public void SelectCharacter()
    {
        string selectedID = characterDatas[selectIndex].CharacterID;

        PlayerPrefs.SetString("LobbyCharacter", selectedID); 
        PlayerPrefs.Save();

        ShowCharacter(selectIndex);
    }

    public void ShowCharacter(int index)
    {
        if(currentcharacter != null)
        {
            Destroy(currentcharacter);
        }

        currentcharacter = Instantiate(characterDatas[index].Lobby_obj, spawner.position, Quaternion.identity, spawner);
    }
}
