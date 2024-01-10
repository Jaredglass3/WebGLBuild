using UnityEngine;
using UnityEngine.UI;

public class LinkedInButton : MonoBehaviour
{
    public void FollowLink(string link)
    {
     
        // Handle LinkedIn button click action here
        // For example, open a LinkedIn URL
        Application.OpenURL(link);
    }
}
