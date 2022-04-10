using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: usar scriptable objects pra esse sistema assim que possivel
public class FoodController : MonoBehaviour
{
    [SerializeField] private FoodParameters foodParameters;
    [SerializeField] private float currentFoodAmount;
    [SerializeField] private SpriteRenderer currentSprite;
    [SerializeField] private float timeToEnableFood = 1;
    [SerializeField] private List<GameObject> inactiveFoodPrefabList = new List<GameObject>();
    [SerializeField] private List<GameObject> activeFoodPrefabList = new List<GameObject>();

    private void Start()
    {        
        InstantiateAndDisableFood();
        currentFoodAmount = foodParameters.foodInitialAmount;
    }    

    private void Update()
    {
        FoodGenerator();
        SetFoodStage();

    }
    private void SetFoodStage()
    {
        FoodStage stage = GetFoodStage(currentFoodAmount);
        if (stage != null)
        {
            currentSprite.sprite = stage.foodStageSprite;
            //TODO: ativar/posicionar as food de acordo com a posicao de cada estagio

            if (activeFoodPrefabList.Count > stage.foodPositions.Length)
            {
                foreach (GameObject food in activeFoodPrefabList)
                {
                    food.SetActive(false);                  
                }
                activeFoodPrefabList = new List<GameObject>();              
            }
            else
            {
                if (activeFoodPrefabList.Count != stage.foodPositions.Length)
                {
                    for (int i = 0; i < stage.foodPositions.Length; i++)
                    {                        
                        GameObject food = inactiveFoodPrefabList[i];
                        
                        food.transform.localPosition = stage.foodPositions[i];                         
                        food.SetActive(true);
                        activeFoodPrefabList.Add(food);
                        
                    }
                }
            }
            EnableFood(activeFoodPrefabList);
        }
        else
        {
            currentSprite.sprite = null;
        }
    }

    private FoodStage GetFoodStage(float foodAmount)
    {
        for (int i = foodParameters.foodStages.Length - 1; i >= 0; i--)
        {
            FoodStage stage = foodParameters.foodStages[i];
            if (stage.minFoodAmount <= foodAmount)
            {
                return stage;
            }
        }
        return null;
    }
    private void InstantiateAndDisableFood()
    {
        foreach (var foodPos in foodParameters.foodStages[foodParameters.foodStages.Length - 1].foodPositions)
        {
            inactiveFoodPrefabList.Add(Instantiate(foodParameters.foodPrefab, transform.position, Quaternion.identity, transform));
        }
        DisableFood(inactiveFoodPrefabList);
    }
    private void FoodGenerator()
    {
        if (currentFoodAmount < foodParameters.foodMaxAmount)
        {
            //Time.deltaTime / 18 takes 1800 seconds(30 minutes) to deplete a 100 units of hungry
            currentFoodAmount += Time.deltaTime / 2;
        }
    }

    public void ReduceFoodAmount(float amount)
    {
        currentFoodAmount -= amount;
        if (currentFoodAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void EnableFood(List<GameObject> foods)
    {
        foreach (var food in foods)
        {
            if (!food.activeInHierarchy)
            {
                StartCoroutine(EnableFoodCor(food));
            }
        }
    }
    private void DisableFood(List<GameObject> foods)
    {
        foreach (var food in foods)
        {
            if (food.activeInHierarchy)
            {
                food.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator EnableFoodCor(GameObject food)
    {
        yield return new WaitForSeconds(timeToEnableFood);
        food.gameObject.SetActive(true);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var foodPos in GetFoodStage(currentFoodAmount).foodPositions)
        {
            if (foodPos != null)
            {
                Gizmos.DrawWireSphere(transform.position + foodPos, 0.2f);
            }

        }

    }

    private void OnDestroy()
    {
        EntityNumberController.RemoveFoodCount(foodParameters);
    }


}
