using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using UnityEngine.EventSystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;
    ARSessionOrigin m_SessionOrigin;
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    public GameObject place_scene;

    //public GameObject ARCamera;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        //place_scene.SetActive(false);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            //Instead of instantiating a prefab on the touch position, place the scene or object in the Unity editor project hierarchy and disable it
            //Enable the scene when it detects a touch input on the tracked plane (ground plane) and move it to the touch position in the real world
            if (!place_scene.activeSelf)
            {
                place_scene.SetActive(true);
                place_scene.transform.position = hitPose.position;

                //This is to make sure while pinching to zoom the position of the scene doesn't change and remains at the touch position itself
                m_SessionOrigin.MakeContentAppearAt(place_scene.transform, place_scene.transform.position, place_scene.transform.rotation);

                //Disable the point cloud as soon as the scene is initiated 
                GetComponent<ARPointCloudManager>().enabled = false;

                //spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

            }
            /*else if(place_scene.activeSelf && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                place_scene.transform.position = hitPose.position;
                //spawnedObject.transform.position = hitPose.position;
            }*/
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
