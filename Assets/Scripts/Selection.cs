using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    //UI Buttons
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    //Camera Section
    [Header("Camera Settings")]
    [Tooltip("Take a main camera")]
    [SerializeField]
    private Camera mainCamera = null;
    [Tooltip("Current view")]
    [SerializeField]
    private Transform currentView = null;
    [Tooltip("Array of camera view")]
    [SerializeField]
    private Transform[] cameraView;

    [Tooltip("Transition speed of the main camera")]
    [SerializeField] float transitionSpeed = 1.0f;
    
    private int currentCharacterIndex;

    private void Awake()
    {
        SelectCharacter(0);
        //currentView = mainCamera.transform;
        currentView = cameraView[0];
        //cameraView[0] = currentView;   
    }

    private void LateUpdate()
    {
        ChangeCameraAngle();
    }

    private void SelectCharacter(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);
        /*for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
            //stand animation
        }*/
    }
    public void ChangeCharacter(int _change)
    {
        currentCharacterIndex += _change;
        //ChangeCameraAngle();
        currentView = cameraView[currentCharacterIndex];
        SelectCharacter(currentCharacterIndex);
    }
    private void ChangeCameraAngle()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
        mainCamera.transform.eulerAngles = currentAngle;
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
