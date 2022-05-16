using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    private static Text warningText;
    [SerializeField] private float warningDuration;
    [SerializeField] private float fadeSpeed;
    private float startTime;
    private static bool newWarning;
    private void Awake()
    {
        warningText = GameObject.Find("WarningText").GetComponent<Text>();
        warningText.color = new Color(warningText.color.r, warningText.color.g, warningText.color.b, 0);
    }

    //Displays the warning message
    public static void Display(string warningMessage)
    {
        warningText.text = warningMessage;
        warningText.color = new Color(warningText.color.r, warningText.color.g, warningText.color.b, 1);
        newWarning = true;
    }

    //sets a timer every time a warning message has been sent, fading the message away when it ends
    private void Update()
    {
        if (newWarning)
        {
            startTime = Time.time;
            newWarning = false;
        }
        if (Time.time - startTime >= warningDuration)
        {
            warningText.color = Color.Lerp(warningText.color, new Color(warningText.color.r, warningText.color.g, warningText.color.b, 0), Time.deltaTime * fadeSpeed);
        }
    }
}
