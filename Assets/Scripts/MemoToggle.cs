using System;
using System.Collections;
using UnityEngine;

public class MemoToggle : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] string memoContent;

    public void ToggleMemo()
    {
        if(gameObject.activeSelf)
        {
            HideMemo();
        }
        else
        {
            ShowMemo();
        }
    }

    private void ShowMemo()
    {
        gameObject.SetActive(true);
        StartCoroutine(HideMemoAuto());
    }

    private IEnumerator HideMemoAuto()
    {
        yield return new WaitForSeconds(8f);
        HideMemo();
    }

    private void HideMemo()
    {
        gameObject.SetActive(false);
    }

    public void PassMemoContent()
    {
        GameManager.instance.ShowMemoText(memoContent);
    }
}
