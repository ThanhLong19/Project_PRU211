using DG.Tweening;
using JetBrains.Annotations;
using ScriptableObject;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemScriptableObject itemData;

    private float _timer;

    private void OnEnable()
    {
    }

    private void Start()
    {
        transform.DOMoveY(-10f, GameController.Instance.speedMultiplier).SetSpeedBased(true).SetEase(Ease.Linear)
            .OnComplete(
                () => { Destroy(gameObject); });
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        transform.DOKill();
        gameObject.SetActive(false);
    }

    public virtual void OnActive([CanBeNull] PlayerController player)
    {
    }

    public virtual void OnDeactivate([CanBeNull] PlayerController player)
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
    }
}