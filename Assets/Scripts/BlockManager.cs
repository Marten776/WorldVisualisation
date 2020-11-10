﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField] private string SelectableTag = "Selectable";
    [SerializeField] private string WaterTag = "Water";
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material WaterMaterial;
    private Transform _selection;

    public RectTransform selectionBox;

    public Highlight highlight = new Highlight();

    //public RectTransform selectionBox;
     public Vector2 startPos;


    Vector3 tempY;

    Camera cam;


    public List<GameObject> created = new List<GameObject>();
    public List<GameObject> marked = new List<GameObject>();


    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
           ReleaseSelectionBox();
        }
        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
        if (created.Count>0)
        {
            highlight.Highlighting();
        }
        foreach(var m in marked)
        {
            // podswietlanie zaznaczonych na czerwono
            var renderer = m.GetComponentsInChildren<Renderer>();
            foreach(var s in renderer)
                s.material = highlightedMaterial;
        }
        
        if (Input.GetKeyDown("c"))
        {
            foreach (var m in marked)
            {
                m.gameObject.tag = SelectableTag;
                var renderer = m.GetComponentsInChildren<Renderer>();
                foreach (var s in renderer)
                    s.material = defaultMaterial;
            }
            marked.Clear();

            Debug.Log("Marked Cleared!");
        }

        if(Input.GetKeyDown("b"))
        {
            foreach(var pp in marked)
            {
                pp.gameObject.tag = WaterTag;
                var renderer = pp.GetComponentsInChildren<Renderer>();

                foreach (var sp in renderer)
                {
                    sp.material = WaterMaterial;
                }
                    
                
            }
            marked.Clear();

            Debug.Log("Water Created!");
        }

        OperationsOnCreatedObjects();

    }
    
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        bool m = false;
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);
        if (created.Count > 0)
        {
            foreach (var v in created)
            {

                Vector3 screenPos = cam.WorldToScreenPoint(v.transform.position);

                if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                {
                    marked.Add(v);
                    m = true;
                }

            }

        }
        if (m == true)
        {
            Debug.Log("Group of fields Marked! Number of fields marked: " + marked.Count);
            m = false;
        }

    }
    
    void Raycast()
    {
        if (_selection != null)
        {
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(SelectableTag) || selection.CompareTag(WaterTag))
            {
                sel(selection.gameObject);
            }

        }

    }
    void sel(GameObject selection)
    {

        if (created != null)
        {
            foreach (var c in created)
            {
                if (c.transform == selection.gameObject.transform)
                {
                    if (marked != null)
                    {
                        foreach (var m in marked)
                        {
                            if (m.transform == selection.gameObject.transform)
                            {
                                return;
                            }
                        }
                    }
                    
                    marked.Add(selection.gameObject);
                    
                    Debug.Log("Field Marked! Number of fields marked: " + marked.Count);
                    return;
                }

            }
        }

        created.Add(selection.gameObject);
    }
   
    void OperationsOnCreatedObjects()
    {
        if(marked.Count>0)
        {
            foreach (var m in marked)
            {
                if (m == null)
                    return;
                GroundScal(m);
            }

        }
        else
        {
            foreach (var c in created)
            {
                if (c == null)
                    return;
                GroundScal(c);
            }
        }

        
    }
    public void GroundScal(GameObject obj)
    {
        tempY = obj.transform.localScale;
        
        if (Input.GetKeyDown("h"))
        {
            tempY.y += 1f;
        }
        else if (Input.GetKeyDown("l"))
        {
            if (tempY.y <= 0)
            {
                Debug.Log("No possible, object destroyed!");

                created.Remove(obj);
                marked.Remove(obj);
                Destroy(obj);
                return;
            }
            tempY.y -= 1f;
        }
        obj.transform.localScale = tempY;
    }


}
