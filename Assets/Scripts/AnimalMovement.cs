using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalMovement : MonoBehaviour
{
    float timeStartedLerping;
    bool shouldLerp = false;
    float lerpTime = .5f;
    float lerpWaterTime = 1f;
    bool isThirsty = false;
    bool isHungry = false;
    Vector3 myPos;
    Vector3 newDir;
    Vector3 waterDirection;
    Vector3 foodDirection;
    //public int thirstiness = 100;
    //public int hunger = 100;
    //public int maxThirstiness; 
    //public int maxHunger; 
    bool shouldGo = true;
    bool foundFood = false;
    private float maxAltitude = .5f;
    public WorldMaterial actualCube = null;
    bool reachedGoalCube = true;
    bool currWater=false;
    public bool reachedActualCube=true;
    public bool isDying = false;
    public bool isEating = false;
    bool canWalk = true;

    private string selectableTag = "Selectable";

    public BasicNeeds bn;

    public bool foundVictim = false;

    public FoodChasing fc;
    //public HealthBar healthBar;
    //public HungerBar hungerBar;
    void Start()
    {
        myPos = transform.position;
        newDir = transform.position;
        bn = GetComponent<BasicNeeds>();
        fc = GetComponent<FoodChasing>();
        //maxThirstiness = bn.thirstiness;
        //maxHunger = bn.hunger;
        //healthBar.SetMaxHealth(maxThirstiness);
        //hungerBar.SetMaxHunger(maxHunger);

        
        //InvokeRepeating("WaterNeed", 6f, 6f);
      // InvokeRepeating("FoodNeed", 7f, 7f);
    }
    void Update()
    {
        if(canWalk)
        SearhCubes();
        GoToActualCube(actualCube);

        //if(fc.foundVictim==true)
        //{
         //   canWalk = false;
        //}
        //if(fc.foundVictim==false)
        //{
          //  canWalk = true;
        //}

        if (actualCube.isWater)
        {
            GoToWaterCube(actualCube);
        }
        if (actualCube.isPlantOn)
        {
           if (isEating)
               Eating();

            GoToPlantCube(actualCube);
        }


        
        if (bn.thirstiness <= 50)
           isThirsty = true;
        if (bn.hunger <= 50)
            isHungry = true;
        AnimalDying();
    }


    void AnimalDying()
    {
        if (bn.thirstiness <= 0f)
        {
            isDying = true;
            canWalk = false;
        }
        if (bn.hunger <= 0f)
        {
            isDying = true;
            canWalk = false;
        }


        if (bn.thirstiness <= -10f)
        {
            isDying = false;
            Destroy(gameObject);
        }

        if (bn.hunger <= -10f)
        {
            isDying = false;
            Destroy(gameObject);

        }
    }
    void SearhCubes()
    {
        if (!reachedActualCube)
            return;
        
        List<WorldMaterial> foundCubes = new List<WorldMaterial>();
        var c = Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);

        foreach (var col in c)
         {
             if (col.GetComponent<WorldMaterial>()) foundCubes.Add(col.GetComponent<WorldMaterial>());
         }

        if (isThirsty)
        {
            if (SearchWaterCube(foundCubes) == false)
            {
                if (SearchForRandomCube(foundCubes)) return;
            }
            else
            {
                if (SearchWaterCube(foundCubes)) return;
            }
        }
        if (isHungry)
        {
            if (SearchPlantCube(foundCubes) == false)
            {
                if (SearchForRandomCube(foundCubes)) return;
            }
            else
            {
                if (SearchPlantCube(foundCubes)) return;
            }
        }
        if (SearchForRandomCube(foundCubes)) return;

    }
    private bool SearchSpecificCube(bool b, WorldMaterial currentCube)
    {
        if (b && Mathf.Abs(actualCube.transform.localScale.y - currentCube.transform.localScale.y) < maxAltitude &&  !currentCube.isAnimalOn)
        {
            SetNewCube(currentCube);
            return true;
        }

        return false;
    }
    WorldMaterial FindClosestElement(List<WorldMaterial> foundCubes)
    {
        WorldMaterial closestElementPosition = actualCube;
        float closestDistanceToElement = Mathf.Infinity;
        foreach (var element in foundCubes)
        {
            if (element.isWater)
            {
                float distanceToElement = Vector3.Distance(transform.position, element.transform.position);
                if (distanceToElement < closestDistanceToElement)
                {
                    closestDistanceToElement = distanceToElement;
                    closestElementPosition = element;
                }
            }
        }
        return closestElementPosition;
    }

    private bool SearchWaterCube(List<WorldMaterial> foundCubes)
    {
            WorldMaterial closestElement=FindClosestElement(foundCubes);
            if (SearchSpecificCube(closestElement.isWater, closestElement))
            {
            closestElement.isAnimalOn = true;
                return true;
            }
        return false;
    }
    private bool SearchPlantCube(List<WorldMaterial> foundCubes)
    {
        foreach (var c in foundCubes)
        {
            if (SearchSpecificCube(c.isPlantOn, c))
            {
                c.isPlantOn = true;
                return true;
            }
        }
        return false;
    }
    private bool SearchForRandomCube(List<WorldMaterial> foundCubes)
    {
        int listCount = foundCubes.Count;
        int elementNumber = Random.Range(0, listCount);
        var currentGoal = foundCubes[elementNumber];
        if (!currentGoal.isWater && !currentGoal.isPlantOn)
        {
            if (actualCube != null)
            {
                //if (Mathf.Abs(actualCube.transform.localScale.y - currentGoal.transform.localScale.y) < maxAltitude)
                SetNewCube(currentGoal);
            }
        }
        return false;
    }


    private void SetNewCube(WorldMaterial c)
    {
        actualCube.isAnimalOn = false;
        //actualCube.isClear = true;
        actualCube = c;
        reachedActualCube = false;
        if (actualCube.isWater&& isThirsty)
        {
            GoToWaterCube(actualCube);
        }
        if(actualCube.isPlantOn && isHungry)
        {
            Debug.Log("Found some food!");
            GoToPlantCube(actualCube);
        }
        else
            GoToActualCube(actualCube);

        

    }

    private void GoToActualCube(WorldMaterial c)
    {
        
        Vector3 position = c.transform.position;
        if(gameObject.transform.position.y > position.y)
        {
            float scale = c.transform.localScale.y;
            position = new Vector3(position.x, position.y + scale, position.z);

        }
        transform.LookAt(position);
        transform.position = Vector3.Lerp(transform.position, position, lerpTime * Time.deltaTime);




        //Debug.Log("I want to go there: "+ position);
        if (Vector3.Distance(transform.position, position) < 2f)
        {
            //Debug.Log("I end up here " + position);
            reachedActualCube = true;
            return;
        }
    }
    private void GoToWaterCube(WorldMaterial c)
    {
        Vector3 waterPosition = c.transform.position;
        if (gameObject.transform.localScale.y - c.transform.localScale.y <= .7f)
        {
            float scale = c.transform.localScale.y;
            waterPosition = new Vector3(waterPosition.x, waterPosition.y + scale, waterPosition.z);
        }

        transform.LookAt(waterPosition);
        transform.position = Vector3.Lerp(transform.position, waterPosition, lerpWaterTime * Time.deltaTime);
        //Debug.Log("I am thirsty ");
        if (Vector3.Distance(transform.position, waterPosition) < 2f)
        {
            
            Debug.Log("ahhhh");
            bn.thirstiness = 100;
            bn.healthBar.Health(bn.thirstiness);
            reachedActualCube = true;
            isThirsty = false;
            return;
        }
    }
    private void GoToPlantCube(WorldMaterial c)
    {
        Vector3 waterPosition = c.transform.position;
        transform.LookAt(waterPosition);
        transform.position = Vector3.Lerp(transform.position, waterPosition, lerpWaterTime * Time.deltaTime);
        //Debug.Log("I am thirsty ");
        if (Vector3.Distance(transform.position, waterPosition) < 2f)
        {
            isEating = true;
            canWalk = false;
            Eating();
            //Debug.Log("Omnomonom");
            //hunger = 100;
            //hungerBar.Hunger(hunger);
            //reachedActualCube = true;
            //isHungry = false;
           
            //return;
        }
    }
    void Eating()
    {
        bn.hunger += 1;
        bn.hungerBar.Hunger(bn.hunger);
        Debug.Log("omnomonomon");
        if(bn.hunger>= 100)
        {
            reachedActualCube = true;
            canWalk = true;
            isHungry = false;
            isEating = false;
            return;
        }
    }

    //void FindCubes()
    //{
    //    //if (reachedGoalCube == true)
    //   // {
    //        Collider[] ground=Physics.OverlapBox(transform.position, new Vector3(10, 10, 10), Quaternion.identity);
    //        Movement(ground);

    //        if (thirstiness <= 50)
    //        {
    //            GoToWater(ground);
    //        }
    //        if (hunger <= 50)
    //        {
    //            GoToFood(ground);
    //       }
    //   // }
    //}
    //void Movement(Collider[] ground)
    //{
    //    myPos = transform.position;


    //    if ((Vector3.Distance(myPos, newDir) < .1f || shouldGo == false))
    //    {
    //        int elementNumber = Random.Range(0, ground.Length);
    //        var currentGoal = ground[elementNumber];
    //        if (currentGoal.tag == "Selectable")
    //        {
    //            newDir = currentGoal.transform.position;
    //            float newDirScale = currentGoal.transform.localScale.y;

    //            newDir = new Vector3(currentGoal.transform.position.x, currentGoal.transform.position.y + newDirScale, currentGoal.transform.position.z);
    //            //Debug.Log("I want to go there: " + newDir.y + " And my current position is: " + transform.position.y);
    //            if (newDir.y - transform.position.y > .5f || newDir.y - transform.position.y < -.5f)
    //            {
    //                Debug.Log("Opps, i'd prefer not to go there");
    //                shouldGo = false;
    //                return;
    //            }
    //            transform.LookAt(newDir);
    //           // Debug.Log(newDir);
    //            shouldGo = true;

    //        }
    //    }
    //    if (Vector3.Distance(myPos, newDir) > .1f)
    //    {
    //        transform.position = Vector3.Lerp(myPos, newDir, lerpTime * Time.deltaTime);
    //        reachedGoalCube = true;
    //    }

    //}
    //void GoToFood(Collider[] ground)
    //{
    //    myPos = transform.position;
    //    foreach (Collider p in ground)
    //    {
    //        if (p.CompareTag("Bush"))
    //        {
    //            //Debug.Log("Yaaaay! Found some food");
    //            foundFood = true;
    //            waterDirection = p.transform.position;
    //            Vector3 bushPosition = p.transform.position;
    //            if (bushPosition.y - transform.position.y > .5f || bushPosition.y - transform.position.y < -.5f)
    //            {
    //                Debug.Log("Opps, i'd prefer not to go for this bush");
    //                //shouldGo = false;
    //                return;
    //            }
    //            //Debug.Log("Bush pleace " + bushPosition);
    //            transform.LookAt(bushPosition);
    //            transform.position = Vector3.MoveTowards(transform.position,bushPosition,lerpTime*Time.deltaTime);
    //            //Debug.Log("Distance: " + Vector3.Distance(transform.position, bushPosition));
    //            if (Vector3.Distance(transform.position,bushPosition) < 1f)
    //            {
    //                hunger = 100f;
    //                Debug.Log("Yummy");
    //                Destroy(p.gameObject);
    //                return;
    //            }
    //        }
    //    }
    //}
    //void GoToWater(Collider[] ground)
    //{
    //    myPos = transform.position;
    //    foreach (Collider p in ground)
    //    {
    //        if (p.CompareTag("Water"))
    //        {

    //                Debug.Log("Yaaaay! Found some new water");
    //                _ = p.transform.position;
    //                float WaterScale = p.transform.localScale.y;
    //                Vector3 waterDirection = new Vector3(p.transform.position.x, p.transform.position.y + WaterScale, p.transform.position.z);
    //                if (waterDirection.y - transform.position.y > .5f || waterDirection.y - transform.position.y < -.5f)
    //                {
    //                    Debug.Log("Opps, i'd prefer not to go for this water");
    //                    shouldGo = false;
    //                    return;
    //                }
    //                Debug.Log("Water pleace " + waterDirection);
    //                transform.LookAt(waterDirection);
    //                if (Vector3.Distance(myPos, waterDirection) >= 2f)
    //                {
    //                    transform.position = Vector3.Lerp(myPos, waterDirection, lerpTime * Time.deltaTime);
    //                }
    //                else
    //                {
    //                    thirstiness = 100f;
    //                    Debug.Log("ahhhh. i drinked from " + Vector3.Distance(myPos, waterDirection));
    //                    currWater = false;
    //                    return;
    //                }

    //        }
    //    }
    //}
    //void WaterNeed()
    //{
    //    thirstiness -= 10;
    //    healthBar.Health(thirstiness);
    //}
    //void FoodNeed()
    //{
    //    hunger -= 10;
    //    hungerBar.Hunger(hunger);
    //}
}
