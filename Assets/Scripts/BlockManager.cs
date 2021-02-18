using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class BlockManager : MonoBehaviour
{

    BuildManager buildManager;

    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string waterTag = "Water";
    [SerializeField] private Material highlightedMat;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material waterMat;
    private Transform _selection;

    public RectTransform selectionBox;

    //Highlight highlight;

    //public RectTransform selectionBox;
     public Vector2 startPos;


    Vector3 tempY;

    Camera cam;


    public List<GameObject> created = new List<GameObject>();
    public List<GameObject> marked = new List<GameObject>();


    public List<GameObject> water = new List<GameObject>();

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //highlight = gameObject.AddComponent<Highlight>();
    }
    void Update()
    {
        GroundCreated();
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
           ReleaseSelectionBox();
        }
        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
        if (created.Count>0)
        {
            Highlighting();
        }
        foreach(var m in marked)
        {
            // podswietlanie zaznaczonych na czerwono
            var renderer = m.GetComponentsInChildren<Renderer>();
            foreach(var s in renderer)
                s.material = highlightedMat;
        }
        
        if (Input.GetKeyDown("c"))
        {
            foreach (var m in marked)
            {
                
                if (m.gameObject.tag=="Water")
                {
                    m.transform.localScale = new Vector3(m.transform.localScale.x, m.transform.localScale.y + 0.2f, m.transform.localScale.z);

                }
                m.gameObject.tag = selectableTag;
                var renderer = m.GetComponentsInChildren<Renderer>();
                foreach (var s in renderer)
                    s.material = defaultMat;
                    
            }
            marked.Clear();
            water.Clear();
            Debug.Log("Marked Cleared!");
        }

        if(Input.GetKeyDown("b"))
        {
            
            foreach (var pp in marked)
            {

                pp.gameObject.tag = waterTag;
                var renderer = pp.GetComponentsInChildren<Renderer>();
                water.Add(pp);
                foreach (var sp in renderer)
                {
                    
                    sp.material = waterMat;
                    pp.transform.localScale = new Vector3(pp.transform.localScale.x, pp.transform.localScale.y-0.2f, pp.transform.localScale.z);
                 }
            }
            marked.Clear();

            Debug.Log("Water Created!");
        }

        OperationsOnCreatedObjects();

    }




    void GroundCreated()
    {
        GameObject[] cre = GameObject.FindGameObjectsWithTag(selectableTag);

        
        if (cre.Length > 0 && cre.Length > created.Count)
        {
           
           foreach (GameObject worldItem in cre)
            {
                created.Add(worldItem);
            }

       }
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
            if (selection.CompareTag(selectableTag) || selection.CompareTag(waterTag))
            {
                sel(selection.gameObject);
            }

        }
    }
    void sel(GameObject selection)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
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
        if (marked.Count > 0)
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
               // if (c.transform.localScale.y == 1)
               // {
               //     water.Add(c);
                //    return;
               // }
                //else if (c.transform.localScale.y > 1)
                //{     
                //    water.Remove(c);
                //    return;
                //}
                //GroundScal(c);
            }
        }
        
    }
    public void GroundScal(GameObject obj)
    {
        tempY = obj.transform.localScale;
        
        if (Input.GetKeyDown("h"))
        {
            
            tempY.y += 0.2f;
        }
        else if (Input.GetKeyDown("l"))
        {
            if(tempY.y <=.3f)
            {
                Debug.Log("No possible, cant go so low");
                return;
            }
            tempY.y -= 0.2f;
        }
        obj.transform.localScale = tempY;
    }
    public void Highlighting()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (_selection != null)
        {
            if (_selection.CompareTag(selectableTag))
            {
                var selectionRenderer = _selection.GetComponentsInChildren<Renderer>();
                foreach (var c in selectionRenderer)
                {
                    c.material = defaultMat;
                }
            }
            if (_selection.CompareTag(waterTag))
            {
                var selectionRenderer = _selection.GetComponentsInChildren<Renderer>();
                foreach (var b in selectionRenderer)
                {
                    b.material = waterMat;
                }
            }
            _selection = null;

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag) || selection.CompareTag(waterTag))
            {
                var SelectionRenderer = selection.GetComponentsInChildren<Renderer>();
                if (SelectionRenderer != null)
                {
                    foreach (var b in SelectionRenderer)
                    {
                        b.material = highlightedMat;
                    }
                }
            }

            _selection = selection;
        }



    }

}
