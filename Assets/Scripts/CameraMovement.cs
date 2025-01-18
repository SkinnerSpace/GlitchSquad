using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveDelay;

    public Vector2 xClamp;

    public Vector2 yClamp;

    public float smoothEdgeThreshold;

    public float movementSpeed = 5f;

    public Transform cameraTransform;

    public Transform target;

    private void LateUpdate()
    {
        moveDelay -= Time.deltaTime;
        if (moveDelay > 0)
        {
            return;
        }
        // Текущее положение камеры
        Vector2 currentPos = cameraTransform.localPosition;

        // Положение цели
        Vector2 targetPos = target.localPosition;

        // Ограничиваем целевую позицию в пределах границ
        targetPos.x = Mathf.Clamp(targetPos.x, xClamp.x, xClamp.y);
        targetPos.y = Mathf.Clamp(targetPos.y, yClamp.x, yClamp.y);

        // Расстояние до края для расчёта сглаживания
        float xDistanceToEdge = Mathf.Min(Mathf.Abs(currentPos.x - xClamp.x), Mathf.Abs(currentPos.x - xClamp.y));
        float yDistanceToEdge = Mathf.Min(Mathf.Abs(currentPos.y - yClamp.x), Mathf.Abs(currentPos.y - yClamp.y));

        // Факторы замедления скорости у краёв
        float xEdgeFactor = Mathf.Clamp01(xDistanceToEdge / smoothEdgeThreshold);
        float yEdgeFactor = Mathf.Clamp01(yDistanceToEdge / smoothEdgeThreshold);

        // Применяем сглаживание скорости
        float effectiveSpeedX = movementSpeed * xEdgeFactor;
        float effectiveSpeedY = movementSpeed * yEdgeFactor;

        // Интерполяция для плавного движения
        float newX = Mathf.Lerp(currentPos.x, targetPos.x, Time.deltaTime * effectiveSpeedX);
        float newY = Mathf.Lerp(currentPos.y, targetPos.y, Time.deltaTime * effectiveSpeedY);

        // Обновляем позицию камеры
        cameraTransform.localPosition = new Vector3(newX, newY, -10);
    }
}
