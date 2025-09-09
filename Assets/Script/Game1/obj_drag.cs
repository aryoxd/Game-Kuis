using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Transform originalParent;

    [SerializeField] private DropZone assignedDropZone;

    private DropZone currentDropZone;
    private bool isSnapped = false;

    private AudioSource audioSource;

    public AudioClip snapSound;
    public AudioClip failSound;

    private bool hasCountedCorrect = false; // untuk memastikan jawaban benar hanya dihitung 1x

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        audioSource = GetComponent<AudioSource>();

        if (assignedDropZone == null)
            Debug.LogWarning($"DropZone belum di-assign di {gameObject.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSnapped && !LifeManager.Instance.CanUseLife())
        {
            Debug.Log("Nyawa habis, tidak bisa memindahkan object yang sudah ditempatkan.");
            eventData.pointerDrag = null;
            return;
        }

        if (isSnapped && currentDropZone != null)
        {
            currentDropZone.SetOccupied(false);

            if (hasCountedCorrect && ScoreManager.Instance != null)
            {
                ScoreManager.Instance.KurangiBenar();
                hasCountedCorrect = false;
            }

            currentDropZone = null;
            isSnapped = false;

            LifeManager.Instance.UseLife();
        }

        transform.SetParent(canvas.transform);
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool droppedOnDropZone = false;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            DropZone dz = result.gameObject.GetComponent<DropZone>();
            if (dz != null && !dz.IsOccupied())
            {
                droppedOnDropZone = true;

                transform.SetParent(dz.transform);
                rectTransform.anchoredPosition = Vector2.zero;

                dz.SetOccupied(true);
                currentDropZone = dz;
                isSnapped = true;

                if (dz == assignedDropZone)
                {
                    if (!hasCountedCorrect && ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.TambahBenar();
                        hasCountedCorrect = true;
                    }

                    if (snapSound != null)
                        audioSource.PlayOneShot(snapSound);
                }
                else
                {
                    if (failSound != null)
                        audioSource.PlayOneShot(failSound);
                }

                break;
            }
        }

        if (!droppedOnDropZone)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void ResetPosition()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;

        if (currentDropZone != null)
        {
            currentDropZone.SetOccupied(false);
            currentDropZone = null;
        }

        isSnapped = false;

        // Kalau reset & sudah dihitung benar, kurangi skor
        if (hasCountedCorrect && ScoreManager.Instance != null)
        {
            ScoreManager.Instance.KurangiBenar();
            hasCountedCorrect = false;
        }
    }
}
