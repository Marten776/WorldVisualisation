using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour
{
    [SerializeField] private string SelectableTag = "Selectable";
    [SerializeField] private string WaterTag = "Water";
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material WaterMaterial;
    private Transform _selection;
    public void Highlighting()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
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
            if (_selection.CompareTag(WaterTag))
            {
                var selectionRenderer = _selection.GetComponentsInChildren<Renderer>();
                foreach (var b in selectionRenderer)
                {
                    b.material = WaterMaterial;
                }
            }
            _selection = null;

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();


        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(SelectableTag) || selection.CompareTag(WaterTag))
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

}
