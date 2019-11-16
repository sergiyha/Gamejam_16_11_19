using UnityEngine;

namespace UI
{
    [ExecuteInEditMode]
    public class UiRotator : MonoBehaviour
    {
        private Transform mainCam;

        // Start is called before the first frame update
        void Start()
        {
            mainCam = Camera.main.transform;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.LookAt(mainCam);
        }
    }
}
