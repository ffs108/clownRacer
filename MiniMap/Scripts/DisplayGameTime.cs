using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameTime : MonoBehaviour
{
    public Canvas canvas;
    public Font font;

    private GameObject textObj;
    private float gameTime;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        textObj = new GameObject();
        textObj.transform.parent = canvas.transform;
        textObj.name = "time text";
        text = textObj.AddComponent<Text>();     
        text.text = gameTime.ToString("F0");
        text.font = font;
        text.fontSize = 40;
        text.color = new Color(255f, 255f, 255f);
        text.alignment = TextAnchor.MiddleCenter;

        // Text position
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 290, 0);
        rectTransform.sizeDelta = new Vector2(200, 45);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        text.text = gameTime.ToString("F0");
    }
}
