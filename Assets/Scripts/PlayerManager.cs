using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    private RaycastHit hit;
    List<UnitController> selectedUnits = new List<UnitController>();
    List<BuildingInteraction> selectedBuilding = new List<BuildingInteraction>();
    private bool isDragging = false;
    public bool buildMenu = false;
    private Vector3 mousePos;
    private Transform Spawn;
    private GameObject SpawnLoc;
    private GameObject SelectedObject;
    [SerializeField]
    private GameObject Unit;

    private void OnGUI()
    {
        if(isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePos, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        }
    }
    void Start()
    {
         SelectedObject = Selection.activeTransform.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
        //on mouse down
        if(Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }
            mousePos = Input.mousePosition;
            //create ray from camera
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //shoot ray & retrieve hit data
            if(Physics.Raycast(camRay, out hit))
            {
                Debug.Log(hit.transform.tag);
                //if we click on a unit
                if(hit.transform.CompareTag("PlayerUnit"))
                {
                    //Select unit
                    SelectUnit(hit.transform.gameObject.GetComponent<UnitController>(), Input.GetKey(KeyCode.LeftShift));
                }
                //if we click on a building
                else if (hit.transform.CompareTag("Building"))
                {
                    SelectBuilding(hit.transform.gameObject.GetComponent<BuildingInteraction>());
                    buildMenu = true;
                }
                //click on terrain
                else
                {
                    isDragging = true;
                    DeselectBuilding();
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(isDragging)
            {
                DeselectUnits();
                foreach (var selectableObject in FindObjectsOfType<PlayerUnitController>())
                {
                    if (IsWithinSelectBounds(selectableObject.transform))
                    {
                        SelectUnit(selectableObject.gameObject.GetComponent<UnitController>(), true);
                    }
                }
                isDragging = false;
            }
        }
        //moving units
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //shoot ray & retrieve hit data
            if (Physics.Raycast(camRay, out hit))
            {
                Debug.Log(hit.transform.tag);
                //if right click on terrain
                if (hit.transform.CompareTag("Terrain"))
                {
                    //move units to point
                    foreach(var SelectableObj in selectedUnits)
                    {
                        SelectableObj.MoveUnit(hit.point);
                    }
                }
                //if right click on enemy
                else if(hit.transform.CompareTag("EnemyUnit"))
                {
                    //move units to enemy
                    foreach (var SelectableObj in selectedUnits)
                    {
                        SelectableObj.SetNewTarget(hit.transform);
                    }
                }
            }
        }
    }

    private void SelectUnit(UnitController unit, bool isMultiSelect = false)
    {
        if (!isMultiSelect)
        {
            DeselectUnits();
        }
        selectedUnits.Add(unit);
        unit.SetSelected(true);
    }

    private void DeselectUnits()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].SetSelected(false);
        }
        selectedUnits.Clear();
    }

    private void SelectBuilding(BuildingInteraction Building)
    {
        Building.SetSelected(true);
        selectedBuilding.Add(Building);
        SpawnLoc = GameObject.Find("Unit Spawn");
        Spawn = SpawnLoc.transform;
    }

    private void DeselectBuilding()
    {
        for (int i = 0; i < selectedBuilding.Count; i++)
        {
            selectedBuilding[0].SetSelected(false);
        }
        selectedBuilding.Clear();
        buildMenu = false;

    }

    private bool IsWithinSelectBounds(Transform transform)
    {
        if(!isDragging)
        {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = ScreenHelper.GetViewportBounds(camera, mousePos, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
    }

    public void BuildUnit()
    {
        Instantiate(Unit, Spawn.position, Spawn.rotation);
    }
}
