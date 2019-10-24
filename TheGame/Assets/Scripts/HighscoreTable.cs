using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEnrtyTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighscoreEntry(10000, "CMK");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //sort list by score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }

        //PlayerPrefs.DeleteAll();

    }

    private void Update()
    {
        //AddHighscoreEntry(10000, "CMK");
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;

        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        //create highscore
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
