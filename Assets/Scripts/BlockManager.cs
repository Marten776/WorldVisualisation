using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField] private string SelectableTag = "Selectable";
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private Material defaultMaterial;
    private Transform _selection;

    Vector3 tempY;


    public List<GameObject> created = new List<GameObject>();
    public List<GameObject> marked = new List<GameObject>();


    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
        }

        if(created.Count>0)
        {
            Highlighting();
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
                var renderer = m.GetComponentsInChildren<Renderer>();
                foreach (var s in renderer)
                    s.material = defaultMaterial;
            }
            marked.Clear();

            Debug.Log("Marked Cleared!");
        }
        OperationsOnCreatedObjects();
    }
    void Highlighting()
    {

            if (_selection != null)
            {
            if (_selection.CompareTag(SelectableTag))
            {
                var selectionRenderer = _selection.GetComponentsInChildren<Renderer>();
                foreach (var c in selectionRenderer)
                {
                    c.material = defaultMaterial;
                }
            }
                _selection = null;

            }
        
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(SelectableTag))
                {
                    var SelectionRenderer = selection.GetComponentsInChildren<Renderer>();
                    if (SelectionRenderer != null)
                    {
                        foreach (var b in SelectionRenderer)
                        {
                            b.material = highlightedMaterial;
                        }
                    }
                }
                _selection = selection;
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
            if (selection.CompareTag(SelectableTag))
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
