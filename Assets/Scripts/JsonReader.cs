using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonReader : MonoBehaviour
{

    private static JsonReader _instance;

    public static JsonReader Instance { get { return _instance; } }

    public Item[] items;
    public GameObject contextMenu;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    public void ReadFromWebPage(string url)
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
       
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Request and wait for the desired page
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        // If we cannot connect to the webpage, it will throw an error
        // Otherwise we extract the data contained on the webpage
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                ReadJson(webRequest.downloadHandler.text);             
                break;
        }
    }

    void ReadJson(string value)
    {
        // Clear array elements and repopulate with
        // new elements everytime an npc is clicked
        // and extract the data from the webpage
        Array.Clear(items, 0, items.Length);
        value = "{\"Items\":" + value + "}";
        items = JsonHelper.FromJson<Item>(value);
        for (int i = 0; i < contextMenu.transform.childCount; i++)
        {
            contextMenu.SetActive(true);
            contextMenu.transform.GetChild(i).GetComponent<Text>().text = items[i].name + ":$" + items[i].price.Replace("0", "");
        }
    }
}

