using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BugReport : MonoBehaviour
{
    public string developerEmail = "andreisakalosh@gmail.com";

    public void OpenPlayerEmail()
    {
        string subject = "Message_from_player";
        string body = "Hello_developers";
        //body += "Here, write what you think about the game or about bugs what you found while playing in it.%0A%0A";
        //body += "Best regards,%0A";
        //body += "Your Name";

        string uri = "mailto:" + developerEmail + "?subject=" + UnityWebRequest.EscapeURL(subject) + "&body=" + UnityWebRequest.EscapeURL(body);
        Application.OpenURL(uri);
    }
}
