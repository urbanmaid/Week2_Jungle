using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

// string을 인수로 받는 UnityEvent를 새로 정의합니다.
[System.Serializable]
public class UnityEventString : UnityEvent<string> { }

public class LocalizedEventTrigger : MonoBehaviour
{
    [Header("번역할 문자열")]
    [SerializeField] private LocalizedString localizedString;

    [Header("번역 완료 후 호출할 이벤트")]
    [Tooltip("번역이 완료된 string을 인수로 전달합니다.")]
    public UnityEventString onStringLoaded;

    private Coroutine loadCoroutine;

    // 이 함수를 버튼의 OnClick() 등에서 호출합니다.
    public void Trigger()
    {
        if (loadCoroutine != null)
        {
            StopCoroutine(loadCoroutine);
        }
        loadCoroutine = StartCoroutine(LoadStringAndInvoke());
    }

    private IEnumerator LoadStringAndInvoke()
    {
        var handle = localizedString.GetLocalizedStringAsync();
        yield return handle;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            // 로딩 성공 시, 번역된 문자열(handle.Result)을 가지고 onStringLoaded 이벤트를 호출합니다.
            onStringLoaded.Invoke(handle.Result);
        }
        else
        {
            Debug.LogError($"'{localizedString.TableEntryReference}' 키의 번역 로딩에 실패했습니다.");
        }
        loadCoroutine = null;
    }
}