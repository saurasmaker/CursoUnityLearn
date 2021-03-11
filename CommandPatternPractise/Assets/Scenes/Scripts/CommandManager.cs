using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{

    private List<ICommand> _commandBuffer = new List<ICommand>();
    private static CommandManager _instance;
    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The CommandManager is NULL.");

            return _instance;
        }
    }

    public void AddCommand(ICommand command)
    {
        _commandBuffer.Add(command);
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        Debug.Log("Rewinding...");
        foreach(var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undue();
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished...");
    }

    public void Player()
    {
        StartCoroutine(PlayerRoutine());
    }

    IEnumerator PlayerRoutine()
    {
        Debug.Log("Rewinding...");
        foreach (var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Execute();
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished...");
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
