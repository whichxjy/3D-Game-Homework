using UnityEngine;
using System.Collections.Generic;

public class UFOFactory {
    // singleton instance
    private static UFOFactory instance;
    // waiting queue
    private Queue<UFOController> waiting = new Queue<UFOController>();
    // running list
    private List<UFOController> running = new List<UFOController>();

    public static UFOFactory GetInstance() {
        if (instance == null) {
            instance = new UFOFactory();
        }
        return instance;
    }

    public UFOController GetUFO(UFOInfo info) {
        // get unfo controller
        UFOController ufoController;
        if (waiting.Count > 0) {
            ufoController = waiting.Dequeue();
            ufoController.SetInfo(info);
            ufoController.obj.transform.position = new Vector3(Random.Range(-2, 2), 20, Random.Range(-2, 2));
            Rigidbody rigidbody = ufoController.obj.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }
        else {
            Vector3 initPosition = new Vector3(Random.Range(-2, 2), 20, Random.Range(-2, 2));
            GameObject ufoObj = Loader.LoadObj("Prefabs/UFO", initPosition);
            Rigidbody rigidbody = ufoObj.AddComponent<Rigidbody>();
            rigidbody.AddForce(info.speed * Vector3.one, ForceMode.Impulse);
            rigidbody.useGravity = true;
            ufoController = ufoObj.AddComponent<UFOController>() as UFOController;
            ufoController.info = info;
            ufoController.obj = ufoObj;
        }

        // set material
        Material mat;
        if (info.color == UFOColor.Red) {
            mat = Loader.LoadMat("Materials/Red");
        }
        else if (info.color == UFOColor.Green) {
            mat = Loader.LoadMat("Materials/Green");
        }
        else {
            mat = Loader.LoadMat("Materials/Blue");
        }
        ufoController.obj.GetComponent<MeshRenderer>().material = mat;

        running.Add(ufoController);
        ufoController.Appear();
        return ufoController;
    }

    public void RecycleUFO(UFOController ufoController) {
        if (running.Contains(ufoController)) {
            ufoController.Disappear();
            running.Remove(ufoController);
            waiting.Enqueue(ufoController);
        }
    }
}
