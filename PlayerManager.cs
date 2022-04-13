using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player mechanics are handled here

public class PlayerManager : MonoBehaviour
{
    private RaycastHit hit;
    List<UnitController> selectedUnits = new List<UnitController>();
    private bool isDragging = false;
    public bool buildMenu = false;
    private Vector3 mousePos;

    private void OnGUI()
    {
        //Simulates Drag-Select while mouse is held down
        if(isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePos, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //on mouse down
        if(Input.GetMouseButtonDown(0))
        {
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
                else if (hit.transform.CompareTag("Building"))
                {
                    buildMenu = true;
                }
                else
                {
                    isDragging = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(isDragging)
            {
                //Deselect current selected units and select units in drag-select zone
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

    //only select one unit while isMultiSelect(shift button) is false
    private void SelectUnit(UnitController unit, bool isMultiSelect = false)
    {
        if (!isMultiSelect)
        {
            DeselectUnits();
        }
        selectedUnits.Add(unit);
        unit.SetSelected(true);
    }

    //removes units from selectedUnits array
    private void DeselectUnits()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].SetSelected(false);
        }
        selectedUnits.Clear();
    }

    //determins the bounds of the drag-select
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
        /*
         * 
         */
    }
}
