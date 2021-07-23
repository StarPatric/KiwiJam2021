using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCatcher : MonoBehaviour
{
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        raycastForitem();
    }

    // Click on items
    void raycastForitem()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            clickItem obj = hit.collider.gameObject.GetComponent<clickItem>();
            
            if (!obj)
                return;

            obj.wasClicked();
        }
    }

}
