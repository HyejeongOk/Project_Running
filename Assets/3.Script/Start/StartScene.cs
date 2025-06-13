using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public Text start_txt;

    private void Start()
    {
        start_txt.DOFade(0, 1f)
            .SetLoops(-1, LoopType.Yoyo)    //���� �ݺ� : -1 
            .SetEase(Ease.Linear)
            .SetLink(gameObject);   //������Ʈ ���� �� ����
    }

    public void Taptostart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
