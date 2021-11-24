using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFloorBehavior : MonoBehaviour
{
    public GameObject[] CubeTypeArray;
    [SerializeField]int CurrentCubeType;
    public Vector3 SpeedOfCube = new Vector3(0,1,0);
    void Update()
    {
        transform.position += SpeedOfCube*Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("GameController"))
        {
            ChooseCubeType();
            MoveCubePosition();
        }
    }
    void ChooseCubeType()
    {
        CubeTypeArray[CurrentCubeType].SetActive(false);
        CurrentCubeType = Random.Range(0,CubeTypeArray.Length);
        CubeTypeArray[CurrentCubeType].SetActive(true);
    }    
    void MoveCubePosition()
    {
        Vector3 newposition = new Vector3(Mathf.Clamp((transform.position.x + Random.Range(-1f,1f)),-3f,3.8f),
        transform.position.y-13f,
        0f); 
                
        transform.position = newposition;
    }

    public int GetCurrnetCubeType()
    {
        return CurrentCubeType;
    }
    public void VanishCube()
    {
        StartCoroutine(Vanishing());
    }
    IEnumerator Vanishing()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        CubeTypeArray[6].GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        this.gameObject.GetComponent<Collider>().enabled = true;
        CubeTypeArray[6].GetComponent<Renderer>().enabled = true;
    }

    //below is function structure


    public delegate void CubeFunction(PlayerBehavior Gameobj,GameObject Cubeobj);
    public static CubeFunction[] CubeFunctionsArray = new CubeFunction[]
    {
        GeneralCubeFunction,RedCubeFunction,OrengeCubeFunction,YellowCubeFunction,
        GreenCubeFunction,BlueCubeFunction,PurpleCubeFunction
    };
    
    static void GeneralCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.ChangeHp(1);
    }
    static void RedCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.ChangeHp(-1);
    }
    static void OrengeCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.Burning();
    }    
    static void YellowCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.ChangeHp(1);
        Playerobj.Jump(new Vector3(0,5,0));
    }    
    static void GreenCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.ChangeHp(3);
    }
    static void BlueCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Playerobj.Skidding();
    }
    static void PurpleCubeFunction(PlayerBehavior Playerobj,GameObject Cubeobj)
    {
        Cubeobj.GetComponent<GeneralFloorBehavior>().VanishCube();        
    }
}
