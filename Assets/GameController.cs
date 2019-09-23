using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;

    public GameObject plane;

    public GameObject[] pickups;

    const int NumObj = 6;
    private int count = 0;
    [System.Obsolete]
    public GUIText scoreText;
    [System.Obsolete]
    public GUIText winText;
    [System.Obsolete]
    public GUIText speedText;

    private LineRenderer line = null;

    private int closetObject;

    public bool IsPlaying = false;

    int GetClosetObject() {
        int idx = -1;
        float min = -1;
        for (int i = 0; i < pickups.Length; i++)
            if(pickups[i].activeSelf)
        {
            float temp = Vector3.Distance(player.transform.position, pickups[i].transform.position);
            if (min == -1 || min > temp)
            {
                idx = i;
                min = temp;
            }

        }
        return idx;
    }
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        winText.text = "";
        speedText.text = "0.0 m/s";
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.SetWidth(0.1f, 0.1f); 
        }
        
    }

    [System.Obsolete]
    void StartNewGame() {
        Start();
        IsPlaying = true;
        count = 0;
        ShowScore();
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.GetRigidbody().velocity = Vector3.zero;
        player.GetRigidbody().angularVelocity = Vector3.zero;

        var mesgInfo = plane.GetComponent<MeshFilter>().mesh;
        var bounds = mesgInfo.bounds;
        for (int i = 0; i < pickups.Length; i++)
        {
            pickups[i].SetActive(true);
            _ = Random.Range(plane.transform.position.x - plane.transform.localScale.x * bounds.size.x / 2,
                plane.transform.position.x + plane.transform.localScale.x * bounds.size.x / 2);
            _ = Random.Range(plane.transform.position.z - plane.transform.localScale.z * bounds.size.z / 2,
                plane.transform.position.z + plane.transform.localScale.z * bounds.size.z / 2);
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        speedText.text = player.GetRigidbody().velocity.magnitude.ToString("0.00") + "m/s";
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, player.transform.position + player.GetRigidbody().velocity);
        int newidx = GetClosetObject();
        if (closetObject >= 0 && closetObject != newidx)
        {
            pickups[closetObject].GetComponent<Renderer>().material.color = Color.white;
        }
        if(newidx >= 0 && closetObject != newidx)
        {
            pickups[newidx].GetComponent<Renderer>().material.color = Color.red;
            closetObject = newidx;
        }
    }

    [System.Obsolete]
    void OnGUI()
    {
        if (!IsPlaying) {
            GUIStyle style = GUI.skin.GetStyle("Button");
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "NEW GAME", style))
            {
                StartNewGame();
            }
        }
    }

    [System.Obsolete]
    void ShowScore()
    {
        scoreText.text = "Score: " + count;
        if (count >= NumObj)
        {
            winText.text = "You win!";
            IsPlaying = false;
        }
    }

    //Ham tang diem so
    [System.Obsolete]
    public void Increase() {
        count++;
        ShowScore();
    } 
}
