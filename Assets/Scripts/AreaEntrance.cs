using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    private CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
        }
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null && PlayerController.Instance != null)
        {
            virtualCamera.Follow = PlayerController.Instance.transform;
        }
    }
}
