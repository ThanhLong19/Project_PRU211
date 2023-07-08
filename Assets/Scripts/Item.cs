using DG.Tweening;
using JetBrains.Annotations;
using ScriptableObject;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemScriptableObject itemData;

    private float _timer;
    private bool _isActivateInsidePlayer;

    private void OnEnable()
    {
    }

    private void Start()
    {
        _isActivateInsidePlayer = false;
        transform.DOMoveY(-10f, GameController.Instance.speedMultiplier).SetSpeedBased(true).SetEase(Ease.Linear)
            .OnComplete(
                () => { Destroy(gameObject); });
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    public virtual void OnActive([CanBeNull] PlayerController player)
    {
    }

    public virtual void OnDeactivate([CanBeNull] PlayerController player)
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (_isActivateInsidePlayer)
        {
        }
    }
}