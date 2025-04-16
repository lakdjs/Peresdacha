using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R3;
using R3.Triggers;
using UnityEngine.Pool;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreCube;
    [SerializeField] private CollorCube collorCubePrefab;
    [SerializeField] private Text scoreText;

    public SerializableReactiveProperty<int> score = new SerializableReactiveProperty<int>(0);

    [SerializeField] private Material[] materialsAlpha;

    private IObjectPool<CollorCube> pool;

    private void Start()
    {
        pool = new ObjectPool<CollorCube>(OnCreate, OnGet, OnReturned, OnDestroyed);

        ObservableRandomEventTrigger spawnStream = gameObject.AddComponent<ObservableRandomEventTrigger>();
        spawnStream.OnRandomEventObservable().Subscribe(_ =>
        {
            var colorCube = pool.Get();
            colorCube.pool = pool;
        });

        scoreCube.OnTriggerEnterAsObservable().Subscribe(x =>
        {
            if (x.GetComponent<Renderer>().material.color.r == scoreCube.GetComponent<Renderer>().material.color.r)
            {
                score.Value++;
            }
            else
            {
                score.Value--;
            }
        });

        ObservableRandomEventTrigger scoreCubeRandomColorTrigger = scoreCube.AddComponent<ObservableRandomEventTrigger>();
        scoreCubeRandomColorTrigger.OnRandomEventAsObservable().Subscribe(_ =>
        {
            scoreCube.GetComponent<Renderer>().material = materialsAlpha[Random.Range(0, 2)];
        });

        score.SubscribeToText(scoreText);
    }

    private CollorCube OnCreate()
    {
        return Instantiate(collorCubePrefab, new Vector3(Random.Range(-3f, 3f), 5, 0), Quaternion.identity);
    }

    private void OnReturned(CollorCube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void OnGet(CollorCube cube)
    {
        cube.gameObject.SetActive(true);
    }
    private void OnDestroyed(CollorCube cube)
    {
        Destroy(cube.gameObject);
    }
}
