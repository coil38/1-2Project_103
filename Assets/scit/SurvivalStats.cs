using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100;           //최대 허기량
    public float currentHunger;             //현재 허기량
    public float hungerDecreaseRate = 1;    //초당 허기 감소량

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;       //최대 우주복 내구도
    public float currentSuitDurability;         //현재 우주복 내구도
    public float havestingDamage = 5.0f;        //수집시 우주복 데미지
    public float craftingDamage = 3.0f;         //제작시 우주복 데미지

    private bool isGameOver = false;            //게임 오버 상태
    private bool isPaused = false;              //일시 정지 상태
    private float hungerTimer = 0;              //허기 감소 타이머


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void PlayerDeath()
    {
        isGameOver = true;
        Debug.Log("플레이어 사망!");
        //TODO : 사망 처리 추가 (게임오버 UI,리스폰 등등)
    }

    public float GetHungerPercentage()                  //허기짐 % 리턴 함수
    {
        return (currentHunger / maxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()          //슈트 % 리턴 함수
    {
        return (currentSuitDurability / maxSuitDurability) * 100;
    }

    public bool IsGameOver()                            //게임 종료 확인 함수
    {
        return isGameOver;
    }

    public void ResetStats()
    {
        isGameOver = false;
        isPaused = false;
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }

    public void EatFood(float amount)
    {
        if (isGameOver || isPaused) return;

        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"허기 회복 수리 + {amount}", transform.position + Vector3.up);
        }
    }

    //우주복 수리 (크리스탈로 제작한 수리 키트 사용)
    


}
