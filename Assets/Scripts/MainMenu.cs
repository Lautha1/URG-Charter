using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField directory;
    public InputField songTitle;
    public InputField songFile;
    public InputField difficulty;

    public AudioSource audioSource;

    public void Quit()
    {
        Application.Quit();
    }

    public void CreateSongFiles()
    {
        // First create the new directory to work in
        try
        {
            directory.text = directory.text.Trim();

            if (directory.text.EndsWith("\\"))
            {
                PlayerPrefs.SetString("MainDir", directory.text + songTitle.text);
            } 
            else
            {
                PlayerPrefs.SetString("MainDir", directory.text + "\\" + songTitle.text);
            }

            // Create the new main dir
            Directory.CreateDirectory(PlayerPrefs.GetString("MainDir"));

            // Create the charts dir
            Directory.CreateDirectory(PlayerPrefs.GetString("MainDir") + "\\charts");
        } 
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }

        // Save Path of Song File To Use
        PlayerPrefs.SetString("SongDir", songFile.text);

        // Set difficulty and create folder
        PlayerPrefs.SetInt("Diff", int.Parse(difficulty.text));
        PlayerPrefs.SetString("WorkingDir", PlayerPrefs.GetString("MainDir") + "\\charts\\" + difficulty.text);
        Directory.CreateDirectory(PlayerPrefs.GetString("WorkingDir"));

        // Create the lane files
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\L1.txt");
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\L2.txt");
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\L3.txt");
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\L4.txt");
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\L5.txt");
        File.Create(PlayerPrefs.GetString("WorkingDir") + "\\Wheel.txt");

        // Load in the audio clip to file
        StartCoroutine(loadSong(PlayerPrefs.GetString("SongDir")));

        //TODO: Load the charter portion
    }

    public void browseForSongDir()
    {
        //TODO: REplace this line as EditorUtility doesn't work after build
        string path = EditorUtility.OpenFolderPanel("Select Song Directory", "", "");
        directory.text = path;
    }

    public void browseForSongFile()
    {
        //TODO: REplace this line as EditorUtility doesn't work after build
        string path = EditorUtility.OpenFilePanel("Select Song File", "", "wav");
        songFile.text = path;
    }

    public IEnumerator loadSong(string path)
    {
        WWW www = new WWW(("file://" + path));
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            audioSource.clip = www.GetAudioClip();
            while (audioSource.clip.loadState != AudioDataLoadState.Loaded)
            {
                Debug.Log("Loading");
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
