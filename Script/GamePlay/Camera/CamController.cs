
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerVisualsTransform;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
       Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
       _orientationTransform.forward = viewDirection.normalized;

       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       Vector3 playerInput = _orientationTransform.right * horizontalInput + _orientationTransform.forward * verticalInput;
       if (playerInput != Vector3.zero)
       {
           _playerVisualsTransform.forward = Vector3.Slerp(_playerVisualsTransform.forward, playerInput.normalized, Time.deltaTime *_rotationSpeed); //slerp= rotasyonlar için anismasyonlu gibi bir geçiş
       }
       
       
    }

}
