using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killdeText = null;
    [SerializeField] GameObject win = null;

    int enemysOnMap;
    int enemysKilled;

    private void Start()
    {
        KilldeText();
        win.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        UnityEngine.Cursor.visible = false;                   // Hide the cursor
    }

    public void modifeEnemysOnMap()
    {
        enemysOnMap++;
        KilldeText();
    }

    public void modifeEnemysKilled()
    {
        enemysKilled++;
        KilldeText();

        if(enemysKilled >= enemysOnMap)
        {
            win.SetActive(true);

            UnityEngine.Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            UnityEngine.Cursor.visible = true;                  // Make the cursor visible
        }
    }

    void KilldeText()
    {
        killdeText.text = enemysKilled.ToString() + "/" + enemysOnMap.ToString() + "Killde";
    }
}
