using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    //Camera positioning parameters
    private float m_distanceAway = 3.0f;
    private float m_distanceUp = 1.5f;
    private float m_heightOffset = 0.0f;
    private float m_smooth = 4.0f;

    //Position of the character and the target to look
    public GameObject m_characterPos;
    public Transform m_target;

    //Position of the camera based on the collision avoidance
    private Vector3 m_camPosWanted;

    //Variables that stores the raycast informations
    RaycastHit m_hitWall;
    RaycastHit m_hitUp;


    public void Start()
    {
        m_hitWall = new RaycastHit();
        m_hitUp = new RaycastHit();
    }


    public void Update()
    {
        //For collision avoidance we need 2 values: the distanceUp of the camera and the wanted position 
        Vector3 characterOffset = m_target.position + new Vector3(0f, m_distanceUp - m_heightOffset, 0f);
        drawDebugLines(characterOffset);
        m_camPosWanted = m_target.position + (m_target.up * (m_distanceUp - m_heightOffset)) - (m_target.forward * m_distanceAway);

        collisionAvoidance(characterOffset, ref m_camPosWanted);

        //Positionig and orienting the camera
        transform.position = Vector3.Lerp(transform.position, m_camPosWanted, Time.deltaTime * m_smooth);
        transform.LookAt(m_target);

    }


    private void collisionAvoidance(Vector3 fromObject, ref Vector3 toTarget)
    {

        //Camera Wall avoidance - raycast from the back of the character and from characterOffset to camPosWanted
        if (Physics.Linecast(fromObject, toTarget, out m_hitWall) ||
           Physics.Raycast(m_characterPos.transform.position, -m_characterPos.transform.forward, out m_hitWall, 3.0f))
        {
            toTarget = new Vector3(m_hitWall.point.x, toTarget.y, m_hitWall.point.z);
        }

        //Camera Ceil avoidance - raycast from the up of the character and from the up of the camera
        if (Physics.Raycast(transform.position, Vector3.up, out m_hitUp, 1.0f) ||
            Physics.Raycast(m_characterPos.transform.position, Vector3.up, out m_hitUp, 2.0f))
        {
            if (m_hitUp.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                if (m_camPosWanted.y > m_hitUp.distance)
                {
                    Debug.Log("Ceil");
                    m_heightOffset += 0.2f; //adjusting the height
                }
            }
        }
        else
        {
            if (m_heightOffset != 0.0f)     //height back to normal
            {
                m_heightOffset -= 0.1f;
                if (m_heightOffset < 0.0f)
                    m_heightOffset = 0.0f;
            }
        }
    }


    void OnGUI()
    {
        GUI.Label(new Rect(225, 20, 100.0f, 30.0f), "Smoothness");
        GUI.Label(new Rect(225, 50, 100.0f, 30.0f), "Distance up");
        GUI.Label(new Rect(225, 80, 100.0f, 30.0f), "Distance away");

        GUI.color = new Color(1, 0, 0);
        m_smooth = GUI.HorizontalSlider(new Rect(20, 20, 200, 20), m_smooth, 1.0f, 10.0f);

        GUI.color = new Color(0, 1, 0);
        m_distanceUp = GUI.HorizontalSlider(new Rect(20, 50, 200, 20), m_distanceUp, 1.0f, 10.0f);

        GUI.color = new Color(0, 0, 1);
        m_distanceAway = GUI.HorizontalSlider(new Rect(20, 80, 200, 20), m_distanceAway, 1.0f, 10.0f);
    }

    void drawDebugLines(Vector3 charOffset)
    {
        //Camera positioning
        Debug.DrawLine(m_target.position, charOffset, Color.green);
        Debug.DrawLine(m_target.position, m_camPosWanted, Color.yellow);
        Debug.DrawLine(m_target.position, new Vector3(m_camPosWanted.x,
                                                      m_target.position.y,
                                                      m_camPosWanted.z), Color.blue);

        //Camera Wall avoidance - raycast from the character
        Debug.DrawRay(m_characterPos.transform.position, -m_characterPos.transform.forward, Color.magenta);

        //Ceiling avoidance - raycast from the character
        Debug.DrawRay(m_characterPos.transform.position, Vector3.up, Color.magenta);
    }

    void OnDestroy()
    {

    }

}