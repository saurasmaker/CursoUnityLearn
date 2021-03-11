using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private static CommandManager _instance;
    public static CommandManager Instance
    {
        get {
            if (_instance == null)
                Debug.LogError("The CommandManager is NULL");

            return _instance; 
        }
    }

    private List<ICommand> _commandBuffer = new List<ICommand>();

    private void Awake()
    {
        _instance = this;
    }


    public void AddCommand(ICommand command)
    {
        _commandBuffer.Add(command);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");

        foreach (var command in _commandBuffer)
        {
            command.Execute();
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("Finished...");
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        Debug.Log("Playing...");

        foreach (var command in Enumerable.Reverse(_commandBuffer)){
            command.Undue();
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Finished...");

    }

    public void Done()
    {
        var cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach(var cube in cubes)
        {
            cube.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        _commandBuffer.Clear();     
    }
}
