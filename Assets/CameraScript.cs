using UnityEngine;

public class CameraScript : MonoBehaviour
{
        private Transform swivel, stick;
        public float stickMinZoom = -250f, stickMaxZoom = -45f;

        public float swivelMinZoom, swivelMaxZoom;

        private float zoom = 1f;

        public float moveSpeedMinZoom = 100f, moveSpeedMaxZoom = 400f;

        public float rotationSpeed = 180f;

        private float rotationAngle;
        public Transform mapLimit;

        private void Awake()
        {
                swivel = transform.GetChild(0);
                stick = swivel.GetChild(0);
        }

        private void Update()
        {
                float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
                if (zoomDelta != 0f)
                {
                        AdjustZoom(zoomDelta);
                }

                float xDelta = Input.GetAxis("Horizontal");
                float zDelta = Input.GetAxis("Vertical");
                if (xDelta != 0f || zDelta != 0f)
                {
                        AdjustPosition(xDelta, zDelta);
                }

                float rotationDelta = Input.GetAxis("Rotation");
                if (rotationDelta != 0f)
                {
                        AdjustRotation(rotationDelta);
                }
        }

        private void AdjustRotation(float delta)
        {
                rotationAngle += delta * rotationSpeed * Time.deltaTime;
                if (rotationAngle < 0f)
                {
                        rotationAngle += 360f;
                }
                else if (rotationAngle >= 360f)
                {
                        rotationAngle -= 360f;
                }
                transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }

        private void AdjustPosition(float xDelta, float zDelta)
        {
                Vector3 direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
                //float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
                float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * Time.deltaTime;

                Vector3 position = transform.localPosition;
                position += direction * distance;
                transform.localPosition = ClampPosition(position, mapLimit) * Time.deltaTime;
        }

        private void AdjustZoom(float delta)
        {
                zoom = Mathf.Clamp01(zoom + delta);

                float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
                stick.localPosition = new Vector3(0f, 0f, distance);

                float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
                swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }

        private Vector3 ClampPosition(Vector3 position, Transform transformHolder)
        {
                Vector3 TopLeft = transformHolder.Find("TopLeft").position;
                Vector3 BotRight = transformHolder.Find("BotRight").position;
                float X = Random.Range(TopLeft.x, BotRight.x);
                float Z = Random.Range(TopLeft.z, BotRight.z);

                position.x = X;
                position.z = Z;

                return position;
        }
}