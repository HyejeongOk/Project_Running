using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public MapData mapdata;

    [Header("선택하기 버튼")]
    public Image select_btn;    //선택하기
    public Image check_btn;

    [Header("정보")]
    public Text name_txt;
    public Image Previewbg;

    private SelectManager_Map selectmap_mgr;

    public void Initialized(SelectManager_Map mgr)
    {
        selectmap_mgr = mgr;

        Previewbg.sprite = mapdata.Mapobj;
        name_txt.text = mapdata.MapName;

    }

    public void OnSelectButtonClicked()
    {
        selectmap_mgr.SelectMap(this);
    }

    public void BeforeClick()
    {
        select_btn.gameObject.SetActive(true);
        check_btn.gameObject.SetActive(false);
    }

    public void AfterClick()
    {
        select_btn.gameObject.SetActive(false);
        check_btn.gameObject.SetActive(true);
    }
}
