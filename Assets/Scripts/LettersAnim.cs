using UnityEngine;
using System.Collections;

public class JumpingObjects : MonoBehaviour
{
    // �������, ������� ����� �������
    public GameObject[] jumpObjects;

    // ������ ������
    public float jumpHeight = 2f;

    // �������� ������ (�������� ��������� ������� � �������� �������)
    public float jumpSpeed = 5f;

    // ����� �������� ����� ��������
    public float delayBetweenJumps = 0.5f; // �������� �������� ��� �������� ������

    // ����� �������� ����� ������ �������
    public float initialDelay = 3f;

    private int currentObjectIndex = 0;

    void Start()
    {
        // ��������� �������� ��� �������
        StartCoroutine(JumpObjectsCoroutine());
    }

    // �������� ��� �������
    IEnumerator JumpObjectsCoroutine()
    {
        // ���� ��������� ��������
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // �������� ������� ������
            GameObject currentObject = jumpObjects[currentObjectIndex];

            // ������� �����
            yield return StartCoroutine(Jump(currentObject));

            // ����������� ������, ����� �������� ������ ��������
            currentObjectIndex++;

            // ���� ��� ������� ��������, �������� � ������
            if (currentObjectIndex >= jumpObjects.Length)
            {
                currentObjectIndex = 0;
            }

            // ����� ����� �������
            yield return new WaitForSeconds(delayBetweenJumps); //  �������� ����� ��������
        }
    }

    // �������� ��� ������ ���������� �������
    IEnumerator Jump(GameObject obj)
    {
        // ��������� ��������� �������
        Vector3 startPosition = obj.transform.position;

        // ����������� �����, ����������� ��� ������� �� �������� ������
        float jumpTime = jumpHeight / jumpSpeed;

        // ������� �����
        float elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            // �������� ����� � ���������� ���������
            obj.transform.position += new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������� ����
        elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            // �������� ���� � ���������� ���������
            obj.transform.position -= new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ������ � ��������� �������
        obj.transform.position = startPosition;
    }
}