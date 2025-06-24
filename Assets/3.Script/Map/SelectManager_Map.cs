using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager_Map : MonoBehaviour
{
    public List<MapData> MapDatas;
    public List<SelectMap> mapSlots;    // �� ����

    private SelectMap selectMap = null;

    [Header("Scene�� ���õǾ� �ִ� ��")]   
    public MapData selectedMapData; // ���� ���õ� ��
    public Image sceneMap_img; //�� ���� �̹��� ������Ʈ
    //public Text selectedMapName;    //�� �̸�
    public Text currentMapName; //���� ���� �� �̸�

    private void Start()
    {
        for(int i = 0; i < MapDatas.Count; i++)
        {
            mapSlots[i].mapdata = MapDatas[i];
            mapSlots[i].Initialized(this);
        }

        SelectMap(mapSlots[0]);
    }

    // �� ����
    public void SelectMap(SelectMap select)
    {

        selectMap = select;

        // ������ �� ������ ����
        selectedMapData = select.mapdata;
        ScrollManager.instance.selectmap = select.mapdata;

        // Scene�� �� ���� �ݿ�
        DisplayonSceneMap();

        // ������ �� ����
        SelectDisplayedMap();
    }

    // ���� ������ �� �����ֱ�
    public void DisplayonSceneMap()
    {
        if(selectedMapData.Mapobj == null)
        {
            Debug.Log("���õ� �� sprite�� ����ֽ��ϴ�.");
            return;
        }

        Debug.Log($" �� ��������Ʈ �Ҵ� : {selectedMapData.MapName}");
        // �� ��� ��������Ʈ ����
        sceneMap_img.sprite = selectedMapData.Mapobj;

        // �� �� �̸� ����
        currentMapName.text = selectedMapData.MapName;
        Debug.Log($" �� ��������Ʈ �Ҵ� : {selectedMapData.Mapobj.name}");
    }

    // ���� ������ ���� �� �÷��� �� ������ ���� �ȴ�.
    public void SelectDisplayedMap()
    {
        foreach(var slot in mapSlots)
        {
            if(slot.mapdata == selectedMapData)
            {
                slot.AfterClick();  // ���� ǥ��
            }

            else
            {
                slot.BeforeClick(); // ���� ǥ��
            }
        }
    }
}
