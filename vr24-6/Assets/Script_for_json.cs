using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Script_for_json : MonoBehaviour
{
    public Text x;
    public Text z;
    public string jsonURL;
    public Jsonclass jsnData;

    void Start()
    {
        StartCoroutine(getData());
        float targetX = jsnData.X;
        float currentY = transform.position.y;
        float targetZ = jsnData.Z;
        Vector3 targetPosition = new Vector3(targetX, currentY, targetZ);
        transform.position = targetPosition;
    }

    IEnumerator getData()
    {
        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        Debug.Log("Файл сохранён по пути:" + resultFile);
        jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
        x.text = jsnData.X.ToString();
        z.text = jsnData.Z.ToString();
    }
    [System.Serializable]

    public class Jsonclass
    {
        public int X;
        public int Z;
    }
}