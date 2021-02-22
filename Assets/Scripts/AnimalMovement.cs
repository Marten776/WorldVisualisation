using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalMovement : MonoBehaviour
{
    float lerpTime = .5f;
    float lerpWaterTime = 1f;
    bool isThirsty = false;
    bool isHungry = false;
    private float maxAltitude = .5f;
    public WorldMaterial actualCube = null;
    public bool reachedActualCube=true;
    public bool isDying = false;
    public bool isEating = false;
    bool canWalk = true;
    bool isScaled = false;
    private string selectableTag = "Selectable";

    public BasicNeeds bn;

    public bool foundVictim = false;

    public FoodChasing fc;
    //public HealthBar healthBar;
    //public HungerBar hungerBar;
    void Start()
    {

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

        if(foundVictim==true)
        {
            canWalk = false;
        }
        if (foundVictim == false)
        {
            canWalk = true;
        }

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


        
        if (bn.thirstiness <= 50 )
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
        Vector3 waterPosition = ScaleGoal(c.gameObject);
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
        
        Vector3 waterPosition = ScaleGoal(c.gameObject);
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
            isScaled = false;
            return;
        }
    }

    Vector3 ScaleGoal(GameObject goal)
    {
        var scale = goal.transform.localScale.y;

        
        Vector3 newGoal = new Vector3(goal.transform.position.x, goal.transform.position.y + scale, goal.transform.position.z);
        if (goal.transform.position.y == (scale + newGoal.y))
            return goal.transform.position;
        else
        return newGoal;
    }
}
