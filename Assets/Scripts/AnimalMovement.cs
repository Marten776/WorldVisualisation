using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    float timeStartedLerping;
    bool shouldLerp = false;
    float lerpTime = 2f;
    bool isThirsty = false;
    Vector3 myPos;
    Vector3 newDir;
    Vector3 waterDirection;
    Vector3 foodDirection;
    public float thirstiness = 70f;
    public float hunger = 70f;
    bool shouldGo = true;
    bool foundFood = false;
    private float maxAltitude = 2f;
    public WorldMaterial actualCube;
    bool reachedGoalCube = true;
    private void Start()
    {
        myPos = transform.position;
        newDir = transform.position;
        InvokeRepeating("WaterNeed", 6f, 6f);
        InvokeRepeating("FoodNeed", 6f, 6f);
    }
    void Update()
    { 
        FindCubes();
        if(thirstiness == 0f)
        { 
            Destroy(gameObject);
        }
        if(hunger == 0f)
        {
            Destroy(gameObject);
        }
    }
    //void SearhCubes()
    //{
    //    List<WorldMaterial> foundCubes = new List<WorldMaterial>();
    //    var c = Physics.OverlapBox(transform.position, new Vector3(2, 2, 2), Quaternion.identity);

    //    foreach (var col in c)
    //    {
    //        if (col.GetComponent<WorldMaterial>()) foundCubes.Add(col.GetComponent<WorldMaterial>());
    //    }
    //    if (!isThirsty)
    //    {
    //        if (SearchForRandomCube(foundCubes)) return;
    //    }
    //    if (isThirsty)
    //    {
    //        if (SearchWaterCube(foundCubes)) return;
    //    }
    //}
    //private bool SearchSpecificCube(bool b, WorldMaterial currentCube)
    //{
    //    if (b && Mathf.Abs(actualCube.transform.position.y - currentCube.transform.position.y) < maxAltitude && !currentCube.isAnimalOn)
    //    {
    //        SetNewCube(currentCube);
    //        return true;
    //    }

    //    return false;
    //}

    //private bool SearchWaterCube(List<WorldMaterial> foundCubes)
    //{
    //    foreach (var c in foundCubes)
    //    {
    //        if (SearchSpecificCube(c.isWater, c))
    //        {
    //            c.isAnimalOn = true;
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //private bool SearchForRandomCube(List<WorldMaterial> foundCubes)
    //{

    //    int listCount = foundCubes.Count;
    //    int elementNumber = Random.Range(0, listCount);
    //    var currentGoal = foundCubes[elementNumber];
    //    SetNewCube(currentGoal);

    //    return false;
    //}

    //private void SetNewCube(WorldMaterial c)
    //{
    //    actualCube.isAnimalOn = false;
    //    actualCube = c;
    //    GoToActualCube(actualCube);
    //    reachedActualCube = false;

    //}

    //private void GoToActualCube(WorldMaterial c)
    //{
    //    c.transform.position += Vector3.up;
    //    transform.position = Vector3.Lerp(transform.position, c.transform.position, lerpTime * Time.deltaTime);
    //    if (Vector3.Distance(transform.position, c.transform.position) < .5f)
    //    {
    //        reachedActualCube = true;
    //        return;
    //    }

    //}
    void FindCubes()
    {
        //if (reachedGoalCube == true)
       // {
            Collider[] ground=Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);
            Movement(ground);
            if (thirstiness <= 50)
            {
                GoToWater(ground);
            }
            if (hunger <= 50)
            {
                GoToFood(ground);
           }
        //}
    }
    void Movement(Collider[] ground)
    {
        myPos = transform.position;
        //reachedGoalCube = false;
        if (Vector3.Distance(myPos, newDir) > .1f)
        {

            transform.position = Vector3.Lerp(myPos, newDir, lerpTime * Time.deltaTime);
            reachedGoalCube = false;
        }
        if ((Vector3.Distance(myPos, newDir) < .1f || shouldGo == false))
        {
            int elementNumber = Random.Range(0, ground.Length);
            var currentGoal = ground[elementNumber];
            if (currentGoal.tag == "Selectable")
            {
                newDir = currentGoal.transform.position;
                float newDirScale = currentGoal.transform.localScale.y;

                newDir = new Vector3(currentGoal.transform.position.x, currentGoal.transform.position.y + newDirScale, currentGoal.transform.position.z);
                //Debug.Log("I want to go there: " + newDir.y + " And my current position is: " + transform.position.y);
                if (newDir.y - transform.position.y > .5f || newDir.y - transform.position.y < -.5f)
                {
                    Debug.Log("Opps, i'd prefer not to go there");
                    shouldGo = false;
                    return;
                }
                transform.LookAt(newDir);
               // Debug.Log(newDir);
                shouldGo = true;
                reachedGoalCube = true;
            }
        }


    }
    void GoToFood(Collider[] ground)
    {
        

        myPos = transform.position;
        foreach (Collider p in ground)
        {
            if (p.CompareTag("Bush"))
            {
                //Debug.Log("Yaaaay! Found some food");
                foundFood = true;
                waterDirection = p.transform.position;
                
                Vector3 bushPosition = p.transform.position;
                if (bushPosition.y - transform.position.y > .5f || bushPosition.y - transform.position.y < -.5f)
                {
                    Debug.Log("Opps, i'd prefer not to go for this bush");
                    //shouldGo = false;
                    return;
                }

                //Debug.Log("Bush pleace " + bushPosition);
                transform.LookAt(bushPosition);
                transform.position = Vector3.MoveTowards(transform.position,bushPosition,lerpTime*Time.deltaTime);
                //Debug.Log("Distance: " + Vector3.Distance(transform.position, bushPosition));
                if (Vector3.Distance(transform.position,bushPosition) < 1f)
                {
                    hunger = 100f;
                    Debug.Log("Yummy");
                    Destroy(p.gameObject);
                    return;
                }
            }
        }
    }
    void GoToWater(Collider[] ground)
    {
        

        myPos = transform.position;
        foreach (Collider p in ground)
        {
            if (p.CompareTag("Water"))
            {

                Debug.Log("Yaaaay! Found some water");
                _ = p.transform.position;
                float WaterScale = p.transform.localScale.y;
                Vector3 waterDirection = new Vector3(p.transform.position.x, p.transform.position.y + WaterScale, p.transform.position.z);
                if (waterDirection.y - transform.position.y > .5f || waterDirection.y - transform.position.y < -.5f)
                {
                    Debug.Log("Opps, i'd prefer not to go for this water");
                    shouldGo = false;
                    return;
                }

                Debug.Log("Water pleace " + waterDirection);
                transform.LookAt(waterDirection);

                if (Vector3.Distance(myPos, waterDirection) > .5f)
                {
                    transform.position = Vector3.Lerp(myPos, waterDirection, lerpTime* Time.deltaTime);
                    thirstiness = 100f;
                    Debug.Log("ahhhhh");
                    return;

                }
            }
        }
    }
    void WaterNeed()
    {
        thirstiness -= 10f;
        Debug.Log("Rabbit thirstines level is " + thirstiness);
    }
    void FoodNeed()
    {
        hunger -= 10f;
        Debug.Log("Rabbit hunger level is " + hunger);
    }
}
