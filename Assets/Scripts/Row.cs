using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;    
    }
    private void StartRotating(){
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }
    private IEnumerator Rotate(){
        rowStopped = false;
        timeInterval = 0.05f;

        for(int i=0;i<30;i++){
            // repeat for 30 frames
            if (transform.position.y <= -4f)
            {
                transform.position = new Vector2(transform.position.x, 1.25f);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        switch (randomValue % 3){
            case 1:
            randomValue +=2;
            break;
            case 2:
            randomValue +=1;
            break;
        }
        for(int i = 0; i<randomValue;i++){
            if (transform.position.y <= -4f)
            {
                transform.position = new Vector2(transform.position.x, 1.25f);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            if(i > Mathf.RoundToInt(randomValue * 0.25f)){
                timeInterval = 0.05f;
            }
            if(i > Mathf.RoundToInt(randomValue * 0.5f)){
                timeInterval = 0.1f;
            }
            if(i > Mathf.RoundToInt(randomValue * 0.75f)){
                timeInterval = 0.15f;
            }
            if(i > Mathf.RoundToInt(randomValue * 0.95f)){
                timeInterval = 0.2f;
            }

            yield return new WaitForSeconds(timeInterval);
        }
        if(transform.position.y == -4f){
            stoppedSlot = "Diamond";
        } else if(transform.position.y == -3.25f){
            stoppedSlot = "Crown";
        } else if(transform.position.y == -2.5f){
            stoppedSlot = "Melon";
        } else if(transform.position.y == -1.75f){
            stoppedSlot = "Bar";
        } else if(transform.position.y == -1f){
            stoppedSlot = "Seven";
        } else if(transform.position.y == -0.25f){
            stoppedSlot = "Cherry";
        } else if(transform.position.y == .5f){
            stoppedSlot = "Lemon";
        } else if(transform.position.y == 1.25f){
            stoppedSlot = "Diamond";
        }
        rowStopped = true;
    }
    private void OnDestroy(){
        GameControl.HandlePulled -= StartRotating;
    }
}