using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class GameMN : Singleton<GameMN>
{
 
    void Start()
    {

        DontDestroyOnLoad(gameObject);
    }
    public void AddButtonHoverEffect(Button button)
    {
        Color originalColor = button.image.color;
        button.transition = Selectable.Transition.None;
        button.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = button.GetComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) =>
        {
            button.transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack);
            button.image.DOColor(Color.yellow, 0.2f);
        });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) =>
        {
            button.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
            button.image.DOColor(originalColor, 0.2f);
        });
        trigger.triggers.Add(entryExit);
    }
    public void Transittion(GameObject appear, GameObject disappear)
    {
        CanvasGroup appearCanvasGroup = appear.GetComponent<CanvasGroup>();
        CanvasGroup disappearCanvasGroup = disappear.GetComponent<CanvasGroup>();
        if (appearCanvasGroup == null)
            appearCanvasGroup = appear.AddComponent<CanvasGroup>();
        if (disappearCanvasGroup == null)
            disappearCanvasGroup = disappear.AddComponent<CanvasGroup>();
        disappearCanvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            disappear.SetActive(false);
            appear.SetActive(true);
            appearCanvasGroup.alpha = 0;
            appearCanvasGroup.DOFade(1, 0.5f);
        });
    }

}
