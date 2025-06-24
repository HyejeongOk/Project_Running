using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager_Map : MonoBehaviour
{
    public List<MapData> MapDatas;
    public List<SelectMap> mapSlots;    // 맵 슬롯

    private SelectMap selectMap = null;

    [Header("Scene에 선택되어 있는 맵")]   
    public MapData selectedMapData; // 현재 선택된 맵
    public Image sceneMap_img; //씬 맵의 이미지 오브젝트
    //public Text selectedMapName;    //맵 이름
    public Text currentMapName; //현재 씬의 맵 이름

    private void Start()
    {
        for(int i = 0; i < MapDatas.Count; i++)
        {
            mapSlots[i].mapdata = MapDatas[i];
            mapSlots[i].Initialized(this);
        }

        SelectMap(mapSlots[0]);
    }

    // 맵 선택
    public void SelectMap(SelectMap select)
    {

        selectMap = select;

        // 선택한 맵 데이터 저장
        selectedMapData = select.mapdata;
        ScrollManager.instance.selectmap = select.mapdata;

        // Scene에 맵 선택 반영
        DisplayonSceneMap();

        // 선택한 맵 갱신
        SelectDisplayedMap();
    }

    // 씬에 선택한 맵 보여주기
    public void DisplayonSceneMap()
    {
        if(selectedMapData.Mapobj == null)
        {
            Debug.Log("선택된 맵 sprite가 비어있습니다.");
            return;
        }

        Debug.Log($" 맵 스프라이트 할당 : {selectedMapData.MapName}");
        // 씬 배경 스프라이트 변경
        sceneMap_img.sprite = selectedMapData.Mapobj;

        // 씬 맵 이름 변경
        currentMapName.text = selectedMapData.MapName;
        Debug.Log($" 맵 스프라이트 할당 : {selectedMapData.Mapobj.name}");
    }

    // 씬에 선택한 맵이 곧 플레이 때 나오는 맵이 된다.
    public void SelectDisplayedMap()
    {
        foreach(var slot in mapSlots)
        {
            if(slot.mapdata == selectedMapData)
            {
                slot.AfterClick();  // 선택 표시
            }

            else
            {
                slot.BeforeClick(); // 해제 표시
            }
        }
    }
}
