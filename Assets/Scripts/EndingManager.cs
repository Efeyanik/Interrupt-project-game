using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManeger : MonoBehaviour
{


    public GameObject EndingUptPanel;
    public TextMeshProUGUI currentTimeText; 
    public TextMeshProUGUI highScoreText;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


      public void endingScreen()
    {
        EndingUptPanel.SetActive(true);
        Time.timeScale = 0;
        CalculateScores();



    }

    



    void CalculateScores()
    {
        // O anki hayatta kalma süresi
        float survivalTime = Time.timeSinceLevelLoad;

        
        // Daha önce kaydedilmiþ rekoru çek (Yoksa 0 kabul et)
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0);

        // Eðer yeni süre, eski rekordan büyükse?
        if (survivalTime > bestTime)
        {
            bestTime = survivalTime; // Yeni rekor bu olsun
            PlayerPrefs.SetFloat("BestTime", bestTime); // Hafýzaya kaydet
            PlayerPrefs.Save(); // Garantilemek için kaydet
        }

        // ekrana yazdýrma
        currentTimeText.text = "Süre: " + FormatTime(survivalTime);
        highScoreText.text = "Rekor: " + FormatTime(bestTime);
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }





    public void startAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
