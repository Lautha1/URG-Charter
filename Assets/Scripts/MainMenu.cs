using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField directory;
    public InputField songTitle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CreateSongFiles()
    {
        try
        {
            directory.text = directory.text.Trim();

            if (directory.text.EndsWith("\\"))
            {
                Debug.Log(directory.text + songTitle.text);
                PlayerPrefs.SetString("SongDir", directory.text + songTitle.text);
            } 
            else
            {
                Debug.Log(directory.text + "\\" + songTitle.text);
                PlayerPrefs.SetString("SongDir", directory.text + "\\" + songTitle.text);
            }

            // Create the new working dir
            Directory.CreateDirectory(PlayerPrefs.GetString("SongDir"));
        } 
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
