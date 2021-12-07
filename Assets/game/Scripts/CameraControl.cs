using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region Variables

    //Public
    [Range(0.0f, 100.0f)] public float movementSpeed;//camera movement
    [Range(0.0f, 100.0f)] public float zoomSpeed;//camera zoom
    [Range(0.0f, 95.0f)] public float rotateSpeed;//camera rotation    
    public float top;
    public float bot;
    public float left;
    public float right;
    public Vector3 offset;//target of the camera
    public Vector3 rotateValue;

    //Private
    private GameManager _gameManager;
    private Transform _cameraTransform;
    private Vector3 _cameraOriginalPos;
    private Vector3 _forward;
    private Vector3 _target;
    private Camera _camera;
    private float zoomTo;
    private float pitch;
    private float yaw;
    private float x;
    private float y;

    #endregion

    #region Methods

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
        _cameraOriginalPos = transform.position;
        _target = _cameraOriginalPos - offset;
        _cameraTransform.position = _target + offset;
    }

    private void Update()
    {
        LimitCamera(_gameManager.fieldManager);
        if (SelectionManager.SelectedPlayer != null)
        {
            CenterCamera(SelectionManager.SelectedPlayer.transform);
        }
        else if (SelectionManager.SelectedEnemy != null)
        {
            CenterCamera(SelectionManager.SelectedEnemy.transform);
        }

        Zoom();
        _cameraTransform.position = _target + offset;
        DebugCamera();
    }

    private void LimitCamera(FieldManager lastHexagon)
    {
        Vector3 cameraCenter = _camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, _camera.nearClipPlane + 22));
        _forward = Vector3.Cross(_camera.transform.right, Vector3.up);

        if (Input.mousePosition.y >= Screen.height * top)
        {
            if (cameraCenter.z <= lastHexagon.hexagons.Values.Last().worldPos.z)
            {
                _target += _forward * (Time.deltaTime * movementSpeed);
            }
        }
        else if (Input.mousePosition.y <= Screen.height * bot)
        {
            if (cameraCenter.z >= lastHexagon.hexagons.Values.First().worldPos.z + 12)
            {
                _target += -_forward * (Time.deltaTime * movementSpeed);
            }
        }
        if (Input.mousePosition.x >= Screen.width * right)
        {

            if (cameraCenter.x <= lastHexagon.hexagons.Values.Last().worldPos.x)
            {
                _target += Vector3.right * (Time.deltaTime * movementSpeed);
            }
        }
        if (Input.mousePosition.x <= Screen.width * left)
        {
            if (cameraCenter.x >= lastHexagon.hexagons.Values.First().worldPos.x)
            {
                _target += Vector3.left * (Time.deltaTime * movementSpeed);
            }
        }
    }

    private void Zoom()
    {
        // Attaches the float y to scrollWheel up or down
        y = Input.mouseScrollDelta.y;
        if (y == 0)
        {
            zoomTo = 0;
            return;
        }

        Vector3 tempOffset = offset;
        if (y > 0)
        {
            zoomTo += zoomSpeed;
            tempOffset += _cameraTransform.forward * (Time.deltaTime * zoomTo);
        }
        if (y < 0)
        {
            zoomTo -= zoomSpeed;
            tempOffset += _cameraTransform.forward * (Time.deltaTime * zoomTo);
        }

        Vector3 tempPos = _cameraTransform.position + tempOffset;
        if (tempPos.y >= 10 && tempPos.y < 30)
        {
            offset = tempOffset;
            _cameraTransform.position = tempPos;
        }
    }

    private void DebugCamera()
    {
        if (_cameraTransform.position.y < 5)
        {
            _cameraTransform.position = new Vector3(_cameraTransform.position.x, 5, _cameraTransform.position.z);
            offset.y = 3.5f;
        }
        if (_cameraTransform.position.y > 15)
        {
            _cameraTransform.position = new Vector3(_cameraTransform.position.x, 15, _cameraTransform.position.z);
            offset.y = 15f;
        }  
    }
    
    public void CenterCamera(Transform target)
    {
        //ResetRotation();
        Vector3 tempvector = new Vector3(target.position.x, _cameraTransform.position.y - offset.y, target.position.z - 2);
        _target = tempvector;
    }

    public void RightRotation()
    {
        pitch += rotateSpeed;
        _cameraTransform.eulerAngles = new Vector3(_camera.transform.eulerAngles.x, pitch, _cameraTransform.eulerAngles.z);
    }

    public void LeftRotation()
    {
        pitch -= rotateSpeed;
        _cameraTransform.eulerAngles = new Vector3(_camera.transform.eulerAngles.x, pitch, _cameraTransform.eulerAngles.z);
    }

    public void ResetRotation()
    {
        _cameraTransform.eulerAngles = new Vector3(_camera.transform.eulerAngles.x, 0.0f, _cameraTransform.eulerAngles.z);
        pitch = 0.0f;
    }

    #endregion

}
