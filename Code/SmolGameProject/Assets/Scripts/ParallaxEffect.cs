using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    /**
     * Must be attached to : the gameobject containing a layer of the background.
     * 
     * Description : Moves the layer according to the camera.
    **/

    [SerializeField] private float _parallaxEffect = 0;
    [SerializeField] private GameObject _mainCamera = null;

    // Update is called once per frame
    void Update()
    {
        // translate the background layer
        transform.position = new Vector3(_mainCamera.transform.position.x * _parallaxEffect, transform.position.y, transform.position.z);

        // TODO if the level is extended : reloop the parallax
    }
}
