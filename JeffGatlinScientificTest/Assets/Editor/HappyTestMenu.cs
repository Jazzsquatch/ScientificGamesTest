using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HappyTestMenu : ScriptableWizard {

    public struct PeopleData
    {
        public string name;
        public int birthYear;
        public int deathYear;
    }

    private const int MIN_YEAR = 1900;
    private const int MAX_YEAR = 2000;
    private const int MIN_AGE = 30;
    private const int MAX_AGE = 100;

    public static List<string> namesList = new List<string>() { "Chloe", "Emily", "Aaliyah", "Emma", "Jennifer", "Olivia", "Hannah",        "Jessica", "Kellie", "Savannah", "Sarah", "Lily", "Ava", "Sophia", "Isabella", "Mia", "Grace", "Ella", "Lauren", "Charlotte",        "Elizabeth", "Abigail", "Rebecca", "Samantha", "Kasmine", "Ashley", "Amy", "Anna", "Madison", "Zoe", "Jade", "Alyssa",        "Sophie", "Nicole", "Lucy", "Abby", "Megan", "Katie", "Amber", "Natalie", "Amanda", "Bella", "Rachel", "Claire", "Taylor",        "Alexis", "Vanessa", "Paige", "Alice", "Ellie"};

    public static List<PeopleData> peopleList = new List<PeopleData>();

    [MenuItem("Test/Determine Most Alive Year")]
    public static void DetermineMostAliveYear()
    {
        GenerateRandomPeopleList();
        PrintPeopleList();

        int yearWithMostPeopleAlive = GetYearWithMostPeopleAlive();

        Debug.Log("YearWithMostPeopleAlive: " + yearWithMostPeopleAlive);
    }

    private static int GetYearWithMostPeopleAlive()
    {
        int highestYear = 0;

        // ------------------------------------------------------------
        // Build an index of people alive during each year within range
        // ------------------------------------------------------------
        Dictionary<int, int> yearIndex = new Dictionary<int, int>();

        int i = 0;
        int j = 0;
        // init the index
        for (i = MIN_YEAR; i <= MAX_YEAR; i++)
        {
            yearIndex.Add(i, 0);
        }

        // add one to each year within the lifespan of a person
        PeopleData pdata;
        for (i = 0; i < peopleList.Count; i++)
        {
            pdata = peopleList[i];
            for (j = pdata.birthYear; j <= pdata.deathYear; j++)
            {
                yearIndex[j] += 1;
            }
        }

        // scan thru index and find most people alive
        int highestAlive = 0;
        foreach (KeyValuePair<int, int> kvp in yearIndex)
        {
            if (kvp.Value > highestAlive)
            {
                highestAlive = kvp.Value;
                highestYear = kvp.Key;
            }
        }
        Debug.Log("Most Alive " + highestAlive + " in " + highestYear);

        return highestYear;
    }

    private static void GenerateRandomPeopleList()
    {
        peopleList.Clear();

        PeopleData pdata;
        int age;
        for (int i = 0; i < namesList.Count; i++)
        {
            pdata = new PeopleData();
            pdata.name = namesList[i];
            age = Random.Range(MIN_AGE, MAX_AGE);
            pdata.birthYear = Random.Range(MIN_YEAR, MAX_YEAR - age);
            pdata.deathYear = pdata.birthYear + age;
            peopleList.Add(pdata);
        }
    }

    private static void PrintPeopleList()
    {
        string txt = "";
        PeopleData pdata;
        for (int i = 0; i < peopleList.Count; i++)
        {
            pdata = peopleList[i];
            txt += pdata.name + " Born: " + pdata.birthYear + " Died: " + pdata.deathYear + "\n";
        }
        Debug.Log(txt);
    }
}
