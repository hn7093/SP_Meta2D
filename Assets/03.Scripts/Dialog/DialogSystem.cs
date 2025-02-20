using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct Speaker
{
    public Image imageCharacter; // 캐릭터 이미지
    public Image imageDialog; // 대화창 이미지 (좌,우)
    public TextMeshProUGUI textName; // 화자 이름 UI
    public TextMeshProUGUI textDialog; // 대화 내용 UI
    public GameObject arrow; // 완료시 보이는 커서
}
[System.Serializable]
public struct DialogData
{
    public int speakerIdx; // 문장의 순서
    public string name; // 캐릭터 이름
    [TextArea(3, 5)]
    public string dialog; // 대사
}
public class DialogSystem : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private Speaker[] speakers; // 현재 화자 배열
    [SerializeField] private DialogData[] dialogs; // 대사 목록
    [SerializeField] private bool isAutoStart = true; // 자동 시작인가
    private bool isFirst = true; // 최초 1화만 호출 하기 위한 변수
    private int currentDialogIdx = -1; // 현재 대화 인덱스
    private int currentSpeakerIdx = 0; // 현재 화자 인덱스


    // 출력 옵션
    private float speakSpeed = 0.1f; // 출력 속도
    private bool isSpeaking; // 출력 여부


    private void Awake()
    {
        //Setup();
    }
    public void StartDialog()
    {
        uiObject.SetActive(true);
        StartCoroutine(ActiveDialog());
    }
    private IEnumerator ActiveDialog()
    {
        yield return new WaitUntil(() => UpdateDialog());
    }
    private void Setup()
    {
        // 게임 오브젝트 비활성화
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
            // 마지막 이미지만 보이도록
            speakers[i].imageCharacter.gameObject.SetActive(true);
        }
    }
    public bool UpdateDialog()
    {
        // 처음 대사
        if (isFirst == true)
        {
            // 초기화
            Setup();
            if (isAutoStart)
                SetNextDialog();
            isFirst = false;
        }
        if (Input.GetMouseButtonDown(0))// 좌클릭
        {
            // 타이핑 속도별 출력중이라면 중지
            if (isSpeaking == true)
            {
                isSpeaking = false;
                //중지후 전체 출력후 종료
                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIdx].textDialog.text = dialogs[currentDialogIdx].dialog;
                speakers[currentSpeakerIdx].arrow.SetActive(true);
                return false;
            }
            // 다음 대사가 있다면 진행
            if (dialogs.Length > currentDialogIdx + 1)
            {
                SetNextDialog();
            }
            else
            {
                // 현재 대화 캐릭터와 UI 숨김
                for (int i = 0; i < speakers.Length; i++)
                {
                    SetActiveObjects(speakers[i], false);
                    speakers[i].imageCharacter.gameObject.SetActive(false);
                }
                // 종료
                uiObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    // 다음 대사 진행
    public void SetNextDialog()
    {
        // 이전 화자 비활성
        SetActiveObjects(speakers[currentSpeakerIdx], false);

        // 다음 순번
        currentDialogIdx++;
        // 다음 화자
        currentSpeakerIdx = dialogs[currentDialogIdx].speakerIdx;

        // 오브젝트 활성화, 대사 설정
        SetActiveObjects(speakers[currentSpeakerIdx], true);
        speakers[currentSpeakerIdx].textName.text = dialogs[currentDialogIdx].name;
        // 속도별 출력
        //speakers[currentSpeakerIdx].textDialog.text = dialogs[currentDialogIdx].dialog;
        StartCoroutine("OnTypingText");
    }

    public void SetActiveObjects(Speaker speaker, bool isActive)
    {
        // 오브젝트 비활성화
        speaker.imageDialog.gameObject.SetActive(isActive);
        speaker.textName.gameObject.SetActive(isActive);
        speaker.textDialog.gameObject.SetActive(isActive);
        speaker.arrow.SetActive(false);

        // 캐릭터 알파값 조정 - 반투명
        Color color = speaker.imageCharacter.color;
        color.a = isActive ? 1 : 0.2f;
        speaker.imageCharacter.color = color;
    }


    // 속도별 출력
    private IEnumerator OnTypingText()
    {
        int idx = 0;
        isSpeaking = true;
        // 한글자씩 출력
        while (idx <= dialogs[currentDialogIdx].dialog.Length)
        {
            speakers[currentSpeakerIdx].textDialog.text = dialogs[currentDialogIdx].dialog.Substring(0, idx);
            idx++;
            yield return new WaitForSeconds(speakSpeed);
        }
        isSpeaking = false;
        speakers[currentSpeakerIdx].arrow.SetActive(true);
    }
}
