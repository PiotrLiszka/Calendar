using System;
using System.Collections.Generic;

namespace Lib.ConsoleDraw;

public class DrawControls
{
    private readonly List<ConsoleKey> keys;
    private readonly SortedDictionary<ConsoleKey, string> controlsInformation = new();
    private readonly string prevNext;

    public DrawControls(List<ConsoleKey> key, CalendarDraw source)
    {
        keys = key;

        prevNext = source.GetType().Name switch
        {
            "DrawMain" => "miesiąc",
            "DrawDay" => "dzień",
            _ => "",
        };

        ControlInformationInit();
    }

    public void Draw()
    {
        foreach (ConsoleKey keyItem in keys)
        {
            try
            {
                Console.WriteLine($"{controlsInformation[keyItem]}");
            }
            catch (KeyNotFoundException)
            {
                Console.Write(keyItem.ToString());
                Console.WriteLine("\tBrak informacji o klawiszu");
            }
        }
    }

    private void ControlInformationInit()
    {
        if (controlsInformation.Count != 0)
            controlsInformation.Clear();

        controlsInformation.Add(ConsoleKey.LeftArrow, $"[ <- ]\tPoprzedni {prevNext}");
        controlsInformation.Add(ConsoleKey.RightArrow, $"[ -> ]\tNastępny {prevNext}");
        controlsInformation.Add(ConsoleKey.A, "[ A ]\tDodaj wydarzenie");
        controlsInformation.Add(ConsoleKey.Q, "[ Q ]\tSkróty klawiszowe");
        controlsInformation.Add(ConsoleKey.P, "[ P ]\tPokaż wydarzenia");
        controlsInformation.Add(ConsoleKey.D, "[ D ]\tWyświetlenie wybranego dnia");
    }
}
