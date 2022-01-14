using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    // from System
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private Text prizeText;

    // slot rows
    [SerializeField]
    private Row[] rows;
    
    [SerializeField]
    private Transform handle;

    private int prizeValue;

    // check the result when rows stop spinning
    private bool resultsChecked = false;

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped){
            // 계속 돌고있음
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked){
            // 도는게 멈췄고 결과 확인 안됐다면
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: " + prizeValue;
        }
    }

    /*
    OnMouseDown: Reserved Event
    마우스 왼쪽 버튼 클릭시 triggered
    모바일 기기에서는 하나의 터치만 인식
    복잡한 터치를 지원하려면 MobileTouchEventOption을 쓰는게 낫다
    */
    void OnMouseDown(){
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped){
            // row 다 멈춤
            /*
            코루틴은 프레임별로 변화를주고싶을때 사용
            코루틴은 IEnumerator로 선언
            불러올때는 StartCoroutine("코루틴 이름")
            */
            StartCoroutine("PullHandle");
        }
    }

    private IEnumerator PullHandle(){
        for (int i = 0; i < 30; i += 10)
        {
            handle.Rotate(0f, 0f, i);
            // 30도 까지 기울어짐

            /*
            yield return: 코루틴을 잠시 중지하고 다음 프레임에서 다시 실행하려면
            WaitForSeconds: 다음 프레임에서 바로 시작하지 않고 딜레이를 주려면 사용
            */
            yield return new WaitForSeconds(0.1f);
        }
        // trigger event
        HandlePulled();
        for (int i = 0; i < 30; i += 10)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void CheckResults(){
        // 결과 및 상금 확인
        if(rows[0].stoppedSlot == "Diamond" &&
        rows[1].stoppedSlot == "Diamond" &&
        rows[2].stoppedSlot == "Diamond"){
            //  전부 Diamond
            prizeValue = 200;
        }
        else if(rows[0].stoppedSlot == "Crown" &&
        rows[1].stoppedSlot == "Crown" &&
        rows[2].stoppedSlot == "Crown"){
            //  전부 Crown
            prizeValue = 400;
        }
        else if(rows[0].stoppedSlot == "Melon" &&
        rows[1].stoppedSlot == "Melon" &&
        rows[2].stoppedSlot == "Melon"){
            //  전부 Melon
            prizeValue = 600;
        }
        else if(rows[0].stoppedSlot == "Bar" &&
        rows[1].stoppedSlot == "Bar" &&
        rows[2].stoppedSlot == "Bar"){
            //  전부 bar
            prizeValue = 800;
        }
        else if(rows[0].stoppedSlot == "Seven" &&
        rows[1].stoppedSlot == "Seven" &&
        rows[2].stoppedSlot == "Seven"){
            //  전부 Seven
            prizeValue = 1500;
        } 
        else if(rows[0].stoppedSlot == "Cherry" &&
        rows[1].stoppedSlot == "Cherry" &&
        rows[2].stoppedSlot == "Cherry"){
            //  전부 Cherry 
            prizeValue = 3000;
        } 
        else if(rows[0].stoppedSlot == "Lemon" &&
        rows[1].stoppedSlot == "Lemon" &&
        rows[2].stoppedSlot == "Lemon"){
            //  전부 Lemon 
            prizeValue = 5000;
        } 
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Diamond")) || 
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Diamond")) ||
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Diamond"))){
            prizeValue = 100;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Crown")) || 
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Crown")) || 
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Crown"))){
            prizeValue = 300;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Melon")) || 
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Melon")) || 
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Melon")))
        {
            prizeValue = 500;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Bar")) ||
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Bar")) ||
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Bar"))){
            prizeValue = 700;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Seven")) ||
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Seven")) ||
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Seven"))){
            prizeValue = 1000;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Cherry")) ||
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Cherry")) ||
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Cherry"))){
            prizeValue = 2000;
        }
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Lemon")) ||
        ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Lemon")) ||
        ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Lemon"))){
            prizeValue = 4000;
        }
    }
}
