using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100;           //�ִ� ��ⷮ
    public float currentHunger;             //���� ��ⷮ
    public float hungerDecreaseRate = 1;    //�ʴ� ��� ���ҷ�

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;       //�ִ� ���ֺ� ������
    public float currentSuitDurability;         //���� ���ֺ� ������
    public float havestingDamage = 5.0f;        //������ ���ֺ� ������
    public float craftingDamage = 3.0f;         //���۽� ���ֺ� ������

    private bool isGameOver = false;            //���� ���� ����
    private bool isPaused = false;              //�Ͻ� ���� ����
    private float hungerTimer = 0;              //��� ���� Ÿ�̸�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void PlayerDeath()                          //�÷��̾� ��� �Լ�
    {
        isGameOver = true;
        Debug.Log("�÷��̾� ���!");
        //TODO : ��� ó�� �߰� (���ӿ��� UI,������ ���)
    }

    public float GetHungerPercentage()                  //����� % ���� �Լ�
    {
        return (currentHunger / maxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()          //��Ʈ % ���� �Լ�
    {
        return (currentSuitDurability / maxSuitDurability) * 100;
    }

    public bool IsGameOver()                            //���� ���� Ȯ�� �Լ�
    {
        return isGameOver;
    }

    public void ResetStats()                    //���� �Լ� �ۼ� (������ �ʱ�ȭ �뵵)
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
            FloatingTextManager.Instance.Show($"��� ȸ�� ���� + {amount}", transform.position + Vector3.up);
        }
    }

    //���ֺ� ���� (ũ����Ż�� ������ ���� ŰƮ ���)
    public void RepairSuit(float amount)
    {
        if (!isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability + amount);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"���ֺ� ���� + {amount}", transform.position + Vector3.up);
        }
        /*
        //������ ������ ���ֺ� ������
        public void DamageOnHarvesting()
        {
            if(isGameOver || isPaused) return;

            currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);  //0�� ���Ϸ� �� �������� ���� ���ؼ�
            CheckDeath();
        }

        //������ ���۽� ���ֺ� ������
        public void DamageOnCrafting();
        {
            if (isGameOver || isPaused) return;

            currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);  //0�� ���Ϸ� �� �������� ���� ���ؼ�
            CheckDeath();
        }*/



}


}
