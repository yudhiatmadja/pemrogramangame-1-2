using UnityEngine;

public class HangTrigger : MonoBehaviour
{
    public GameObject panelText;

    private void Start() {
        panelText.SetActive(false);
    } 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            panelText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            panelText.SetActive(false);
        }
    }
}
