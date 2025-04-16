using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class ObservableRandomEventTrigger : ObservableTriggerBase
{
    public float IntervalMax { get; private set; } = 1.0f;
    public float IntervalMin { get; private set; } = 0.1f;

    private Subject<Unit> onRandom;

    private float currentInterval = 0f;
    private float currentIntervalDuration = 0f;

    private void Start()
    {
        currentInterval = GenerateNewInterval();

        transform.UpdateAsObservable().Subscribe(_ =>
        {
            currentIntervalDuration += Time.deltaTime;

            if (currentIntervalDuration >= currentInterval)
            {
                if (onRandom != null)
                {
                    onRandom.OnNext(Unit.Default);
                }
                currentIntervalDuration = 0f;
                currentInterval = GenerateNewInterval();
            }
        });
    }

    public Subject<Unit> OnRandomEventAsObservable()
    {
        return onRandom ??= new Subject<Unit>();
    }

    protected override void RaiseOnCompletedOnDestroy()
    {
        onRandom?.OnCompleted();
    }

    private float GenerateNewInterval()
    {
        return Random.Range(IntervalMin,IntervalMax);
    }
}
