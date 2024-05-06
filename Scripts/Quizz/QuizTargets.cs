using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuizTargets : MonoBehaviour
{
    [SerializeField]
    public List<TargetObject> allTargetObjects; // Populate this list in the inspector

    // This function will be called to get 10 random objects from allTargetObjects
    public List<TargetObject> GetRandomTargets(int numberOfQuestions)
    {
        // Ensure we don't request more questions than available targets
        int questionsToGenerate = Mathf.Min(numberOfQuestions, allTargetObjects.Count);

        // Shuffle the list using Fisher-Yates algorithm
        for (int i = allTargetObjects.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            TargetObject temp = allTargetObjects[i];
            allTargetObjects[i] = allTargetObjects[swapIndex];
            allTargetObjects[swapIndex] = temp;
        }

        // Take the first questionsToGenerate items after shuffling
        return allTargetObjects.Take(questionsToGenerate).ToList();
    }

}
