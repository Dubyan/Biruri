using UnityEngine;
using System.Collections;

public class JumpingObjects : MonoBehaviour
{
    // Объекты, которые будут прыгать
    public GameObject[] jumpObjects;

    // Высота прыжка
    public float jumpHeight = 2f;

    // Скорость прыжка (скорость изменения позиции в единицах времени)
    public float jumpSpeed = 5f;

    // Время задержки между прыжками
    public float delayBetweenJumps = 0.5f; // Изменено значение для быстрого прыжка

    // Время задержки перед первым прыжком
    public float initialDelay = 3f;

    private int currentObjectIndex = 0;

    void Start()
    {
        // Запускаем корутину для прыжков
        StartCoroutine(JumpObjectsCoroutine());
    }

    // Корутина для прыжков
    IEnumerator JumpObjectsCoroutine()
    {
        // Ждем начальную задержку
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // Получаем текущий объект
            GameObject currentObject = jumpObjects[currentObjectIndex];

            // Прыгаем вверх
            yield return StartCoroutine(Jump(currentObject));

            // Увеличиваем индекс, чтобы прыгнуть другим объектом
            currentObjectIndex++;

            // Если все объекты прыгнули, начинаем с начала
            if (currentObjectIndex >= jumpObjects.Length)
            {
                currentObjectIndex = 0;
            }

            // Пауза между циклами
            yield return new WaitForSeconds(delayBetweenJumps); //  Задержка между прыжками
        }
    }

    // Корутина для прыжка отдельного объекта
    IEnumerator Jump(GameObject obj)
    {
        // Сохраняем начальную позицию
        Vector3 startPosition = obj.transform.position;

        // Расчитываем время, необходимое для подъема на заданную высоту
        float jumpTime = jumpHeight / jumpSpeed;

        // Прыгаем вверх
        float elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            // Движемся вверх с постоянной скоростью
            obj.transform.position += new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Прыгаем вниз
        elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            // Движемся вниз с постоянной скоростью
            obj.transform.position -= new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаем объект в начальную позицию
        obj.transform.position = startPosition;
    }
}