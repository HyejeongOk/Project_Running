using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public List<CharacterData> characterDatas;
    public Transform spawner;

    public GameObject currentcharacter;
    private int selectIndex;

    private void Start()
    {
        selectIndex = PlayerPrefs.GetInt("LobbyCharacter", 0);

        ShowCharacter(selectIndex);
    }

    public void OnClickRightArrow()
    {
        selectIndex++;

        if(selectIndex > characterDatas.Count-1)
        {
            selectIndex = 0;
        }
        SelectCharacter();
    }

    public void OnClickLeftArrow()
    {
        selectIndex--;
        if(selectIndex < 0)
        {
            selectIndex = characterDatas.Count - 1; //끝으로 순환
        }

        SelectCharacter();
    }

    public void SelectCharacter()
    {
        PlayerPrefs.GetInt("LobbyCharacter", selectIndex);
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
