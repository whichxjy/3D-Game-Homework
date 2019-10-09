using UnityEngine;
using System.Collections.Generic;

public class Ruler {
    private static int[] UFONum = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
    private static float[] sizes = { 1f, 2f, 3f };
    private static float[] speeds = { 0.2f, 0.3f, 0.4f };
    private static int[] scores = { 1, 2, 3 };
    private static UFOColor[] colors = { UFOColor.Red, UFOColor.Green, UFOColor.Blue };

    public static List<UFOController> GenerateUFOs(int round) {
        List<UFOController> ufoControllers = new List<UFOController>();

        for (int i = 0; i < UFONum[round]; i++) {
            UFOInfo info = new UFOInfo();
            info.size = sizes[Random.Range(0, sizes.Length)];
            info.speed = speeds[Random.Range(0, speeds.Length)];
            info.score = scores[Random.Range(0, scores.Length)];
            info.color = colors[Random.Range(0, colors.Length)];
            UFOController ufoController = UFOFactory.GetInstance().GetUFO(info);
            ufoControllers.Add(ufoController);
        }

        return ufoControllers;
    }   
}
