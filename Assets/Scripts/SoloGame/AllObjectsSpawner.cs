/*

	Скрипт висит на камере и спавнит все объекты.

    <----------------------------------------------------------------------------------->

    Спавн привязан ко времени.

    В интервале времени рандомно выбирается время, через которое заспавнится
    следующий объект. Интервал со временем уменьшается, пока по 5 процентов за шаг,
    то есть раз в 20 секунд.

	<----------------------------------------------------------------------------------->

    ВЫБОР ПОЗИЦИИ ДЛЯ ЗАПУСКА ОБЪЕКТА

	last - массив, в котором хранятся последние 3 запущенных объекта.

	Когда нужно запустить объект, вызывается ModifyLast().

	ModifyLast находит участки, в которых можно запустить объект так, чтобы он
	не прикасался к последним трём запущеным объектам. prevs - массив,
	который представляет из себя отсортированный last с помощью quicksort.
	Участки записываются в массив пар places, где key(или first) - начало
	участка, а value(или second) - конец.

	Перед запуском FindPos() происходит сдвиг:
	last[0] = last[1];
	last[1] = last[2];
	Таким образом последние два значения становятся двумя предпоследними.

	Далее вызывается float FindPos(args).
	Эта функция уже точно находит позицию, в которой будет запущен следующий
	объект и возвращает значение в last[2].
    Функция рандомно выбирает интервал значений из доступных, и уже в нём
    рандомно выбирает позицию точного запуска.

	После этого управление возвращается в CheckSpawn(), где и используется
	только что полученное значение last[2].

	<----------------------------------------------------------------------------------->

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjectsSpawner : MonoBehaviour
{
    public GameObject events;
    public GameObject[] obstacles;
    public GameObject[] bonuses;
    public GameObject coin;
    public GameObject firePillar;


    private float[] last = new float[3];

    private float lastObjectLaunchTime;
    private float currentLaunchInterval;
    private float minimalIntervalTime;
    private float maximalIntervalTime;
    private float lastIntervalChangeTime;
    private bool minimalReached;

    private float lastBonusLaunchTime;
    private float lastEventLaunchTime;

    private float lastCoinLaunchTime;
    private float currentCoinInterval;

    private float lastWindLaunchTime;
    private float currentWindInterval;
    private float minimalWindInterval;
    private float maximalWindInterval;
    private bool secondSpawned;
    private float lastSpawnPos;
    [SerializeField]
    private float[] modifiers;
    [SerializeField]
    private float[] minimalWindIntervals;
    [SerializeField]
    private float[] maximalWindIntervals;


    private HeroController HController;
    private SpeedModifierController speedModifierController;

    private ScoreCounter scoreCounter;
    private GameObject camera;

    private bool jetpackJustStarted;

    void Start()
    {
        Time.timeScale = 1f;

        last[0] = -2f;
        last[1] = 0f;
        last[2] = 2f;

        lastObjectLaunchTime = Time.time;
        lastIntervalChangeTime = Time.time;
        lastBonusLaunchTime = Time.time;
        lastEventLaunchTime = Time.time;
        minimalIntervalTime = 1f;
        maximalIntervalTime = 2.5f;
        currentLaunchInterval = Random.Range(minimalIntervalTime, maximalIntervalTime);
        currentCoinInterval = Random.Range(10f, 15f);
        minimalReached = false;

        lastWindLaunchTime = Time.time;
        minimalWindInterval = 30f;
        maximalWindInterval = 70f;
        secondSpawned = false;
        lastSpawnPos = Random.Range(0, 2) == 0 ? -0.3f : 0.3f;
        currentWindInterval = Random.Range(minimalWindInterval, maximalWindInterval);


        HController = GameObject.Find("Hero").GetComponent<HeroController>();

        camera = GameObject.Find("Main Camera");
        scoreCounter = camera.GetComponent<ScoreCounter>();
        speedModifierController = camera.GetComponent<SpeedModifierController>();
    }

    void Update()
    {
        CheckObstacleSpawn();
        CheckInterval();

        CheckCoinSpawn();

        CheckWindSpawn();
    }

    void SpawnObstacle()
    {
        if (scoreCounter.score >= 100)
        {
            Object.Instantiate(obstacles[Random.Range(0, 3)], new Vector3(last[2], -6f, 0f), Quaternion.identity);
        }
        else
        {
            Object.Instantiate(obstacles[Random.Range(1, 3)], new Vector3(last[2], -6f, 0f), Quaternion.identity);
        }
    }

    void CheckObstacleSpawn()
    {
        if (Time.time - lastObjectLaunchTime >= currentLaunchInterval)
        {
            lastObjectLaunchTime = Time.time;
            currentLaunchInterval = Random.Range(minimalIntervalTime, maximalIntervalTime);

            ModifyLast();
            int rand = Random.Range(0, 100);
            if (rand < 75) // spawn obstacle (бомба должна быть нулевой в массиве)
            {
                SpawnObstacle();
            }
            else if (rand >= 75 && rand < 90) // spawn bonus
            {
                if (Time.time - lastBonusLaunchTime >= 10f) // bonus delay
                {
                    if (HController.IsUsingJetpack())
                    {
                        Object.Instantiate(bonuses[Random.Range(0, 4)], new Vector3(last[2], -6f, 0f), Quaternion.identity);
                    }
                    else
                    {
                        Object.Instantiate(bonuses[Random.Range(0, 5)], new Vector3(last[2], -6f, 0f), Quaternion.identity);
                    }
                    
                    lastBonusLaunchTime = Time.time;
                }
                else
                {
                    SpawnObstacle();
                }
            }
            else // spawn event
            {
                if (Time.time - lastEventLaunchTime >= 45f) // event delay
                {
                    Object.Instantiate(events, new Vector3(0, -4f, 0f), Quaternion.identity);
                    lastEventLaunchTime = Time.time;
                }
                else 
                {
                    SpawnObstacle();
                }
            }
        }
    }

    void CheckInterval()
    {
        if (!minimalReached)
        {
            if (Time.time - lastIntervalChangeTime >= 20f)
            {
                minimalIntervalTime *= 0.95f;
                maximalIntervalTime *= 0.95f;
                if (minimalIntervalTime <= 0.5f)
                {
                    minimalReached = true;
                }
                lastIntervalChangeTime = Time.time;
            }
        }
    }

    void CheckCoinSpawn()
    {
        if (Time.time - lastCoinLaunchTime >= currentCoinInterval)
        {
            ModifyLast();
            Object.Instantiate(coin, new Vector3(last[2], -6f, 0f), Quaternion.identity);
            lastCoinLaunchTime = Time.time;
            currentCoinInterval = Random.Range(10f, 15f);
        }
    }

    void SetNewWindInterval()
    {
        for (int i = modifiers.Length - 1; i >= 0; i--)
        {
            if (speedModifierController.objectSpeedModifier > modifiers[i])
            {
                minimalWindInterval = minimalWindIntervals[i];
                maximalWindInterval = maximalWindIntervals[i];
                break;
            }
        }
    }

    void CheckWindSpawn()
    {
        if (Time.time - lastWindLaunchTime >= currentWindInterval)
        {
            if (Random.Range(0, 100) < 10 && !secondSpawned)
            {
                lastSpawnPos = Random.Range(0, 2) == 0 ? -0.3f : 0.3f;
                CreateWind(10, lastSpawnPos);
                lastWindLaunchTime = Time.time;
                currentWindInterval = 6.5f;
                secondSpawned = true;
            }
            else
            {
                if (!secondSpawned)
                {
                    lastSpawnPos = Random.Range(0, 2) == 0 ? -0.3f : 0.3f;
                    Debug.Log("changed");
                }
                CreateWind(10, lastSpawnPos);
                lastWindLaunchTime = Time.time;
                SetNewWindInterval();
                currentWindInterval = Random.Range(minimalWindInterval, maximalWindInterval);
                secondSpawned = false;
            }
        }
    }



    void ModifyLast()
    {
        KeyValuePair<float, float>[] places = new KeyValuePair<float, float>[4];
        int amountOfPlaces = 0;

        float[] prevs = new float[3];
        prevs[0] = last[0];
        prevs[1] = last[1];
        prevs[2] = last[2];
        quicksort(prevs, 0, prevs.Length - 1);



        if (prevs[0] - 1f >= -3f) // поиск участков
        {
            places[amountOfPlaces] = new KeyValuePair<float, float>(-3f, prevs[0] - 1f);
            amountOfPlaces++;
        }

        if (prevs[1] - 1f >= prevs[0] + 1f)
        {
            places[amountOfPlaces] = new KeyValuePair<float, float>(prevs[0] + 1f, prevs[1] - 1f);
            amountOfPlaces++;
        }

        if (prevs[2] - 1f >= prevs[1] + 1f)
        {
            places[amountOfPlaces] = new KeyValuePair<float, float>(prevs[1] + 1f, prevs[2] - 1f);
            amountOfPlaces++;
        }

        if (3f >= prevs[2] + 1f)
        {
            places[amountOfPlaces] = new KeyValuePair<float, float>(prevs[2] + 1f, 3f);
            amountOfPlaces++;
        }



        last[0] = last[1];
        last[1] = last[2];
        last[2] = FindPos(amountOfPlaces, places);
    }

    float FindPos(int AOP, KeyValuePair<float, float>[] plc) // ищет участок
    {
        if (AOP == 0)
        {
            return Random.Range(-3f, 3f);
        }

        if (AOP == 1)
        {
            return Random.Range(plc[0].Key, plc[0].Value);
        }

        float[] vars = new float[AOP];
        for (int i = 0; i < AOP; i++)
        {
            vars[i] = Random.Range(plc[i].Key, plc[i].Value);
        }

        return vars[Random.Range(0, AOP)];

    }



    // <QuickSort>
    int partition(float[] array, int start, int end)
    {
        float temp; //swap helper
        int marker = start; //divides left and right subarrays
        for (int i = start; i <= end; i++)
        {
            if (array[i] < array[end]) //array[end] is pivot
            {
                temp = array[marker]; // swap
                array[marker] = array[i];
                array[i] = temp;
                marker += 1;
            }
        }

        temp = array[marker];
        array[marker] = array[end];
        array[end] = temp;
        return marker;
    }

    void quicksort(float[] array, int start, int end)
    {
        if (start >= end)
        {
            return;
        }
        int pivot = partition(array, start, end);
        quicksort(array, start, pivot - 1);
        quicksort(array, pivot + 1, end);
    }
    // </QuickSort>

    void CreateWind(float timer, float speedForhero)
    {
        if (speedForhero > 0)
        {
            var enemy = Object.Instantiate(obstacles[3], new Vector3(-2.6f, -6f, 0f), Quaternion.identity);
            enemy.GetComponent<Wind>().Initialize(timer, speedForhero);
        }
        else
        {
            var enemy = Object.Instantiate(obstacles[3], new Vector3(2.6f, -6f, 0f), Quaternion.identity);
            enemy.GetComponent<Wind>().Initialize(timer, speedForhero);
        }

    }
}














