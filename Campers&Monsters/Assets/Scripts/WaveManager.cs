using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<WaveObject> waves = new List<WaveObject>();
    public bool isWaitingForNextWave;
    public bool waveFinish;
    public int currentWave;
    public Transform initPosition;

    public TextMeshProUGUI counterText;
    public GameObject buttonNextWave;

    private void Start()
    {
        StartCoroutine(ProcesWave());
    }
    void Update()
    {
        CheckCounterAnsShowButton();
        CheckCounterForNextWave();
    }

    private void CheckCounterForNextWave()
    {
        if (isWaitingForNextWave && !waveFinish)
        {
            waves[currentWave].counterToNextWave -= 1 * Time.deltaTime;
            counterText.text = waves[currentWave].counterToNextWave.ToString("00");
            if (waves[currentWave].counterToNextWave <= 0)
            {
                Debug.Log("Next Wave");
                ChageWave();
            }
        }
    }

    public void ChageWave()
    {
        if (waveFinish)
            return;

        currentWave++;
        StartCoroutine(ProcesWave());
    }

    private IEnumerator ProcesWave()
    {
        if (waveFinish)
            yield break;

        isWaitingForNextWave = false;
        waves[currentWave].counterToNextWave = waves[currentWave].timeForNextWave;

        // Recorremos la lista de enemigos he instaciamos cada uno.
        for (int i = 0; i < waves[currentWave].enemys.Count; i++)
        {
            var enemyGo = Instantiate(waves[currentWave].enemys[i], initPosition.position, initPosition.rotation);
            yield return new WaitForSeconds(waves[currentWave].timePerCreation);
        }

        isWaitingForNextWave = true;
        
        // 
        if (currentWave >= waves.Count - 1)
        {
            Debug.Log("Nivel Terminado");
            waveFinish = true;
        }
    }

    private void CheckCounterAnsShowButton()
    {
        if (!waveFinish)
        {
            buttonNextWave.SetActive(isWaitingForNextWave);
            counterText.gameObject.SetActive(isWaitingForNextWave);
        }
    }

    [System.Serializable]
    public class WaveObject
    {
        public float timePerCreation = 1F;
        public float timeForNextWave = 10F;
        [HideInInspector] public float counterToNextWave = 0F;
        public List<GameObject> enemys = new List<GameObject>();
    }
}
